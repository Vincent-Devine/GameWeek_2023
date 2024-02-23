using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBackground : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float time;

    private float tLerp;

    private void Start()
    {
        transform.position = startPosition;
        tLerp = 0;
    }

    private void FixedUpdate()
    {
        tLerp += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, endPosition, tLerp / time);
        if (transform.position.x <= endPosition.x)
        {
            tLerp = 0;
            transform.position = startPosition;
        }
    }
}
