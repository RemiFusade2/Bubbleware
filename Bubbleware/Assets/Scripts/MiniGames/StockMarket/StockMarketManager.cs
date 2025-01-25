using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StockMarketManager : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private int positionCount = 1;

    private float lastTime = 0;
    private float lastValue = 0;

    private void OnEnable()
    {
        positionCount = 0;
        lastTime = 0;
        lastValue = Random.Range(0, 1.0f);
        AddRandomValue(0);
        StartCoroutine(AddRandomValueAsync(0.2f));
        StartCoroutine(CloseTheGameAsync(10));
    }

    // Update is called once per frame
    void Update()
    {
        //AddRandomValue(Time.deltaTime);
    }

    private IEnumerator AddRandomValueAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        AddRandomValue(delay);
        StartCoroutine(AddRandomValueAsync(delay));
    }

    private void AddRandomValue(float deltaTime)
    {
        positionCount++;
        lastTime += deltaTime;
        lastValue += Random.Range(-0.1f, 0.1f);
        lineRenderer.positionCount = positionCount;
        lineRenderer.SetPosition(positionCount-1, new Vector3(lastTime, lastValue, 0));
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.PlayerOneWon();

        MySceneManager.Instance.ShowHUBScreen();
    }
}
