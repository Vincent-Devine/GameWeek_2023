using UnityEngine;

public class BubbleColumn : MonoBehaviour
{
    private Vector3 positionRaycastHole;
    private Vector3 positionRaycastLimiteDown;
    private Vector3 positionRaycastLimiteUp;
    int layerMaskPlayer;

    [SerializeField] private GameObject gameManager;

    private SpawnPlayers spawnPlayers;

    private Rigidbody2D crabeInZone = null;
    private bool crabeInZonePropulse = false;

    [SerializeField] Vector2 pushForce;

    private Collider2D limitCrab;

    void Start()
    {
        spawnPlayers = gameManager.GetComponent<SpawnPlayers>();
        positionRaycastHole = transform.GetChild(0).transform.position;
        layerMaskPlayer = LayerMask.GetMask("Tortoise");
        limitCrab = GetComponents<Collider2D>()[1];
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitHole = Physics2D.Raycast(positionRaycastHole, Vector2.up, 1000f, layerMaskPlayer);
        Debug.DrawRay(positionRaycastHole, Vector2.up);

        if (hitHole.collider)
        {
            crabeInZonePropulse = spawnPlayers.GetCrab().GetComponent<PlayerMovement>().inBubbleColumn = false;
            limitCrab.enabled = false;
        }
        else if(!hitHole.collider)
        {
            limitCrab.enabled = true;
            if (crabeInZone)
            {
                crabeInZone.AddForce(pushForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject == spawnPlayers.GetCrab())
        {
            crabeInZone = other.gameObject.GetComponent<Rigidbody2D>();
            crabeInZonePropulse = other.gameObject.GetComponent<PlayerMovement>().inBubbleColumn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject == spawnPlayers.GetCrab())
        {
            crabeInZonePropulse = other.gameObject.GetComponent<PlayerMovement>().inBubbleColumn = false;
            crabeInZone = null;
        }
    }
}
