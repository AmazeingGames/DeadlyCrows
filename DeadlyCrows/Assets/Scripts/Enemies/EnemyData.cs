using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: Header("Stats")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackRate { get; private set; }

    [field: Header("Sounds")]
    [field: SerializeField] public float DeathSound { get; private set; }
    [field: SerializeField] public float WindUpSound { get; private set; }
    [field: SerializeField] public float AttackSound { get; private set; }
}
