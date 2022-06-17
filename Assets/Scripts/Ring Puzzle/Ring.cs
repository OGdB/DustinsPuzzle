using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ring_Puzzle
{
    public class Ring : MonoBehaviour
    {
        [SerializeField, Tooltip("The rotation that is 'correct' for this ring"), Range(0, 359)] 
        private float correctYRotation;

        [SerializeField, Tooltip("The speed at which the ring rotates"), Range(0.1f, 1f)] 
        private float rotationSpeed = 0.4f;

        [SerializeField, Tooltip("The amount of degrees the ring rotates on function call"), Range(1, 60)]
        private float rotateAmount = 15f;

        [SerializeField, Tooltip("The amount of times the ornament will blink when on the correct rotation"), Range(1, 5)]
        private int blinkAmount = 3;
        [SerializeField, Tooltip("The pause length between the blinks"), Range(0, 1f)]
        private float blinkInterval = 0.2f;

        [Header("Assignables")]
        [SerializeField, Tooltip("The pivot around which the ring rotates")] 
        private Transform centerPoint;
        
        //[SerializeField, Tooltip("The key to press to make this ring rotate")]
        //private KeyCode rotateKey = KeyCode.R;

        // Other variables for technical stuff
        public bool IsRotating { get => isRotating; }
        private bool isRotating = false;
        private WaitForFixedUpdate fixedUpdate;
        private WaitForSeconds blinkIntervalWait;
        private List<Material> emissiveOrnaments = new();
        public bool CorrectRotation { get; set; } = false;

        private void Awake()
        {
            fixedUpdate = new WaitForFixedUpdate();
            blinkIntervalWait = new WaitForSeconds(blinkInterval);

            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {
                emissiveOrnaments.Add(renderer.material);
            }
        }

        public void RotateRing()
        {
            if (isRotating) return;
            _ = StartCoroutine(Rotate());
        }

        public void RotateToRandomRotation()
        {
            _ = StartCoroutine(RotateRandom());

            IEnumerator RotateRandom()
            {
                isRotating = true;

                int randomInt = Random.Range(1, 5);
                float randomRotateAmount = randomInt * rotateAmount;

                float targetRotation = transform.rotation.eulerAngles.y + randomRotateAmount;
                Vector3 targetAngle = transform.rotation.eulerAngles;
                targetAngle.y = targetRotation;

                float rotatedAmount = 0;
                while (rotatedAmount <= randomRotateAmount)
                {
                    rotatedAmount += rotationSpeed;

                    transform.RotateAround(centerPoint.position, Vector3.up, rotationSpeed);

                    yield return fixedUpdate;
                }

                transform.rotation = Quaternion.Euler(targetAngle);

                isRotating = false;
            }
        }

        private IEnumerator Rotate()
        {
            isRotating = true;
            float targetRotation = transform.rotation.eulerAngles.y + rotateAmount;

            Vector3 targetAngle = transform.rotation.eulerAngles;
            targetAngle.y = targetRotation;

            float rotatedAmount = 0;
            while (rotatedAmount <= rotateAmount)
            {
                rotatedAmount += rotationSpeed;

                transform.RotateAround(centerPoint.position, Vector3.up, rotationSpeed);

                yield return fixedUpdate;
            }
        
            transform.rotation = Quaternion.Euler(targetAngle);

            if ((int)transform.rotation.eulerAngles.y == (int)correctYRotation)
                yield return MaterialBlink();  // Wait until blinking is done
            else
                CorrectRotation = false;  // Will set controlling bool back to false (in case the player rotates the ring again)

            isRotating = false;
        }

        private IEnumerator MaterialBlink()
        {
            int blinked = 0;

            while (blinked < blinkAmount)
            {
                blinked++;

                foreach (Material mat in emissiveOrnaments)
                {
                    SetCustomMaterialEmissionIntensity(mat, 4.6f);
                }

                yield return blinkIntervalWait;

                foreach (Material mat in emissiveOrnaments)
                {
                    SetCustomMaterialEmissionIntensity(mat, 6f);
                }

                yield return blinkIntervalWait;
            }

            CorrectRotation = true;
            //GetComponentInParent<RingPuzzleManager>().CorrectRotationCheck();
        }

        // Found: https://forum.unity.com/threads/setting-material-emission-intensity-in-script-to-match-ui-value.661624/
        // Annoying changing values of shaders.
        public static void SetCustomMaterialEmissionIntensity(Material mat, float intensity)
        {
            // get the material at this path
            Color color = mat.GetColor("_Color");

            // for some reason, the desired intensity value (set in the UI slider) needs to be modified slightly for proper internal consumption
            float adjustedIntensity = intensity - (0.4169F);

            // redefine the color with intensity factored in - this should result in the UI slider matching the desired value
            color *= Mathf.Pow(2.0F, adjustedIntensity);
            mat.SetColor("_EmissionColor", color);
            //Debug.Log("<b>Set custom emission intensity of " + intensity + " (" + adjustedIntensity + " internally) on Material: </b>" + emissiveMaterial);
        }
    }
}