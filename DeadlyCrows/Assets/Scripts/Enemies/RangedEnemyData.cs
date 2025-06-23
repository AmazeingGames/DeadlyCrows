using UnityEngine;

[CreateAssetMenu(fileName = "RangedEnemyData", menuName = "Scriptable Objects/EnemyData/Ranged")]
public class RangedEnemyData : EnemyData
{
    [field: Header("Ranged")]
    [field: SerializeField] public bool showTrajectory { get; private set; }
}
