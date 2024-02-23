using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockingPlastic : MonoBehaviour
{
    private GameObject rope;
    private GameObject plastic;

    public float duration;
    private float timeLerp;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool finish = false;

    private void Start()
    {
        plastic = transform.GetChild(0).gameObject;
        rope = transform.GetChild(1).gameObject;
        startPosition = plastic.transform.position;
        endPosition = new Vector3(plastic.transform.position.x, plastic.GetComponent<BoxCollider2D>().size.y + plastic.transform.position.y);
    }

    private void FixedUpdate()
    {
        if(!rope && !finish)
        {
            timeLerp += Time.deltaTime;
            plastic.transform.position = Vector3.Lerp(startPosition, endPosition, timeLerp / duration);
            if (plastic.transform.position.y >= endPosition.y)
            {
                print("finish");
                finish = true;
            }

        }
    }
}
