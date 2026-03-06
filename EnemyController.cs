using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 2f;
    public float Distance = 2f;

    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        float x = _startPosition.x + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector2(x, transform.position.y);
    }
}
