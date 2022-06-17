using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    [SerializeField] private bool rememberPosition = false;

    [Header("Aries")]
    [SerializeField] private bool setAriesPosition = false;
    [SerializeField] private Vector3 ariesPosition;

    [Header("Cancer")]
    [SerializeField] private bool setCancerPosition = false;
    [SerializeField] private Vector3 cancerPosition;

    [Header("Leo")]
    [SerializeField] private bool setLeoPosition = false;
    [SerializeField] private Vector3 leoPosition;

    [Header("Scorpio")]
    [SerializeField] private bool setScorpioPosition = false;
    [SerializeField] private Vector3 scorpioPosition;

    [Header("Taurus")]
    [SerializeField] private bool setTaurusPosition = false;
    [SerializeField] private Vector3 taurusPosition;

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
    }
}
