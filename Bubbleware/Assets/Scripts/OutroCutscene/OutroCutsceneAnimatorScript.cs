using UnityEngine;

public class OutroCutsceneAnimatorScript : MonoBehaviour
{
    public OutroCutsceneManager OutroManager;

    public void PopBubble()
    {

    }

    public void AnimationEnd()
    {
        OutroManager.ShowScoreSceen();
    }
}
