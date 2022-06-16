using System.Collections;
using UnityEngine;

namespace Ring_Puzzle
{
    public class RotateButton : Interactable
    {
        [SerializeField]
        private Ring connectedRing;
        public override void OnInteraction()
        {
            if (connectedRing.IsRotating) return;

            base.OnInteraction();
        }
    }
}