using UnityEngine;

public class ReversingBlock : CustomBlockScript
{
    public override void CollisionUpdate(GameObject obj)
    {
        Rigidbody2D collisionRb = obj.transform.GetComponent<Rigidbody2D>();

        collisionRb.velocity = new Vector2(collisionRb.velocity.x * -1, collisionRb.velocity.y);
    }
}
