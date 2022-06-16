﻿using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float minX = -60f;
    public float maxX = 60f;

    public float sensitivity;
    private Camera cam;

    private Vector3 thisRot;
    private Vector3 cameraRot;

    public bool inverted = false;
    private int invertedInt = 1;

    private void Awake()
    {
        cam = Camera.main;
        thisRot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        cameraRot = new Vector3(-cam.transform.rotation.eulerAngles.x, 0, 0);
        invertedInt = inverted ? 1 : -1;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseXMove = Input.GetAxis("Mouse X");
        float mouseYMove = Input.GetAxis("Mouse Y");

        float ySpeed = mouseXMove * sensitivity;
        thisRot.y += ySpeed;
        float xSpeed = invertedInt * mouseYMove * sensitivity;
        cameraRot.x += xSpeed;

        cameraRot.x = Mathf.Clamp(cameraRot.x, minX, maxX);

        transform.localEulerAngles = thisRot;
        cam.transform.localEulerAngles = cameraRot;
    }

    private void OnValidate()
    {
        invertedInt = inverted ? 1 : -1;
    }
}
