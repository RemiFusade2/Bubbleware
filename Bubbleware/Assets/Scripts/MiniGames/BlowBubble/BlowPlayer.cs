using UnityEngine;

public class BlowPlayer : MonoBehaviour, IPlayerController
{
    public int goal;
    public Transform bubble;
    public float scaleMultiplier;    

    public int Count {get; private set;}
    private float scale;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        scale = bubble.localScale.x;
    }

    private void OnEnable()
    {
        Count = 0;
        SetScale();
        audioPlayer = GetComponent<AudioPlayer>();
    }

    public void Move(Vector2 moveVector)
    {
    }

    public void OnCancel()
    {
    }

    public void OnConfirm()
    {
        Count++;
        SetScale();
        audioPlayer.PlaySFX (0);

        if (Count >= goal)
        {

            if (name == "Player 1")
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

    private void SetScale()
    {
        float newScale = scale + Count * scaleMultiplier;
        bubble.localScale = new Vector3(newScale, newScale, newScale);
        bubble.localPosition = new Vector3(newScale / 2, bubble.localPosition.y, bubble.localPosition.z);
    }
}
