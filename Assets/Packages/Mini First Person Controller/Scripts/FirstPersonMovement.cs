using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float walkSpeed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;

    private float speed = 5;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

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
    {
        // Get targetMovingSpeed.
        Vector2 targetVelocity = InputManager.GetMovementInput() * speed;

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}