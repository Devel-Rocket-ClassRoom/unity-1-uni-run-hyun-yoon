using UnityEngine;

public class PlayerControlor : MonoBehaviour
{
    public float jumpForce;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    public BoxCollider2D col;

    Vector3 originalSize;
    Vector3 slideSize;

    public GameManager gameManager;

    private int energyP = 5;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        originalSize = col.size;
        slideSize = new Vector3(originalSize.x, originalSize.y/2f, originalSize.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && jumpCount < 2)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++;
        }
        else if (Input.GetButtonUp("Fire1") && playerRigidbody.linearVelocity.y > 0)
        {
            playerRigidbody.linearVelocity *= 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isSliding", true);
            col.size = slideSize;
            col.offset = new Vector2(0f, -0.12f);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isSliding", false);
            col.size = originalSize;
            col.offset = new Vector2(0f, 0f);
        }

        if (gameManager.energy <= 0)
        {
            Die();
        }

        animator.SetBool("grounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platforms") &&
            collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platforms"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Dead") && !isDead)
        {
            Die();
        }

        if (collision.CompareTag("Hit") && !isDead)
        {
            gameManager.Hit(energyP * 4);
        }

        if (collision.CompareTag("Coin") && !isDead)
        {
            gameManager.AddCoinWithHealth(energyP);
            collision.gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        animator.SetTrigger("die");

        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        isDead = true;

        gameManager.OnPlayerDead();
    }
}
