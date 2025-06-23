using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged")]
    [SerializeField] Bullet bullet;
    Rigidbody targetRigidbody;

    private void Start()
    {
        targetRigidbody = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RunStateMachine();
    }

    protected override void Attack()
    {
        base.Attack();

        var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
        bullet.SetTarget(target.transform.position);
        Debug.Log($"Target Position: {target.transform.position}");
        Debug.Log($"Target Rigidbody Position: {targetRigidbody.position}");
    }
}
