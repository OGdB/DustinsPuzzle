using UnityEngine;

public class Quit : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.input.Main.Quit.started += _ => QuitNow();
    }
    private void OnDisable()
    {
        InputManager.input.Main.Quit.started -= _ => QuitNow();
    }

    private void QuitNow() => Application.Quit();
}
