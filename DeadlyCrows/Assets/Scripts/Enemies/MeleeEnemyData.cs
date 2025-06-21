using UnityEngine;

public class MeleeEnemyData : EnemyData
{
    public enum MeleeAttackType { Axe, Katana }

    [field: Header("Melee")]
    [field: SerializeField] public MeleeAttackType MyAttackType { get; private set; }

}
