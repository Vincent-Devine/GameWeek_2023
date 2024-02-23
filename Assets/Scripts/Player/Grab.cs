using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour
{
    private List<GameObject> gameObjectsInZone = new List<GameObject>();
    private GameObject blocGrab = null;
    [SerializeField] private Animator animator;

    private void GrabObject()
    {
        blocGrab = gameObjectsInZone[0];
        blocGrab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        blocGrab.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        blocGrab.GetComponent<Rigidbody2D>().drag = 800;
        HingeJoint2D hjBlocGrab = blocGrab.GetComponent<HingeJoint2D>();
        hjBlocGrab.enabled = true;
        hjBlocGrab.connectedBody = this.GetComponent<Rigidbody2D>();
        animator.SetBool("grab", true);
        GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_catch");

    }

    private void UnGrabObject()
    {
        blocGrab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        HingeJoint2D hjBlocGrab = blocGrab.GetComponent<HingeJoint2D>();
        hjBlocGrab.connectedBody = null;
        hjBlocGrab.enabled = false;
        blocGrab = null;
        animator.SetBool("grab", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MovableBloc"))
        {
            gameObjectsInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (blocGrab)
            return;

        if (other.CompareTag("MovableBloc"))
        {
            foreach (GameObject obj in gameObjectsInZone)
            {
                if(other.gameObject == obj)
                {
                    gameObjectsInZone.Remove(obj);
                    return;
                }
            }   
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (gameObjectsInZone.Count == 0)
            return;

        if (context.started)
        {
            GrabObject();
        }
        if (context.canceled)
        {
            gameObjectsInZone.Remove(blocGrab);
            UnGrabObject();
        }
    }

    public GameObject GetGrabItem()
    {
        return blocGrab;
    }
}
