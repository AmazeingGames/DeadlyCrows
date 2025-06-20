using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [Header("Gun")]
    [field: SerializeField] public int MaxBullets { get; private set; }
    [field: SerializeField] public float CooldownBetweenRounds { get; private set; }
    [field: SerializeField] public float BulletLoadTime { get; private set; }

}
