using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float bumperForceCrab;
    [SerializeField] float bumperForceTortoise;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject globalSoundManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject == gameManager.GetComponent<SpawnPlayers>().GetTortoise())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up.normalized * bumperForceTortoise, ForceMode2D.Impulse);
                globalSoundManager.GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Medusa_bump");
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up.normalized * bumperForceCrab, ForceMode2D.Impulse);
                globalSoundManager.GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Medusa_bump");

            }
        }
    }
}
