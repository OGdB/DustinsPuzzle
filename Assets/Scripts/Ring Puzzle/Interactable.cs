using UnityEngine;
using UnityEngine.Events;

namespace Ring_Puzzle
{
    [RequireComponent(typeof(Outline))]
    public class Interactable : MonoBehaviour
    {
        private bool hovered = false;
        private Outline outline;
        public UnityEvent onInteraction;

        private void Awake() => outline = GetComponent<Outline>();

        private void Start() => outline.RemoveOutline();

        public virtual void OnHover()
        {
            if (hovered) return;
            
            hovered = true;
            outline.CreateOutline();
        }
        public virtual void OnStopHover()
        {
            hovered = false;
            outline.RemoveOutline();
        }

        public virtual void OnInteraction()
        {
            onInteraction.Invoke();
        }
    }
}
