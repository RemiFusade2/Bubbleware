using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class StockMarketManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform stockMarketCamera;
    public GameObject startPanel;
    public TMP_Text p1ScoreText;
    public TMP_Text p2ScoreText;
    public TMP_Text winnerText;
    [Space]
    public GameObject followStockMarketValueObject;
    public ParticleSystem buyParticleSystem;
    public ParticleSystem sellParticleSystem;
    public VisualEffect buyVisualEffect;

    [Header("Settings")]
    public float delayBetweenNewValues = 0.1f;
    public float delayBeforeStartOfGame = 1;
    public float delayBeforeEndOfGame = 10;

    private int positionCount = 1;

    private float lastTime = 0;
    private float lastValue = 0;

    private float trendMin;
    private float trendMax;

    private float p1Score;
    private float p2Score;
    private float p1MarketExposure;
    private float p2MarketExposure;

    private bool gameIsRunning;

    private void OnEnable()
    {
        winnerText.text = "";
        InitializeLine();
        InitializeScores();
        startPanel.SetActive(true);
        StartCoroutine(AddRandomValueAsync(delayBeforeStartOfGame + delayBetweenNewValues));
        StartCoroutine(RemoveStartPanel(delayBeforeStartOfGame));
        StartCoroutine(StopTheGameAsync(delayBeforeEndOfGame));
    }

    private void InitializeScores()
    {
        p1Score = 100000;
        p2Score = 100000;
        p1MarketExposure = 1;
        p2MarketExposure = 1;
        p1ScoreText.text = $"P1 Portfolio:\nUS$ {p1Score.ToString("0")}";
        p2ScoreText.text = $"P2 Portfolio:\nUS$ {p2Score.ToString("0")}";
    }

    private IEnumerator RemoveStartPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        startPanel.SetActive(false);
        gameIsRunning = true;
    }

    private IEnumerator AddRandomValueAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        AddRandomValue(delayBetweenNewValues);
        IncreaseScores();
        if (gameIsRunning)
        {
            StartCoroutine(AddRandomValueAsync(delayBetweenNewValues));
        }
    }

    private void InitializeLine()
    {
        positionCount = 0;
        lastTime = 0;
        lastValue = Random.Range(0, 1.0f);
        trendMin = -0.25f;
        trendMax = 0.3f;
        for (int i = 0; i < 50; i++)
        {
            AddRandomValue(delayBetweenNewValues);
        }

        // Bubble random changes of trend
        for (int i = 0; i < 7; i++)
        {
            StartCoroutine(SetBubbleTrend(i, (Random.Range(-0.01f, 0.03f))));
        }
        for (int i = 1; i < 7; i++)
        {
            if (Random.Range(0, 2) % 2 != 0)
            {
                // Bubble bursts
                StartCoroutine(SetBubbleTrend(Random.Range(i-0.5f, i), -Random.Range(0.2f, 0.4f)));
                StartCoroutine(SetBubbleTrend(Random.Range(i, i+0.5f), Random.Range(0.2f, 0.4f)));
            }
        }
        // Final bubble burst
        StartCoroutine(SetBubbleTrend(Random.Range(8.5f, 9.5f), -0.4f));
    }

    private IEnumerator SetBubbleTrend(float delay, float trendDelta)
    {
        yield return new WaitForSeconds(delay);
        trendMin += trendDelta;
        trendMax += trendDelta;
    }

    private void AddRandomValue(float deltaTime)
    {
        // Add new value
        positionCount++;
        lastTime += deltaTime;
        lastValue += Random.Range(trendMin, trendMax);
        lineRenderer.positionCount = positionCount;
        lineRenderer.SetPosition(positionCount-1, new Vector3(lastTime, lastValue, 0));

        // Move camera, center on latest value and make it smooth
        Vector3 targetCameraPosition = new Vector3(lastTime, lastValue, -10);
        stockMarketCamera.position = Vector3.Lerp(stockMarketCamera.position, targetCameraPosition, 0.25f);

        // Set color
        if (positionCount > 50)
        {
            float valueTrend = lineRenderer.GetPosition(positionCount - 1).y - lineRenderer.GetPosition(positionCount - 50).y;
            lineRenderer.startColor = (valueTrend < 0) ? Color.red : Color.green;
            lineRenderer.endColor = (valueTrend < 0) ? Color.red : Color.green;
        }

        followStockMarketValueObject.transform.position = new Vector3(lastTime, lastValue, 0);
    }

    private void IncreaseScores()
    {
        float deltaMarketValue = lineRenderer.GetPosition(positionCount - 1).y - lineRenderer.GetPosition(positionCount - 2).y;
        p1Score += p1MarketExposure * deltaMarketValue * 999;
        p2Score += p2MarketExposure * deltaMarketValue * 999;

        p1ScoreText.text = $"P1 Portfolio:\nUS$ {p1Score.ToString("0")}";
        p2ScoreText.text = $"P2 Portfolio:\nUS$ {p2Score.ToString("0")}";
    }

    public void P1Buy()
    {
        p1MarketExposure++;
        buyVisualEffect.playRate += 0.1f;
        //buyParticleSystem.Emit(1);
    }
    public void P1Sell()
    {
        p1MarketExposure--;
        buyVisualEffect.playRate -= 0.1f;
        //sellParticleSystem.Emit(1);
    }
    public void P2Buy()
    {
        p2MarketExposure++;
        buyParticleSystem.Emit(1);
    }
    public void P2Sell()
    {
        p2MarketExposure--;
        sellParticleSystem.Emit(1);
    }

    private IEnumerator StopTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);

        // stop game
        gameIsRunning = false;

        // show winner
        if (p1Score > p2Score)
        {
            winnerText.text = "Player 1 wins!";
        }
        else if (p2Score > p1Score)
        {
            winnerText.text = "Player 2 wins!";
        }
        else
        {
            winnerText.text = "It's a tie";
        }

        // close game
        StartCoroutine(CloseTheGameAsync(1.0f));
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (p1Score > p2Score)
        {
            GameManager.Instance.PlayerOneWon();
        }
        else if (p2Score > p1Score)
        {
            GameManager.Instance.PlayerTwoWon();
        }

        MySceneManager.Instance.ShowHUBScreen();
    }
}
