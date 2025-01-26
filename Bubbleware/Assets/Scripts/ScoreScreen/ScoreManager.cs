using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{ 
    public TMP_Text P1Wins;
    public TMP_Text P2Wins;
    public float delayBeforeGoingBackToTitle = 3;

    private void OnEnable()
    {
        P1Wins.gameObject.SetActive(false);
        P2Wins.gameObject.SetActive(false);
        if (GameManager.Instance.player1Score > GameManager.Instance.player2Score)
        {
            P1Wins.gameObject.SetActive(true);
        }
        else
        {
            P2Wins.gameObject.SetActive(true);
        }
        StartCoroutine(WaitAndGoBackToTitle(delayBeforeGoingBackToTitle));
    }

    private IEnumerator WaitAndGoBackToTitle(float delay)
    {
        yield return new WaitForSeconds(delay);

        MySceneManager.Instance.ShowTitleScreen();
        AudioManager.Instance.StopMusic ();
        AudioManager.Instance.OnSceneActivated ("TitleScreen");
    }
}
