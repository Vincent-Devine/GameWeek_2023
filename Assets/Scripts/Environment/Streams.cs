using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Streams : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] float streamPushForceTortoise;
    [SerializeField] float streamPushForceCrab;
    private bool isActivated;
    [SerializeField] GameObject musicManager;
    FMOD.Studio.EventInstance test;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            musicManager.GetComponent<PlayMusic>().SwitchAudioPhase("PHASES",1);
            isActivated = true;
        }
    }

     void Update()
    {
        //Get detail on direction
        if(isActivated)
        {
            gameManager.GetComponent<SpawnPlayers>().GetTortoise().GetComponent<Rigidbody2D>().AddForce(Vector2.right * streamPushForceTortoise, ForceMode2D.Force);
            gameManager.GetComponent<SpawnPlayers>().GetCrab().GetComponent<Rigidbody2D>().AddForce(Vector2.right * streamPushForceCrab, ForceMode2D.Force);
        }

    }

}
