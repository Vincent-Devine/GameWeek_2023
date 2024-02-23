using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class SpawnPlayers : MonoBehaviour
{
    PlayerInput tortoise;
    [SerializeField] GameObject prefabTortoise;
    [SerializeField] Transform transformTortoise;
    PlayerInput crab;
    [SerializeField] GameObject prefabCrab;
    [SerializeField] Transform transformCrab;
    [SerializeField] GameObject rope;
    [SerializeField] GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        tortoise = PlayerInput.Instantiate(prefabTortoise, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        crab = PlayerInput.Instantiate(prefabCrab, controlScheme: "Keyboard", pairWithDevice: Keyboard.current);

        rope.transform.position = tortoise.transform.position;
        rope.GetComponent<Rope>().GenerateRope(tortoise.gameObject.GetComponent<Rigidbody2D>(), crab.gameObject.GetComponent<Rigidbody2D>());
        SetUpCamera();
    }

    public GameObject GetTortoise()
    {
        return tortoise.gameObject;
    }

    public GameObject GetCrab()
    {
        return crab.gameObject;
    }

    private void SetUpCamera()
    {
        MoveCamera moveCamera = mainCamera.GetComponent<MoveCamera>();
        moveCamera.tortoise = tortoise.gameObject;
        moveCamera.crab = crab.gameObject;

    }
}
