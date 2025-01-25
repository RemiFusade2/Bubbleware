using UnityEngine;

public class TitlePlayerController : MonoBehaviour, IPlayerController
{
    private int clickCount;

    private void OnEnable()
    {
        this.GetComponent<RectTransform>().localScale = Vector3.one;
        clickCount = 0;
    }

    public void Move(Vector2 moveVector)
    {
        this.GetComponent<RectTransform>().anchoredPosition += 10*moveVector;
    }

    public void OnCancel()
    {
        // nothing
    }

    public void OnConfirm()
    {
        clickCount++;
        this.GetComponent<RectTransform>().localScale = Vector3.one * ((clickCount+2) * 0.5f);
        if (clickCount >= 5)
        {
            MySceneManager.Instance.StartGame();
        }
    }
}
