using UnityEngine;

namespace Ring_Puzzle
{
    public class Interaction : MonoBehaviour
    {
        private Camera cam;
        [SerializeField] 
        private TMPro.TextMeshProUGUI interactText;
        [SerializeField]
        private float interactionDistance = 1.25f;
        
        private Interactable currentInteractable;

        private void Update()
        {
            Raycast();

            if (Input.GetMouseButtonDown(0) && currentInteractable)
            {
                OnInteract();
            }
        }

        private void OnInteract()
        {
            currentInteractable.OnInteraction();
        }

        private void Raycast()
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactionDistance))
            {
                if (hit.transform.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    //print("Found interactable");
                    if (currentInteractable && interactable.GetHashCode() != currentInteractable.GetHashCode())
                    {
                        //print("Switch from interactable.");
                        StopCurrentHover();
                        StartHover(interactable);
                    }
                    else
                    {
                        StartHover(interactable);
                    }
                }
                else  // Nothing was hovered over.
                {
                    StopCurrentHover();
                }
            }
            else if (currentInteractable) StopCurrentHover();
        }

        private void StartHover(Interactable interactable)
        {
            //print("Start hover");            
            currentInteractable = interactable;
            interactText.enabled = true;
            currentInteractable.OnHover();
        }
        private void StopCurrentHover()
        {
            if (!currentInteractable) return;
        
            //print("Stop hover");
            interactText.enabled = false;
            currentInteractable.OnStopHover();
            currentInteractable = null;
        }

        private void Awake()
        {
            cam = Camera.main;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawRay(cam.transform.position, cam.transform.forward * interactionDistance);
        }
    }
}
