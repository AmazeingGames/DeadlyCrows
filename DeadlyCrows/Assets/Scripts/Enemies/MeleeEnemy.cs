using UnityEngine;

public class MeleeEnemy : Enemy
{
    private void Update()
    {
        RunStateMachine();
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
