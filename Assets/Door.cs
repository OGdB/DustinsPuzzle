using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    private Vector3 closedPosition;
    public Vector3 OpenPosition;
    public float openingSpeed = 2f;

    private WaitForFixedUpdate fixedUpdate;

    private void Awake()
    {
        fixedUpdate = new WaitForFixedUpdate();
        closedPosition = transform.position;
    }

    public void OpenDoor()
    {
        _ = StartCoroutine(LerpLocalPosition(openingSpeed, OpenPosition));
    }
    public void CloseDoor()
    {
        _ = StartCoroutine(LerpLocalPosition(openingSpeed, closedPosition));
    }
    public void NextScene() 
    { 
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NextScene();
        }
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
}
