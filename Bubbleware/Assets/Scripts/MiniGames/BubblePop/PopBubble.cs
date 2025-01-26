using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class PopBubble : MonoBehaviour
{
    public float speed;
    public float minY;
    public float maxY;
    public float popRange;
    public float minSwitchTime;
    public float maxSwitchTime;

    private float direction;
    private Vector3 startPostition;
    private float switchCountdown;
    private VisualEffect pop;
    private MeshRenderer bubble;
    private bool ended;

    private void Awake()
    {
        startPostition = transform.position;
        pop = GetComponent<VisualEffect>();
        bubble = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        direction = 1;
        transform.position = startPostition;
        StartCoroutine(CloseTheGameAsync(10));
        RandomizeSwitchCountdown();
        pop.Stop();
        bubble.enabled = true;
        ended = false;
    }

    private void Update()
    {
        if (ended)
        {
            return;
        }
        switchCountdown -= Time.deltaTime;
        if (switchCountdown <= 0)
        {
            RandomizeSwitchCountdown();
            direction = -direction;
        }
        transform.Translate(0, direction * speed * Time.deltaTime, 0);
        if (transform.position.y < minY)
        {
            direction = 1;
            RandomizeSwitchCountdown();
        }
        else if (transform.position.y > maxY)
        {
            direction = -1;
            RandomizeSwitchCountdown();
        }
    }

    public bool Pop()
    {
        bool poppable = math.abs(0 - transform.position.y) < popRange;
        if (poppable)
        {
            ended = true;
            bubble.enabled = false;
            pop.Play();
            StopAllCoroutines();
            StartCoroutine(CloseTheGameAsync(1));
        }
        return poppable;
    }

    private void RandomizeSwitchCountdown()
    {
        switchCountdown = UnityEngine.Random.Range(minSwitchTime, maxSwitchTime);
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!ended)
        {
            //no winner
            AudioManager.Instance.m_globalSfx.PlaySFX (4);
        }
        MySceneManager.Instance.ShowHUBScreen();
    }
}
