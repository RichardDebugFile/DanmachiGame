using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 7f;
    public float jumpHeight = 7f;
    public bool isGrounded = true;
    public bool FacingRight = true;

    private bool isDead = false;
    private bool isAttacking = false;

    public GameObject gameOverScreen; // arrastra aquí tu objeto GameOver desde el editor

    void Update()
    {
        if (isDead)
        {
            if (Input.anyKeyDown)
            {
                // Reiniciar la escena actual
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        float movement = Input.GetAxis("Horizontal");

        if (!isAttacking)
        {
            transform.position += new Vector3(movement * Time.deltaTime * speed, 0f, 0f);
            Flip(movement);

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
                animator.SetBool("Jump", true);
                isGrounded = false;
            }
        }

        animator.SetFloat("Run", Mathf.Abs(movement) > 0.1f ? 1f : 0f);

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
            StartCoroutine(Attack());
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    void Flip(float movement)
    {
        if (movement < 0f && FacingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            FacingRight = false;
        }
        else if (movement > 0f && !FacingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            FacingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
        else if (other.collider.CompareTag("Dead") && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Player_dead");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;

        StartCoroutine(ShowGameOver());
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1.8f); // esperar animación de muerte
        animator.enabled = false; // congelar animación
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true); // mostrar pantalla Game Over
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("Ataque", true);
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
        animator.SetBool("Ataque", false);
    }
}
