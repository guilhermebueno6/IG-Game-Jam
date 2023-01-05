using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public float playerWidth;

    public PhysicsMaterial2D bounceMat, normalMat;
    public bool canJump = true;
    public float jumpValue = 0.0f;
    public float maxJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f),
        new Vector2(playerWidth, 0.1f), 0f, groundMask);

        if(isGrounded)
        {
            Move();
        }

        Jump();

        if(isGrounded)
        {
            rb.sharedMaterial = normalMat;
        }else
        {
            rb.sharedMaterial = bounceMat;
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f), new Vector2(playerWidth, 0.1f));
    }

    void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }

        if(Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

    }

    void Jump()
    {
        if(Input.GetKey("space") && isGrounded && canJump)
        {
            if(jumpValue < maxJump)
            {
                jumpValue += 0.05f;
            }
            
        }

        if(Input.GetKeyUp("space"))
        {
            if(isGrounded)
            {
                float tempx = moveInput * walkSpeed;
                float tempy = jumpValue;
                rb.velocity = new Vector2(tempx, tempy);
                jumpValue = 0;
            }
            canJump = true;
            
        }
    }
}
