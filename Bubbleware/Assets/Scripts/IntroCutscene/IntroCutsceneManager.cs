using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutsceneManager : MonoBehaviour
{
    public List<GameObject> imagesList;
    public float delayBetweenImages;

    private int visibleImageIndex;

    private void HideImages()
    {
        foreach (GameObject image in imagesList)
        {
            image.GetComponent<Image>().enabled = false;
        }
    }

    private void OnEnable()
    {
        HideImages();
        visibleImageIndex = 0;
        imagesList[visibleImageIndex].GetComponent<Image>().enabled = true;
        StartCoroutine(PlayCutsceneAsync(delayBetweenImages));
    }

    private IEnumerator PlayCutsceneAsync(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (visibleImageIndex < imagesList.Count-1)
        {
            imagesList[visibleImageIndex].GetComponent<Image>().enabled = false;
            visibleImageIndex++;
            imagesList[visibleImageIndex].GetComponent<Image>().enabled = true;
            StartCoroutine(PlayCutsceneAsync(delayBetweenImages));
        }
        else
        {
            MySceneManager.Instance.ShowHUBScreen();
        }
    }
}
