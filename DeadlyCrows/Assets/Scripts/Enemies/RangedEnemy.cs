using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [field: Header("Ranged")]
    [field: SerializeField] public bool showReticle { get; private set; }
}
