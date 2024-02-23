using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingNetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject fishingNet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            fishingNet.SetActive(true);
    }
}
