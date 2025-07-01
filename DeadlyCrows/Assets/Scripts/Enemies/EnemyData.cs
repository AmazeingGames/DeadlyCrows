using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyData : ScriptableObject
{
    [field: Header("Prototype")]
    [field: SerializeField] public EnemyData Prototype { get; private set; }

    [Header("Movement")]
    [SerializeField] float _movementSpeed = -1;
    [SerializeField] float _acceleration = -1;
    [SerializeField] float _deceleration = -1;

    [Header("Behaviour")]
    [SerializeField] float _startAttackDistance = -1;
    [SerializeField] float _startChaseDistance = -1;

    [Header("Attacks")]
    [SerializeField] float _attackRate = -1;
    [SerializeField] float _attackWindup = -1;

    [Header("Sounds")]
    [SerializeField] float _deathSound;
    [SerializeField] float _windUpSound;
    [SerializeField] float _attackSound;

    public float MovementSpeed
    {
        get => _movementSpeed == -1 ? Prototype.MovementSpeed : _movementSpeed;
        private set => _movementSpeed = value;
    }

    public float Acceleration
    {
        get => _acceleration == -1 ? Prototype.Acceleration : _acceleration;
        private set => _acceleration = value;
    }

    public float Deceleration
    {
        get => _deceleration == -1 ? Prototype.Deceleration : _deceleration;
        private set => _deceleration = value;
    }

    public float StartAttackDistance 
    { 
        get => _startAttackDistance == -1 ? Prototype._startAttackDistance : _startAttackDistance; 
        private set => _startAttackDistance = value; 
    }

    public float AttackRate 
    { 
        get => _attackRate == -1 ? Prototype._attackRate : _attackRate; 
        private set => _attackRate = value; 
    }

    public float StartChaseDistance
    {
        get
        {
            float chaseDistanceToUse;
            if (Prototype == null)
                chaseDistanceToUse = _startChaseDistance == -1 ? StartAttackDistance : _startChaseDistance;
            else
                chaseDistanceToUse = _startChaseDistance == -1 ? Prototype.StartChaseDistance : _startChaseDistance;

            if (chaseDistanceToUse < StartAttackDistance)
            {
                Debug.LogWarning("Start chase distance should not be less than start attack distance");
                return StartAttackDistance;
            }

            return chaseDistanceToUse;
        }
        private set => _startChaseDistance = value;
            
    }
    public float AttackWindup
    {
        get
        {
            if (Prototype == null)
                return _attackWindup == -1 ? AttackRate : _attackWindup;
            return _attackWindup == -1 ? Prototype.AttackWindup : _attackWindup;
        }
        private set => _attackWindup = value;
    }
    public float DeathSound 
    { 
        get => _deathSound == -1 ? Prototype._deathSound : _deathSound; 
        private set => _deathSound = value; 
    }
    public float WindUpSound 
    { 
        get => _windUpSound == -1 ? Prototype._windUpSound : _windUpSound; 
        private set => _windUpSound = value; 
    }
    public float AttackSound 
    { 
        get => _attackSound == -1 ? Prototype._attackSound : _attackSound; 
        private set => _attackSound = value; 
    }

}
