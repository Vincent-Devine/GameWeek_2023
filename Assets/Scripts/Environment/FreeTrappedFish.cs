using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeTrappedFish : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private float disappearanceFishDurance = 10f;
    [SerializeField] private float speed = -5f;
    public bool freeFish = false;

    BoxCollider2D boxCollider2D;
    Renderer rendererTrappedFish;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rendererTrappedFish = transform.GetChild(0).GetComponent<Renderer>();
    }

    void Update()
    {
        if (!freeFish)
            return;

        time += Time.deltaTime;
        if (time / duration >= 1f)
        {
            boxCollider2D.enabled = false;
            transform.position = new Vector2((transform.position.x + speed * Time.deltaTime), transform.position.y);
            if(time >= disappearanceFishDurance)
                Destroy(gameObject);
        }
        else
        {
            rendererTrappedFish.material.SetFloat("_Dissolve", time / duration);
        }

    }
}
