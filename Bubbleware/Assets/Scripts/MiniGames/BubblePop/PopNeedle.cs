using UnityEngine;

public class PopNeedle : MonoBehaviour
{
    public float speed;
    public float stopX;
    public KeyCode key;
    public PopBubble bubble;

    private float currentSpeed;
    private float minX;
    private float maxX;

    private void Start()
    {
        currentSpeed = 0;
        if (speed < 0)
        {
            minX = stopX;
            maxX = transform.position.x;
        }
        else
        {
            minX = transform.position.x;
            maxX = stopX;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(key) && currentSpeed == 0)
        {
            currentSpeed = speed;
        }
        transform.Translate(currentSpeed * Time.deltaTime, 0, 0, Space.World);
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            if (speed < 0)
            {
                currentSpeed = -speed;
                CheckWin();
            }
            else
            {
                currentSpeed = 0;
            }
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            if (speed < 0)
            {
                currentSpeed = 0;
            }
            else
            {
                currentSpeed = -speed;
                CheckWin();
            }
        }
    }

    private void CheckWin()
    {
        if (bubble.IsPoppable())
        {
            if (name == "Player1")
            {
                GameManager.Instance.PlayerOneWon();
            }
            else
            {
                GameManager.Instance.PlayerTwoWon();
            }
            MySceneManager.Instance.ShowHUBScreen();
        }
    }
}
