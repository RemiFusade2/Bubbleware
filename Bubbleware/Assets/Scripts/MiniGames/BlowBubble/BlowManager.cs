using UnityEngine;
using System.Collections;

public class BlowManager : MonoBehaviour
{
    public BlowPlayer player1;
    public BlowPlayer player2;
    
    void OnEnable()
    {
        StartCoroutine(CloseTheGameAsync(10));
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (player1.Count > player2.Count)
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
