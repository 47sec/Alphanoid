using UnityEngine;

abstract public class CustomBlockScript : MonoBehaviour
{
    // Called with every collision with block
    public virtual void CollisionUpdate(Collision2D collision) { }
}
