using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OutroCutsceneManager : MonoBehaviour
{    
    private void OnEnable()
    {
    }

    public void ShowScoreSceen()
    {
        MySceneManager.Instance.ShowScoreSceen();
    }
}
