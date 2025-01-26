using UnityEngine;

public class OutroCutsceneAnimatorScript : MonoBehaviour
{
    public OutroCutsceneManager OutroManager;

    public void PopBubble()
    {
        AudioManager.Instance.m_globalSfx.PlaySFX (8);
        AudioManager.Instance.m_globalSfx.PlaySFX (11);
    }

    public void AnimationEnd()
    {
        OutroManager.ShowScoreSceen();
    }

    public void Monster01 ()
    {
        AudioManager.Instance.m_globalSfx.PlaySFX (9);
        AudioManager.Instance.m_globalSfx.PlaySFX (13);
    }

    public void Monster02 ()
    {
        AudioManager.Instance.m_globalSfx.PlaySFX (10);
    }
}
