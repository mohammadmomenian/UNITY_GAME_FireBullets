using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    [Tooltip("This is a positive integer which speeds up the player movement")]
    public int speedBoost; // set this to 5
    public float jumpSpeed; // set this to 600
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public LayerMask whatIsGround;
    public Transform leftbulletSpawnPos;
    public Transform rightbulletSpawnPos;
    public GameObject leftbullet;
    public GameObject rightbullet;
    public bool leftPressed, rightPressed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    bool isJumping, canDoubleJump;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

        float playerSpeed = Input.GetAxisRaw("Horizontal"); // value will be 1, -1 or 0

        playerSpeed *= speedBoost;

        if (playerSpeed != 0)
            MovePlayer(playerSpeed);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullets();
        }

        ShowFalling();

        if (leftPressed)
            MovePlayer(-speedBoost);

        if (rightPressed)
            MovePlayer(speedBoost);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(feet.position, feetRadius);

        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
    }

    void MovePlayer(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if (playerSpeed < 0)
            sr.flipX = true;
        else if (playerSpeed > 0)
            sr.flipX = false;

        if (!isJumping)
            anim.SetInteger("State", 1);
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (!isJumping)
            anim.SetInteger("State", 0);
    }

    void ShowFalling()
    {
        if (rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
            anim.SetInteger("State", 2);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if (canDoubleJump && !isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
            anim.SetInteger("State", 2);

            canDoubleJump = false;
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void FireBullets()
    {
        if (sr.flipX)
        {
            Instantiate(leftbullet, leftbulletSpawnPos.position, Quaternion.identity);
        }

        if (!sr.flipX) {
            Instantiate(rightbullet, rightbulletSpawnPos.position, Quaternion.identity);
        }
    }

    public void MobileMoveLeft()
    {
        leftPressed = true;
    }

    public void MobileMoveRight()
    {
        rightPressed = true;
    }

    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;

        StopMoving();
    }

    public void MobileFireBullets()
    {
        FireBullets();
    }

    public void MobileJump()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
