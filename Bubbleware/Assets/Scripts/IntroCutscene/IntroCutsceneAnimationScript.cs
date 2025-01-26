using UnityEngine;

public class IntroCutsceneAnimationScript : MonoBehaviour
{
    public IntroCutsceneManager cutsceneManager;
    public void EndOfAnimation()
    {
        cutsceneManager.EndCutscene();
    }

    public void WolfHowl()
    {
        AudioManager.Instance.m_globalSfx.PlaySFX (7);
    }

    public void Thunder ()
    {
        AudioManager.Instance.m_globalSfx.PlaySFX (12);
    }
}
