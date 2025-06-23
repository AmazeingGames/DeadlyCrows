using UnityEngine;

[CreateAssetMenu(fileName = "MeleeEnemyData", menuName = "Scriptable Objects/EnemyData/Melee")]
public class MeleeEnemyData : EnemyData
{
    public enum MeleeAttackType { Axe, Katana }

    [field: Header("Melee")]
    [field: SerializeField] public MeleeAttackType MyAttackType { get; private set; }

}
