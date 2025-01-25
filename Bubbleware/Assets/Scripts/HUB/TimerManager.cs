using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public List<GameObject> numbersList;

    public int delayBetweenNumbers = 1;

    private int numberIndex;

    private void OnEnable()
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

    private IEnumerator DecrementTimerAsync(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (numberIndex >= numbersList.Count-1)
        {
            StartNewMiniGame();
        }
        else
        {
            numberIndex++;
            DisplayTimer();
            StartCoroutine(DecrementTimerAsync(delayBetweenNumbers));
        }
    }

    private void StartNewMiniGame()
    {
        MySceneManager.Instance.ShowRandomMiniGame();
    }
}
