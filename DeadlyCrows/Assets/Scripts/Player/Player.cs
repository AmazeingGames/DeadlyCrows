using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Movement Speed")]
    [field: SerializeField] public float WalkMoveSpeed { get; set; }
    [field: SerializeField] public float PlayerReloadSpeed { get; set; }

    [field: Header("Roll")]
    [field: SerializeField] public float RollDuration { get; set; }
    [field: SerializeField] public float RollCooldown { get; set; }
    [field: SerializeField] public float RollStartingSpeed { get; set; }
    [field: SerializeField] public float RollMaxSpeed { get; set; }
    [field: SerializeField] public bool AddMathCurve { get; set; }
    // [SerializeField] AnimationCurve rollCurve;

    [Header("Gun")]
    [field: SerializeField] public GunData GunData { get; private set; }

    [Header("Components")]
    [SerializeField] Rigidbody rigidbody;
    [field: SerializeField] public ObjectAnimator PlayerAnimator { get; private set; }

    [Header("Prefabs")]
    [SerializeField] Bullet bullet;

    public float RollDurationTimer { get; set; }
    public float RollCooldownTimer { get; set; }
    public float TimeSinceLastShot { get; set; }

    public float CurrentMoveSpeed { get; set; }

    Vector3 _movementInput;
    public Vector3 MovementInput { get => _movementInput; private set => _movementInput = value; }

    int propertyCurrentBullets;
    public int CurrentBullets 
    { 
        get => propertyCurrentBullets; 
        private set 
        { 
            var previousBulletCount = propertyCurrentBullets;
            propertyCurrentBullets = value;

            var changeAmount = value - previousBulletCount;

            CurrentBulletsChangedEventHandler?.Invoke(this, new(previousBulletCount, value, changeAmount));
        } 
    }

    public static PlayerRollState RollState { get; private set; }
    public static PlayerReloadState ReloadState { get; private set; }
    public static PlayerWalkState WalkState { get; private set; }

    PlayerState previousState;
    PlayerState _playerState;
    public PlayerState PlayerState 
    { 
        get => _playerState; 
        set
        {
            previousState = _playerState;
            _playerState = value;

            previousState?.Exit();
            value.Enter();
        }
    }

    public static EventHandler<CurrentBulletsChangedEventArgs> CurrentBulletsChangedEventHandler;
    public bool CalculateMoveDirection { get; set; }

    public bool CanStateShoot { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RollState = new(this);
        ReloadState = new(this);
        WalkState = new(this);

        PlayerState = WalkState;
    }

    // Update is called once per frame
    void Update()
    {
        RollCooldownTimer -= Time.deltaTime;
        TimeSinceLastShot += Time.deltaTime;

        PlayerState.HandleInput();
        PlayerState.Update();
    }

    private void FixedUpdate()
    {
        PlayerState.FixedUpdate();

        MovePlayer();
    }

    public void MovePlayer()
    {
        if (CalculateMoveDirection)
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.z = Input.GetAxisRaw("Vertical");
        }

        rigidbody.MovePosition(rigidbody.position + MovementInput.normalized * CurrentMoveSpeed * Time.fixedDeltaTime);
    }

    public void Reload(int amount, bool maxReload = false)
    {
        var newCount = CurrentBullets;

        newCount += amount;
        newCount = Math.Clamp(newCount, 0, GunData.MaxBullets);

        if (maxReload)
            newCount = GunData.MaxBullets;

        CurrentBullets = newCount;
    }

    public bool Shoot()
    {
        if (CurrentBullets > 0 && TimeSinceLastShot >= GunData.CooldownBetweenRounds)
        {
            TimeSinceLastShot = 0;
            CurrentBullets -= 1;
            var bulletOffset = PlayerAnimator.IsFlipped ? new(GunData.BulletOffset.x * -1, 0, GunData.BulletOffset.z) : GunData.BulletOffset;
            Bullet bulletInstance = Instantiate(bullet, transform.position + bulletOffset, Quaternion.identity);
            bulletInstance.SetTarget(Mouse.GetPosition());

            return true;
        }
        return false;
    }
}

public class CurrentBulletsChangedEventArgs : EventArgs
{
    public readonly int previousCount;
    public readonly int remainingBullets;
    public readonly int changeAmount;

    public CurrentBulletsChangedEventArgs(int previousCount, int remainingBullets, int changeAmount)
    {
        this.previousCount = previousCount;
        this.remainingBullets = remainingBullets;
        this.changeAmount = changeAmount;
    }
}

