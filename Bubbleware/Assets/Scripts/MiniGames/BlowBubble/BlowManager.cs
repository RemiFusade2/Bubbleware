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
            AudioManager.Instance.m_globalSfx.PlaySFX (0);

            GameManager.Instance.PlayerOneWon();
        }
        else if (player2.Count > player1.Count)
        {
            GameManager.Instance.PlayerTwoWon();
        }
        MySceneManager.Instance.ShowHUBScreen();
    }
}
