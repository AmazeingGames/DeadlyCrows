using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] EnemyData enemyData;

    [Header("Components")]
    [SerializeField] protected Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] SpriteRenderer spriteRenderer;

    public enum EnemyState { Chase, Attack }

    EnemyState myState = EnemyState.Chase;
    private float timeTillAttack;
    private EnemyState myStateLastFrame;
    private bool changedStateLastFrame;
    private float distanceFromTarget;

    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    protected virtual void RunStateMachine()
    {
        distanceFromTarget = Vector3.Distance(agent.transform.position, target.position);

        if (changedStateLastFrame)
        {
            myStateLastFrame = myState;
            changedStateLastFrame = false;
        }

        if (myStateLastFrame != myState)
            changedStateLastFrame = true;

        timeTillAttack -= Time.deltaTime;
        agent.speed = enemyData.MovementSpeed;

        switch (myState)
        {
            case EnemyState.Chase:
                agent.isStopped = false;
                agent.destination = target.position;

                if (distanceFromTarget < enemyData.StartAttackDistance)
                    myState = EnemyState.Attack;
            break;

            case EnemyState.Attack:
                agent.isStopped = true;

                if (changedStateLastFrame)
                    timeTillAttack = enemyData.AttackWindup;

                if (timeTillAttack < 0)
                    Attack();

                if (distanceFromTarget >= enemyData.StartChaseDistance)
                    myState = EnemyState.Chase;
            break;
        }
    }

    protected virtual void Attack() 
    {
        timeTillAttack = enemyData.AttackRate;
    }
}
