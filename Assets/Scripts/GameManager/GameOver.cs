using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private List<GameObject> playersList;
    private Vector2 lastSavePosition;
    [SerializeField] GameObject gameManager;
    private int life = 5;
    
    [SerializeField] private float durationInvulnerability = 2f;
    private bool invulnerability = false;

    [SerializeField] private Animator animatorCrabe;
    [SerializeField] private Animator animatorTortoise;

    // Start is called before the first frame update
    void Start()
    {
        lastSavePosition = Vector2.zero;
        animatorCrabe = GetComponent<SpawnPlayers>().GetCrab().transform.GetChild(0).GetComponent<Animator>();
        animatorTortoise = GetComponent<SpawnPlayers>().GetTortoise().transform.GetChild(0).GetComponent<Animator>();
        life = 5;
        invulnerability = false;
    }

    public void TakeDamage(int damage = 1)
    {
        if (invulnerability)
            return;

        gameManager.GetComponent<SpawnPlayers>().GetTortoise().GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Crab_landRock");
        life -= damage;
        invulnerability = true;
        StartCoroutine(Invulnerability());
        if (life <= 0 )
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        gameManager.GetComponent<SpawnPlayers>().GetTortoise().GetComponent<PlaySound>().PlaySoundEvent("event:/SFX/Petrolpool");
        animatorCrabe.SetBool("death", true);
        animatorTortoise.SetBool("death", true);
        StartCoroutine(DeathFinish());
        SceneManager.LoadScene(1);
    }

    public void Checkpoint(Vector2 checkpointPosition)
    {
        lastSavePosition = checkpointPosition;
    }

    IEnumerator Invulnerability()
    {
        yield return new WaitForSeconds(durationInvulnerability);
        invulnerability = false;
    }

    IEnumerator DeathFinish()
    {
        yield return new WaitForSeconds(2f);
    }
}
