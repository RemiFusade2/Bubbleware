using UnityEngine;

public class IntroCutsceneAnimationScript : MonoBehaviour
{
    public IntroCutsceneManager cutsceneManager;
    public void EndOfAnimation()
    {
        cutsceneManager.EndCutscene();
    }
}
