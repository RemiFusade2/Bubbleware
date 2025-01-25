using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitAndGoBackToTitle(2.0f));
    }

    private IEnumerator WaitAndGoBackToTitle(float delay)
    {
        yield return new WaitForSeconds(delay);

        MySceneManager.Instance.ShowTitleScreen();
    }
}
