using System.Collections;
using UnityEngine;

public class StockMarketManager : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CloseTheGameAsync(10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.PlayerOneWon();

        MySceneManager.Instance.ShowHUBScreen();
    }
}
