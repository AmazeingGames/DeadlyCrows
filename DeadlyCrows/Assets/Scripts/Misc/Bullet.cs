using UnityEngine;

// Because different guns and different enemies would fire bullets with different properties, we should likely consider creating a ScriptableObject containing different data for different kinds of bullets fired from different kinds of objects
public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] CircleCollider2D circleCollider;

    [Header("Properties")]
    [SerializeField] float velocity;
    [SerializeField] float maxDistance;

    Vector2 targetDirection;

    public void SetTarget(Vector2 targetPosition)
    { 
        targetDirection = (targetPosition - rigidbody.position).normalized;
    }

    // I would like to add an easing function where the bullet starts fast and gradually slows to a constant speed
    private void Update()
    {
        rigidbody.linearVelocity = targetDirection * velocity;
    }
}