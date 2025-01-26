using UnityEngine;

public class PopNeedle : MonoBehaviour, IPlayerController
{
    public float speed;
    public float stopX;
    public PopBubble bubble;

    private float currentSpeed;
    private float minX;
    private float maxX;
    private Vector3 startPosition;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
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
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        currentSpeed = 0;
        transform.position = startPosition;
        audioPlayer = GetComponent<AudioPlayer> ();
    }

    private void Update()
    {
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
            if (name == "Player 1")
            {
                GameManager.Instance.PlayerOneWon();
            }
            else
            {
                GameManager.Instance.PlayerTwoWon();
            }

            AudioManager.Instance.m_globalSfx.PlaySFX (1);
            MySceneManager.Instance.ShowHUBScreen();
        }
    }

    public void Move(Vector2 moveVector)
    {
    }

    public void OnConfirm()
    {
        if (currentSpeed == 0)
        {
            currentSpeed = speed;
            audioPlayer.PlaySFX (0);
        }
    }

    public void OnCancel()
    {
    }
}
