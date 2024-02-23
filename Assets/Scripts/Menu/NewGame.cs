using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField] private GameObject transitionFish;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float time;

    private bool startAnimation;

    private float tLerp;

    private void Start()
    {
        transitionFish.transform.position = startPosition;
        tLerp = 0;
        startAnimation = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            startAnimation = true;

        if(startAnimation)
        {
            tLerp += Time.deltaTime;
            transitionFish.transform.position = Vector3.Lerp(startPosition, endPosition, tLerp / time);
            if (transitionFish.transform.position.x >= endPosition.x)
                SceneManager.LoadScene(1);
        }
    }
}
