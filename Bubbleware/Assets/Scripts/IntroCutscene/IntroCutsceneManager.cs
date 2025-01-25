using System.Collections;
using UnityEngine;

public class IntroCutsceneManager : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(PlayCutsceneAsync(1.0f));
    }

    private IEnumerator PlayCutsceneAsync(float delay)
    {
        yield return new WaitForSeconds(delay);

        MySceneManager.Instance.ShowHUBScreen();
    }
}
