using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 12f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _playerRenderer;
    private UIManager _uiManager;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = _rigidbody.linearVelocity.y;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
            y = JumpForce;

        _rigidbody.linearVelocity = new Vector2(x * Speed, y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            SpriteRenderer enemyRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (enemyRenderer == null) return;

            _playerRenderer.material.color = Color.red;
            enemyRenderer.material.color = Color.green;

            _uiManager.TakeDamage();

            Debug.Log("Столкновение с врагом: " + collision.gameObject.name, collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            SpriteRenderer enemyRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (enemyRenderer == null) return;

            _playerRenderer.material.color = Color.green;
            enemyRenderer.material.color = Color.red;

            Debug.Log("Разлетелись с врагом: " + collision.gameObject.name, collision.gameObject);
        }
    }
}
