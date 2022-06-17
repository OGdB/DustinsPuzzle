using System.Collections;
using UnityEngine;

/// <summary>
/// Set the starting position of a puzzle ornament depending on the current progression.
/// </summary>
public class ConstellationOrnament : MonoBehaviour
{
    [SerializeField] private float constellationMovementSpeed = 5f;
    [SerializeField] private bool rememberPosition = false;

    [Header("Aries")]
    [SerializeField] private bool setAriesPosition = false;
    [SerializeField] private Vector3 ariesPosition;

    [Header("Cancer")]
    [SerializeField] private bool setCancerPosition = false;
    [SerializeField] private Vector3 cancerPosition;

    [Header("Taurus")]
    [SerializeField] private bool setTaurusPosition = false;
    [SerializeField] private Vector3 taurusPosition;

    [Header("Leo")]
    [SerializeField] private bool setLeoPosition = false;
    [SerializeField] private Vector3 leoPosition;

    [Header("Scorpio")]
    [SerializeField] private bool setScorpioPosition = false;
    [SerializeField] private Vector3 scorpioPosition;

    public Vector3[] ConstellationOrder { get; private set; }
    private WaitForFixedUpdate fixedUpdate;

    private void Awake()
    {
        fixedUpdate = new WaitForFixedUpdate();
        ConstellationOrder = new Vector3[5];
        ConstellationOrder[0] = ariesPosition;
        ConstellationOrder[1] = cancerPosition;
        ConstellationOrder[2] = taurusPosition;
        ConstellationOrder[3] = leoPosition;
        ConstellationOrder[4] = scorpioPosition;
    }

    public void MoveToConstellationOrder(int constellationInt)
    {
        _ = StartCoroutine(LerpLocalPosition(constellationMovementSpeed, ConstellationOrder[constellationInt]));
    }

    private IEnumerator LerpLocalPosition(float speed, Vector3 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.localPosition;

        while (transform.localPosition != targetPosition)
        {
            float timeSinceStarted = Time.time - startTime;
            float progress = timeSinceStarted / speed;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);

            yield return fixedUpdate;
        }
    }

    private void OnValidate()
    {
        if (rememberPosition == true)
        {
            if (setAriesPosition)
            {
                ariesPosition = transform.localPosition;
                setAriesPosition = false;
            }
            else if (setCancerPosition)
            {
                cancerPosition = transform.localPosition;
                setCancerPosition = false;
            }
            else if (setLeoPosition)
            {
                leoPosition = transform.localPosition;
                setLeoPosition = false;
            }
            else if (setScorpioPosition)
            {
                scorpioPosition = transform.localPosition;
                setScorpioPosition = false;
            }
            else if (setTaurusPosition)
            {
                taurusPosition = transform.localPosition;
                setTaurusPosition = false;
            }
            rememberPosition = false;
        }

        ConstellationOrder = new Vector3[5];
        ConstellationOrder[0] = ariesPosition;
        ConstellationOrder[1] = cancerPosition;
        ConstellationOrder[2] = taurusPosition;
        ConstellationOrder[3] = leoPosition;
        ConstellationOrder[4] = scorpioPosition;
    }
}
