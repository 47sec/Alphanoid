using UnityEngine;

abstract public class CustomBlockScript : MonoBehaviour
{
    // Called with every hit
    public virtual void CollisionUpdate(GameObject obj) { }
}
