using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour
{
    /* Properties */
    public Player player;
    [Header("Configuration")] 
    public float velocityThreshold = 2.0f;

    /* References */
    private Animator controller;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        controller = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            controller.SetTrigger("Jumped");


        controller.SetBool("isGrounded", player.locomotion.isGrounded);
        controller.SetBool("isFalling", player.locomotion.rb.velocity.y < 0);


        bool isMoving = IsMoving();
        controller.SetBool("isRunning", isMoving);

        if (isMoving)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") < 0;
    }

    private bool IsMoving()
    {
        // We don't want the player run animation to play when the player
        // has been knocked back by something but isn't pressing inputs.

        bool velocityCheck = player.locomotion.rb.velocity.sqrMagnitude > velocityThreshold * velocityThreshold;
        bool inputCheck = Input.GetAxisRaw("Horizontal") != 0;

        return velocityCheck && inputCheck;
    }
}
