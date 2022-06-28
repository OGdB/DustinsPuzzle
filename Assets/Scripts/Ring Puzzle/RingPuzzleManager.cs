using UnityEngine;
using UnityEngine.Events;

namespace Ring_Puzzle
{
    public class RingPuzzleManager : MonoBehaviour
    {
        [Tooltip("Everything that happens when the puzzle is finished"), Space(5)]
        public UnityEvent onPuzzleFinishedEvents;
        [Tooltip("Everything that happens when the level is finished")]
        public UnityEvent onLevelFinished;
        
        private int currentPosition = 0;

        private void Start()
        {
            RandomMoveOrnaments();
            RandomRotateRings();
        }

/*        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Ring[] rings = FindObjectsOfType<Ring>();
                foreach (var ring in rings)
                {
                    ring.CheatToCorrectRotation();
                }
            }
        }*/

        public void CorrectRotationCheck()
        {
            if (currentPosition < 5)
            {
                bool puzzleFinished = true;
                Ring[] rings = FindObjectsOfType<Ring>();
                for (int i = 0; i < rings.Length; i++)
                {
                    Ring ring = rings[i];
                    if (!ring.CorrectRotation)
                        puzzleFinished = false;
                }

                if (puzzleFinished)
                {
                    //Debug.Log("All rings on correct rotation!");
                    onPuzzleFinishedEvents.Invoke();
                }

                if (currentPosition == 5)
                {
                    onLevelFinished.Invoke();
                }
            }
        }

        public void MoveToNextConstellation()
        {
            currentPosition++;

            RandomMoveOrnaments();
            RandomRotateRings();
        }

        private void RandomMoveOrnaments()
        {
            if (currentPosition < 5)
            {
                ConstellationOrnament[] ornaments = FindObjectsOfType<ConstellationOrnament>();
                foreach (var ornament in ornaments)
                {
                    ornament.MoveToConstellationOrder(currentPosition);
                }
            }
        }
        private void RandomRotateRings()
        {
            Ring[] rings = FindObjectsOfType<Ring>();
            foreach (var ring in rings)
            {
                ring.RotateToRandomRotation();
            }
        }
/*

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
        }*/
    }
}
