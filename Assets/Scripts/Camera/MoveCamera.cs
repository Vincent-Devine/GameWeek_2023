using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    public GameObject tortoise;
    public GameObject crab;
    Camera cam;
    private Vector3 midpoint;
    private Vector3 cameraDestination;
    float offsetY;
    [SerializeField] float followTimeDelta;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        offsetY = 10f;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager)
        {
            midpoint = (gameManager.GetComponent<SpawnPlayers>().GetTortoise().transform.position + gameManager.GetComponent<SpawnPlayers>().GetCrab().transform.position) / 2f;
            midpoint.y = (gameManager.GetComponent<SpawnPlayers>().GetTortoise().transform.position.y + gameManager.GetComponent<SpawnPlayers>().GetCrab().transform.position.y + 6.5f) / 2f;
            cameraDestination = midpoint - cam.transform.forward ;

            cameraDestination.z = -10f;

            cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);
        }
        
    }

}
