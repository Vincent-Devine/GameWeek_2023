using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float playerSpeed;
    private float move;
    private bool isClimbing = false;
    private bool isGrabbing = false;
    [SerializeField] float playerSpeedInAir;
    private Grab hasGrab = null;
    public bool inBubbleColumn = false;

    private float speedDefault;

    [SerializeField] private Animator animator = null;
    public float raycastDistance = 0.1f; // The length of the ray to cast
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasGrab = rb.GetComponent<Grab>();
        speedDefault = playerSpeed;
    }

    private void FixedUpdate()
    {
        if (isClimbing || isGrabbing || inBubbleColumn)
        {
            if (animator)
            {
                animator.SetBool("onMove", false);
            }
            return;
        }

        if (rb.velocity.x <= -0.2f)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        if (animator)
        {
            if (rb.velocity.x <= -0.2f || rb.velocity.x >= 0.2f)
            {
                animator.SetBool("onMove", true);
                if (rb.mass < 100 && isGrounded && move != 0)
                {
                    GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_walkRock");
                }
                else if(isGrounded && move != 0)
                {
                    //StartCoroutine(WalkSoundFinished());
                    //StartCoroutine(WalkSoundFinished());

                }
            }
            else
            {
                animator.SetBool("onMove", false);
            }
        }


        float horizontalMouvement = move * Time.deltaTime;
        rb.velocity = new Vector2(horizontalMouvement, rb.velocity.y);
    }
    private void Update()
    {
        isGrounded = IsPlayerGrounded();
        if(isGrounded)
        {
            SetPlayerSpeed(playerSpeed);
        }
        else
        {
            SetPlayerSpeed(playerSpeedInAir);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.action.ReadValue<Vector2>().x * playerSpeed;
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
        if(!hasGrab) 
            return;
        else if (isGrabbing && !hasGrab.GetGrabItem())
            isGrabbing = false;
        else if (!hasGrab.GetGrabItem())
            return;
        else if (context.started)
        {
            isGrabbing = true;
        }
        if (context.canceled)
        {
            isGrabbing = false;
        }
    }

    public void SetPlayerSpeed(float speedLimited)
    {
        playerSpeed = speedLimited;
    }

    public void ResetSpeed()
    {
        playerSpeed = speedDefault;
    }

    bool IsPlayerGrounded()
    {
        // Cast a ray downward from the center of the player
        Vector2 rayOrigin = transform.position;
        rayOrigin.y -= GetComponent<Collider2D>().bounds.extents.y; // Adjust for collider size

        // Cast the ray and check for a collision with the ground layer (layer mask)
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance);

        if (hit.collider != null)
        {
            return true; // The player is grounded
        }
        else
        {
            return false; // The player is not grounded
        }
    }

    //IEnumerator WalkSoundFinished()
    //{
    //    GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Turtle_walkSand");
    //    yield return new WaitForSeconds(4f);
    //}

}
