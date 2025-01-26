using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TimerManager : MonoBehaviour
{
    public Transform konfettiParent;

    public List<GameObject> numbersList;

    public GameObject tie;
    public GameObject player1Wins;
    public GameObject player2Wins;

    public int delayBetweenNumbers = 1;
    public int delayPlayerWinsPanel = 2;

    private int numberIndex;

    public int displayPlayerWinsPanel;

    private AudioPlayer audioPlayer;

    private void OnEnable()
    {
        HideNumbers();
        HidePlayerWinTexts();
        audioPlayer = GetComponent<AudioPlayer> ();

        if (displayPlayerWinsPanel != -1)
        {
            StartCoroutine(ShowPlayerWinsPanel(delayPlayerWinsPanel));
        }
        else
        {
            EndRound();
        }

        foreach (Transform konfettiChild in konfettiParent)
        {
            if (konfettiChild.gameObject.activeInHierarchy)
            {
                konfettiChild.GetComponent<VisualEffect>().Stop();
            }
        }
    }

    private void PlayKonfettis()
    {
        foreach (Transform konfettiChild in konfettiParent)
        {
            if (konfettiChild.gameObject.activeInHierarchy)
            {
                konfettiChild.GetComponent<VisualEffect>().Play();
            }
        }
    }

    private void HidePlayerWinTexts()
    {
        player1Wins.SetActive(false); player2Wins.SetActive(false); tie.SetActive(false);
    }

    private void EndRound()
    {
        if (GameManager.Instance.IsGameOver())
        {
            // Game is over, we show outro cutscene
            MySceneManager.Instance.ShowOutroCutscene();
        }
        else
        {
            // Game is not over, we show timer
            numberIndex = 0;
            DisplayTimer();
            audioPlayer.PlaySFX (numberIndex);
            StartCoroutine(DecrementTimerAsync(delayBetweenNumbers));
        }
    }

    private void HideNumbers()
    {
        foreach (GameObject number in numbersList)
        {
            number.GetComponent<Image>().enabled = false;
        }
    }

    private void DisplayTimer()
    {
        HideNumbers();
        numbersList[numberIndex].GetComponent<Image>().enabled = true;
    }

    private IEnumerator ShowPlayerWinsPanel(float delay)
    {
        yield return new WaitForEndOfFrame();
        if (displayPlayerWinsPanel == 1)
        {
            player1Wins.SetActive(true);
            PlayKonfettis();
        }
        else if (displayPlayerWinsPanel == 2)
        {
            player2Wins.SetActive(true);
            PlayKonfettis();
        }
        else
        {
            tie.SetActive(true);
        }
        displayPlayerWinsPanel = -1;
        yield return new WaitForSecondsRealtime(delay);
        HidePlayerWinTexts();
        EndRound();
    }

    private IEnumerator DecrementTimerAsync(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (numberIndex >= numbersList.Count-1)
        {
            audioPlayer.PlaySFX (numberIndex);
            StartNewMiniGame();
        }
        else
        {
            numberIndex++;
            audioPlayer.PlaySFX (numberIndex);

            //if (numberIndex == 0)
            //    audioPlayer.PlaySFX (0);
            //else if (numberIndex == 1) 
            //    audioPlayer.PlaySFX (1);
            //else if (numberIndex == 2) 
            //    audioPlayer.PlaySFX (2);

            DisplayTimer();
            StartCoroutine(DecrementTimerAsync(delayBetweenNumbers));
        }
    }

    private void StartNewMiniGame()
    {
        MySceneManager.Instance.ShowRandomMiniGame();
    }
}
