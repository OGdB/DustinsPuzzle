using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputControls input;

    private void Awake() => input = new InputControls();
    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    public static Vector2 GetMovementInput() => input.Main.Move.ReadValue<Vector2>();
    public static Vector2 GetLookInput() => input.Main.Look.ReadValue<Vector2>();
    public static bool GetInteractInput() => input.Main.Interact.ReadValue<bool>();
}
