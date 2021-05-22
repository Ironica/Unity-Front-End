using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;

    private bool isJumping;
    private bool isGrounded;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        float horizontalmv = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
      
        Vector3 target = new Vector2(horizontalmv, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, .05f);

        
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f,jumpForce));
            isJumping = false;
        }
    }
    
}