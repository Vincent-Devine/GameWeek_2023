using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutBlockingPlastic : MonoBehaviour
{
    private GameObject ropeBlockingPlastic = null;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlockingPlastic"))
            ropeBlockingPlastic = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (ropeBlockingPlastic && other.CompareTag("BlockingPlastic") && other.gameObject == ropeBlockingPlastic)
            ropeBlockingPlastic = null;
    }

    public void Interaction(InputAction.CallbackContext context)
    {
        if (!ropeBlockingPlastic)
            return;

        animator.SetBool("cut", true);
        GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_cut");

        ropeBlockingPlastic.GetComponent<BlockingPlasticRope>().cut = true;
        StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("cut", false);
    }
}
