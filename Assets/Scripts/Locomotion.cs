using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // 
public class Locomotion : MonoBehaviour
{
    /* Properties */
    [Header("Movement")]
    public float moveSpeed = 7.0f;
    public float acceleration = 40.0f;
    [Header("Gravity")]
    public float jumpForce = 13.0f;
    public float downwardAcceleration = 20.0f;
    [Header("Ground Detection")]
    public Vector2 checkAreaScale = new(0.9f, 0.25f);
    public LayerMask groundLayers;

    /* State */
    [HideInInspector] public bool isGrounded;

    /* References */
    [HideInInspector] public Rigidbody2D rb;
    private float directionInput;

    public void Move(float direction)
    {
        directionInput = direction;
    }

    public void Jump()
    {
        if (!isGrounded)
            return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = CheckGrounded();
    }

    /** Always a good idea to *add* to the current velocity instead of setting it.
     * That way if we want to add player knockback, we can just use AddForce.
     */
    private void FixedUpdate()
    {
        /* == Horizontal Movement == */

        float desiredVelocity = directionInput * moveSpeed;

        float velocityDifference = desiredVelocity - rb.velocity.x;

        rb.AddForce(Vector2.right * (velocityDifference * acceleration), ForceMode2D.Force);

        /* == Falling velocity == */
        if (!isGrounded)
        {
            rb.AddForce(Vector2.down * downwardAcceleration, ForceMode2D.Force);
        }
    }
    

    private bool CheckGrounded() =>
        (bool)Physics2D.OverlapBox(GetGroundCheckPosition(), checkAreaScale, 0, groundLayers);
    
    
    private Vector2 GetGroundCheckPosition() =>
        transform.position + (Vector3.down * (checkAreaScale.y/2.0f));


    



    // === Debug Graphics
    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;

        Gizmos.DrawWireCube(GetGroundCheckPosition(), checkAreaScale);
    }
}
