using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Because different guns and different enemies would fire bullets with different properties, we should likely consider creating a ScriptableObject containing different data for different kinds of bullets fired from different kinds of objects
public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] SphereCollider circleCollider;

    [Header("Properties")]
    [SerializeField] float velocity;
    [SerializeField] float maxDistance;
    
    Vector3 targetPosition;
    Vector3 targetDirection;

    public void SetTarget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        targetDirection = (targetPosition - rigidbody.position).normalized;

        // Debug.Log($"Bullet | Target Position: {this.targetPosition}");
        // Debug.Log($"Bullet | Target Direction: {targetDirection}");
    }

    // I would like to add an easing function where the bullet starts fast and gradually slows to a constant speed
    private void Update()
    {
        // Debug.Log($"Bullet | Target Position: {this.targetPosition}");
        // Debug.Log($"Bullet | Target Direction: {targetDirection}");
        rigidbody.linearVelocity = targetDirection * velocity;
    }
}