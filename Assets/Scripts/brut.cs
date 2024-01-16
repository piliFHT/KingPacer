using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class brut : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool AirTime = false;

    // Adjust the player's movement speed
    // Adjust the jump force
    public SpriteRenderer sr;
    public Transform tr;
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI healthText;
    public Vector2 respawnPoint;
    public Vector2 PlyPos;
    public GameManager gameManager;
    public TimeManager timeManager;
    public Animator animator;
    public int i = 0;
    bool jumping;
    public GameObject gameWon;
    public GameObject portal;

    [System.NonSerialized]
    public float jumpAmount = 13,
        gravityScale = 3,
        fallGravityScale = 8,
        playerSpeed = 5.0f,
        jumpHeight = 4.3f,
        buttonPressedTime,
        buttonPressWindow = 0.2f,
        timerBooster = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameWon.SetActive(false);
        portal.SetActive(false);
        AirTime = false;
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        respawnPoint = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeManager.IsGamePaused)
        {
            // Player controls
            float horizontalInput = 0.0f;
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("Walk", true);
                sr.flipX = true;
                horizontalInput += -1.5f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("Walk", true);
                sr.flipX = false;
                horizontalInput += 1.5f;
                // Move right when "D" is pressed
            }
            else
            {
                animator.SetBool("Walk", false);
            }

            if (
                Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.D) && !AirTime
                || Input.GetKey(KeyCode.C)
                    && Input.GetKey(KeyCode.A)
                    && !AirTime
            )
            {
                animator.SetBool("Run", true);
                playerSpeed = 10.0f;
                //Debug.Log("Penis");
            }
            else
            {
                animator.SetBool("Run", false);
                playerSpeed = 5.0f;
                // Debug.Log("prdel");
            }

            rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
            // Modify the player's velocity to control movement



            if (Input.GetKeyDown(KeyCode.Space) && !AirTime)
            {
                rb.gravityScale = gravityScale;
                float jumpForce =
                    Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                AirTime = true;
                jumping = true;
                buttonPressedTime = 0;

                animator.SetBool("JumpAsc", true);
                animator.SetBool("JumpDesc", false);
            }
            if (jumping)
            {
                buttonPressedTime += Time.deltaTime;
                if (buttonPressedTime < buttonPressWindow && Input.GetKeyUp(KeyCode.Space))
                {
                    rb.gravityScale = fallGravityScale;
                }
            }

            if (rb.velocity.y < 0)
            {
                animator.SetBool("JumpDesc", true);
                animator.SetBool("JumpAsc", false);
                rb.gravityScale = fallGravityScale;
                jumping = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform")
        {
            AirTime = false;
            animator.SetBool("JumpDesc", false);
        }

        if (other.gameObject.tag == "Enemy")
        {
            gameManager.ApplyDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            i++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SuperCoin")
        {
            i += 5;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Booster")
        {
            jumpHeight = jumpHeight + 5;
            Destroy(other.gameObject);
        }

        tmp.text = $"Coins: {i}";

        if(i >= 9){
            portal.SetActive(true);
            Debug.Log("Máš hotovo!");
            Invoke("Delay", 1);
            
        }
        if(other.gameObject.tag == "Portal")
        {
            timeManager.IsGamePaused = true;
            gameWon.SetActive(true);
            Time.timeScale = 0f;
        }

        
    }

    void Delay()
    {
        animator.SetInteger("Coins", 9); 
    }
}
