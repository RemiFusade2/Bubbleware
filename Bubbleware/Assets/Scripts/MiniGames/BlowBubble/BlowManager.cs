using UnityEngine;
using System.Collections;

public class BlowManager : MonoBehaviour
{
    public BlowPlayer player1;
    public BlowPlayer player2;

    void OnEnable ()
    {
        StartCoroutine (CloseTheGameAsync (10));
    }
     

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (player1.Count > player2.Count)
        {
            GameManager.Instance.PlayerOneWon ();
        }
        else if (player2.Count > player1.Count)
        {
            GameManager.Instance.PlayerTwoWon ();
        }
        else
        {
            AudioManager.Instance.m_globalSfx.PlaySFX (4);
        }

        MySceneManager.Instance.ShowHUBScreen ();
        
    }

    public void EndGame()
    {
        player1.End();
        player2.End();
        StopAllCoroutines();
        StartCoroutine(CloseTheGameAsync(1));
    }
}
