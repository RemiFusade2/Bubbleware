using Unity.Mathematics;
using UnityEngine;

public class PopBubble : MonoBehaviour
{
    public float speed;
    public float minY;
    public float maxY;
    public float popRange;

    private float direction;

    private void Start()
    {
        direction = 1;
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
}
