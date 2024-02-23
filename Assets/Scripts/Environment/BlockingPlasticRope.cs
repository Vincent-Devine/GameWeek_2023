using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingPlasticRope : MonoBehaviour
{
    public bool cut = false;

    private void Update()
    {
        if(cut)
            StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
