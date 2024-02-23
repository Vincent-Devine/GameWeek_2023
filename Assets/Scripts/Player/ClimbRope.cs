using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbRope : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D nextSegmentRope;
    private Rigidbody2D firstSegmentRope;

    private bool isClimbing = false;
    [SerializeField] float speedClinb;
    [SerializeField] float impulseUp;
    [SerializeField] Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firstSegmentRope = nextSegmentRope = GetComponent<HingeJoint2D>().connectedBody;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (!isClimbing)
        {
            nextSegmentRope = firstSegmentRope;
            return;
        }

        rb.AddForce((nextSegmentRope.transform.position - transform.position) * speedClinb + new Vector3(0,0,impulseUp), ForceMode2D.Impulse);
        if (nextSegmentRope.GetComponent<HingeJoint2D>() && Vector3.Distance(transform.position, nextSegmentRope.transform.position) <= 1f)
        {
            nextSegmentRope = nextSegmentRope.GetComponent<HingeJoint2D>().connectedBody;
            GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_climb");
        }
    }

    public void OnClimbRope(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetBool("climb", true);
            isClimbing = true;
        }
        if (context.canceled)
        {
            animator.SetBool("climb", false);
            isClimbing = false;
        }
    }
}
