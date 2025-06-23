using UnityEngine;

public class EnemyData : ScriptableObject
{
    [field: Header("Stats")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float StartAttackDistance { get; private set; }
    [SerializeField] float startChaseDistance = -1;
    [field: SerializeField] public float AttackRate { get; private set; }
    [SerializeField] float attackWindup = -1;

    [field: Header("Sounds")]
    [field: SerializeField] public float DeathSound { get; private set; }
    [field: SerializeField] public float WindUpSound { get; private set; }
    [field: SerializeField] public float AttackSound { get; private set; }

    public float StartChaseDistance => startChaseDistance == -1 ? StartAttackDistance : startChaseDistance;
    public float AttackWindup => attackWindup == -1 ? AttackRate : attackWindup;
}
