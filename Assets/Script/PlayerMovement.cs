using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    bool alive = true;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    Animator myAnimator;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    void Start()
    {
        // Get reference to the Animator component
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

        if (Mathf.Abs(horizontalInput) > 0)
        {
            myAnimator.SetBool("Fast Run", true);
        }
        else
        {
            myAnimator.SetBool("Fast Run", false);
        }
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Jump();
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            myAnimator.SetTrigger("Jump");
        }
    }
}