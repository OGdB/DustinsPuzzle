using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float walkSpeed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

<<<<<<< HEAD
    private float speed = 5;
    private Rigidbody rigidbody;
=======
    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
>>>>>>> parent of 780afaa (Switched to new input system)



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

<<<<<<< HEAD
    private void OnEnable()
    {
        if (canRun)
        {
            InputManager.input.Main.Sprint.started += _ => speed = runSpeed;
            InputManager.input.Main.Sprint.canceled += _ => speed = walkSpeed;
        }
    }
    private void OnDisable()
    {
        if (canRun)
        {
            InputManager.input.Main.Sprint.started -= _ => speed = runSpeed;
            InputManager.input.Main.Sprint.canceled -= _ => speed = walkSpeed;
        }
    }

    private void FixedUpdate()
=======
    void FixedUpdate()
>>>>>>> parent of 780afaa (Switched to new input system)
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
<<<<<<< HEAD
        Vector2 targetVelocity = InputManager.GetMovementInput() * speed;
=======
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
>>>>>>> parent of 780afaa (Switched to new input system)

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}