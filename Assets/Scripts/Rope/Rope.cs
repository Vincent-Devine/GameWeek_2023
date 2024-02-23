using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject prefabRopeSegment;
    [SerializeField] int numLink = 5;

    public void GenerateRope(Rigidbody2D tortoise, Rigidbody2D crabe)
    {
        Rigidbody2D previousSegment = tortoise;
        for (int i = 0; i < numLink; i++)
        {
            GameObject newSegment = Instantiate(prefabRopeSegment);
            newSegment.transform.parent = transform;
            newSegment.transform.position = transform.position;
            HingeJoint2D hingeJoint2D = newSegment.GetComponent<HingeJoint2D>();
            hingeJoint2D.connectedBody = previousSegment;
            previousSegment = newSegment.GetComponent<Rigidbody2D>();
        }

        previousSegment.GetComponent<SpriteRenderer>().enabled = false;

        // Set crabe
        crabe.GetComponent<HingeJoint2D>().connectedBody = previousSegment;
        GameObject connectedAbove = crabe.GetComponent<HingeJoint2D>().connectedBody.gameObject;
        float spriteBelow = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
        crabe.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBelow * -1);

        crabe.GetComponent<DistanceJoint2D>().connectedBody = tortoise;
    }
}
