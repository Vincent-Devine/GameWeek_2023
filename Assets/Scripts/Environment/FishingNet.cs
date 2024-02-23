using System.Collections;
using UnityEngine;

public class FishingNet : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject gameManager;

    public AnimationCurve curve;

    [SerializeField] private float speed;
    [SerializeField] private float durationShake = 1f;

    private bool isShaking = true;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if(isShaking)
        {
            isShaking = false;
            StartCoroutine(Shaking());
        }

        Vector3 screenPoint = new Vector3(5f, 300f, 0f);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
        if (transform.position.x <= worldPoint.x)
        {
            transform.position = new Vector3(worldPoint.x, worldPoint.y, 0f);
        }
        else
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameManager)
        {
            gameManager.GetComponent<GameOver>().TakeDamage(5);
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;
        while(elapsedTime < durationShake)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / durationShake);
            mainCamera.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        mainCamera.transform.position = startPosition;
    }
}
