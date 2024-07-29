using UnityEngine;

public class ReversingBlock : CustomBlockScript
{
    public override void CollisionUpdate(Collision2D collision)
    {
        Rigidbody2D collisionRb = collision.transform.GetComponent<Rigidbody2D>();

        collisionRb.velocity = new Vector2(collisionRb.velocity.x * -1, collisionRb.velocity.y);
    }
}
