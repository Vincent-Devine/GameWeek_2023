using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTransition : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float time;

    private float tLerp;

    private void Awake()
    {
        transform.position = startPosition;
    }

    private void Start()
    {
        tLerp = 0;
    }

    private void FixedUpdate()
    {
        tLerp += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, endPosition, tLerp / time);
        if (transform.position.x >= endPosition.x)
        {
            gameObject.SetActive(false);
        }
    }
}
