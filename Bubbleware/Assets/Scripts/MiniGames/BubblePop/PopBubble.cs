using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class PopBubble : MonoBehaviour
{
    public float speed;
    public float minY;
    public float maxY;
    public float popRange;

    private float direction;
    private Vector3 startPostition;

    private void Awake()
    {
        startPostition = transform.position;
    }

    private void OnEnable()
    {
        direction = 1;
        transform.position = startPostition;
        StartCoroutine(CloseTheGameAsync(10));
    }

    private void Update()
    {
        transform.Translate(0, direction * speed * Time.deltaTime, 0);
        if (transform.position.y < minY)
        {
            direction = 1;
        }
        else if (transform.position.y > maxY)
        {
            direction = -1;
        }
    }

    public bool IsPoppable()
    {
        return math.abs(0 - transform.position.y) < popRange;
    }

    private IEnumerator CloseTheGameAsync(float delay)
    {
        yield return new WaitForSeconds(delay);
        MySceneManager.Instance.ShowHUBScreen();
    }
}
