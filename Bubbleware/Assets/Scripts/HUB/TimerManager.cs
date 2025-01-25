using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;

    public int startTime = 3;

    private int timer;

    private Coroutine DecrementTimerCoroutine;

    private void OnEnable()
    {
        timer = startTime;
        DisplayTimerText();
        DecrementTimerCoroutine = StartCoroutine(DecrementTimerAsync(1));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void DisplayTimerText()
    {
        timerText.text = timer.ToString("0");
    }

    private IEnumerator DecrementTimerAsync(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        timer--;
        DisplayTimerText();
        if (timer <= 0)
        {
            BackToTitleScreen();
        }
        else
        {
            DecrementTimerCoroutine = StartCoroutine(DecrementTimerAsync(1));
        }
    }

    private void BackToTitleScreen()
    {
        MySceneManager.Instance.ShowTitleScreen();
    }
}
