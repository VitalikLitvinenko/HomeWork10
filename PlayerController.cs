using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _playerRenderer;
    private bool _isGrounded = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        _rigidbody.linearVelocity = new Vector2(x * speed, _rigidbody.linearVelocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
            _isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            _isGrounded = true;

            SpriteRenderer enemyRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (enemyRenderer == null) return;

            _playerRenderer.material.color = Color.red;
            enemyRenderer.material.color = Color.green;

            Debug.Log("Столкновение с врагом: " + collision.gameObject.name, collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            SpriteRenderer enemyRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (enemyRenderer == null) return;

            _playerRenderer.material.color = Color.green;
            enemyRenderer.material.color = Color.red;

            Debug.Log("Разошлись с врагом: " + collision.gameObject.name, collision.gameObject);
        }
    }
}
