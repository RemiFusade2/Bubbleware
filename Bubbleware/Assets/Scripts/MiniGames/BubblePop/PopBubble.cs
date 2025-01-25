using Unity.Mathematics;
using UnityEngine;
using System.Collections;

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

    private void Awake()
    {
        startPostition = transform.position;
    }

    private void OnEnable()
    {
        direction = 1;
        transform.position = startPostition;
        StartCoroutine(CloseTheGameAsync(10));
        RandomizeSwitchCountdown();
    }

    private void Update()
    {
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

    public bool IsPoppable()
    {
        return math.abs(0 - transform.position.y) < popRange;
    }

    private void RandomizeSwitchCountdown()
    {
        switchCountdown = UnityEngine.Random.Range(minSwitchTime, maxSwitchTime);
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        MySceneManager.Instance.ShowHUBScreen();
    }
}
