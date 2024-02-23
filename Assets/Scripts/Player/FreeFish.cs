using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFish : MonoBehaviour
{
    private GameObject trappedFish = null;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TrappedFish"))
            trappedFish = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TrappedFish") && other.gameObject == trappedFish)
            trappedFish = null;
    }

    public void Interaction(InputAction.CallbackContext context)
    {
        if (!trappedFish)
            return;

        trappedFish.GetComponent<FreeTrappedFish>().freeFish = true;
        //trappedFish.SetActive(false);
        animator.SetBool("cut", true);
        GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_cut");

        StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("cut", false);
        GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/BlockedFish_escape");
    }
}
