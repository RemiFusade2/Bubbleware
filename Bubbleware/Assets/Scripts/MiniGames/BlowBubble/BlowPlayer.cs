using UnityEngine;
using UnityEngine.VFX;

public class BlowPlayer : MonoBehaviour, IPlayerController
{
    public int goal;
    public Transform bubble;
    public float scaleMultiplier;
    public BlowManager blowManager;

    public int Count {get; private set;}
    private float scale;
    private AudioPlayer audioPlayer;
    private VisualEffect pop;
    private MeshRenderer bubbleMesh;
    private bool ended;

    private void Awake()
    {
        scale = bubble.localScale.x;
        pop = bubble.GetComponent<VisualEffect>();
        bubbleMesh = bubble.GetComponent<MeshRenderer>();
        audioPlayer = GetComponent<AudioPlayer> ();

    }

    private void OnEnable()
    {
        Count = 0;
        SetScale();
        pop.Stop();
        bubbleMesh.enabled = true;
        ended = false;
    }

    public void Move(Vector2 moveVector)
    {
    }

    public void OnCancel()
    {
    }

    public void OnConfirm()
    {
        if (ended)
        {
            return;
        }
        Count++;
        SetScale();
        audioPlayer.PlaySFX (0);

        if (Count >= goal)
        {
            bubbleMesh.enabled = false;
            pop.Play();
            AudioManager.Instance.m_globalSfx.PlaySFX (0);
            blowManager.EndGame();
        }
    }

    private void SetScale()
    {
        float newScale = scale + Count * scaleMultiplier;
        bubble.localScale = new Vector3(newScale, newScale, newScale);
        bubble.localPosition = new Vector3(newScale / 2, bubble.localPosition.y, bubble.localPosition.z);
    }

    public void End()
    {
        ended = true;
    }
}
