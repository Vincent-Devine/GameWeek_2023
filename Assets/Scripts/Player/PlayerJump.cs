using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float playerJumpForce;
    [SerializeField] int maxJump;
    private int currentJump;
    [SerializeField] float underwaterGravity = -2f;
    private bool isClimbing = false;
    private bool isGrabbing = false;

    private Grab hasGrab = null;

    [SerializeField] private Animator animator = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasGrab = rb.GetComponent<Grab>();
        currentJump = 0;
        Physics2D.gravity = new Vector2(0f, underwaterGravity);
    }

    private void Update()
    {
        //if (animator)
        //animator.SetBool("onJump", false);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isClimbing || isGrabbing)
            return;

        if (context.started) 
        {
            currentJump++;
        }
        if (currentJump <= maxJump && rb)
        {
            if(animator)
            {
                animator.SetBool("onJump", true);
                StartCoroutine(StopAnimation());
                if (rb.mass < 100)
                {
                    GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_jump");
                }
                else
                {
                    GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Turtle_jump");
                }
            }
            rb.AddForce(transform.up * playerJumpForce, ForceMode2D.Impulse);
            //GetComponent<PlayerMovement>().SetPlayerSpeed(playerSpeedInAir);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            currentJump = 0;
            //GetComponent<PlayerMovement>().ResetSpeed();
        }
    }

    public void OnClimbRope(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isClimbing = !isClimbing;
        }
        if (context.canceled)
        {
            isClimbing = !isClimbing;
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (!hasGrab)
            return;
        else if (isGrabbing && !hasGrab.GetGrabItem())
            isGrabbing = false;
        else if (!hasGrab.GetGrabItem())
            return;
        else if (context.started)
        {
            print(hasGrab.GetGrabItem());
            isGrabbing = true;
        }
        if (context.canceled)
        {
            isGrabbing = false;
        }
    }


    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(.2f);
        animator.SetBool("onJump", false);
    }
}
