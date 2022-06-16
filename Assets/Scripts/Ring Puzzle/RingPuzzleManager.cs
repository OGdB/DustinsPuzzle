using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ring_Puzzle
{
    public class RingPuzzleManager : MonoBehaviour
    {
        [Tooltip("Everything that happens when the puzzle is finished"), Space(5)]
        public UnityEvent onPuzzleFinishedEvents;

        [SerializeField, Space(10)]
        private Ring[] rings;
        [SerializeField] 
        private Material playerMaterial;

        private void Awake()
        {
            if (rings.Length == 0)
            {
                Debug.LogError("There are no rings dragged into the inspector to check!");
            }
        }

        public void CorrectRotationCheck()
        {
            bool puzzleFinished = true;
            for (int i = 0; i < rings.Length; i++)
            {
                Ring ring = rings[i];
                if (!ring.CorrectRotation)
                    puzzleFinished = false;
            }

            if (puzzleFinished)
            {
                Debug.Log("All rings on correct rotation!");
                onPuzzleFinishedEvents.Invoke();
                _ = StartCoroutine(MaterialBlink());
            }
            else
                Debug.Log("At least 1 ring not on correct rotation yet!");
        }

        // Dirty copy & pase of Ring blink
        private IEnumerator MaterialBlink()
        {
            int blinked = 0;

            while (blinked < 10)
            {
                blinked++;

                Ring.SetCustomMaterialEmissionIntensity(playerMaterial, 4.6f);

                yield return new WaitForSeconds(0.2f);

                Ring.SetCustomMaterialEmissionIntensity(playerMaterial, 6f);

                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
