using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float playerReloadingSpeed;

    [Header("Roll")]
    [SerializeField] float rollDuration;
    [SerializeField] float rollCooldown;
    [SerializeField] float rollStartingSpeed;
    [SerializeField] float rollMaxSpeed;
    [SerializeField] bool addMathCurve;
    // [SerializeField] AnimationCurve rollCurve;

    [Header("Gun")]
    [field: SerializeField] public GunData GunData { get; private set; }

    [Header("Components")]
    [SerializeField] Bullet bullet;
    [SerializeField] Rigidbody2D rigidbody;

    public enum PlayerState { None, Moving, Reloading, Rolling }

    PlayerState myState = PlayerState.None;
    PlayerState myStateLastFrame;
    bool changedStateLastFrame;
    
    float reloadTimer;
    
    float rollDurationTimer;
    float rollCooldownTimer;

    private float currentMoveSpeed;
    private Vector2 movementInput;

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

    public static EventHandler<CurrentBulletsChangedEventArgs> CurrentBulletsChangedEventHandler;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myState = PlayerState.None;
        myState = PlayerState.Moving;
    }

    // Update is called once per frame
    void Update()
    {
        if (changedStateLastFrame)
        {
            myStateLastFrame = myState;
            changedStateLastFrame = false;
        }

        if (myStateLastFrame != myState)
            changedStateLastFrame = true;

        rollCooldownTimer -= Time.deltaTime;

        switch (myState)
        {
            case PlayerState.Moving:
                
                currentMoveSpeed = playerMoveSpeed;

                if (Input.GetButtonDown("Fire1"))
                    Shoot();

                if (Input.GetButtonDown("Roll") && rollCooldownTimer <= 0)
                    myState = PlayerState.Rolling;

                if (Input.GetButtonDown("Reload"))
                    myState = PlayerState.Reloading;
                break;

            case PlayerState.Reloading:
                if (changedStateLastFrame)
                {
                    currentMoveSpeed = playerReloadingSpeed;
                    reloadTimer = 0;
                }

                reloadTimer -= Time.deltaTime;

                if (reloadTimer < 0)
                {
                    reloadTimer = GunData.BulletLoadTime;
                    Reload(1);   
                }

                if (CurrentBullets == GunData.MaxBullets)
                    myState = PlayerState.Moving;

                if (Input.GetButtonDown("Fire1"))
                {
                    myState = PlayerState.Moving;
                    Shoot();
                }

                if (Input.GetButtonDown("Roll") && rollCooldownTimer <= 0)
                    myState = PlayerState.Rolling;
                break;

            case PlayerState.Rolling:
                if (changedStateLastFrame)
                {
                    rollDurationTimer = 0;
                    rollCooldownTimer = rollCooldown;
                }

                rollDurationTimer += Time.deltaTime;
                float t = rollDurationTimer / rollDuration;

                if (addMathCurve)
                    t = (float)(t < 0.5 ? 16 * t * t * t * t * t : 1 - Math.Pow(-2 * t + 2, 5) / 2);

                float speed = Mathf.Lerp(rollStartingSpeed, rollMaxSpeed, t);
                currentMoveSpeed = speed;

                if (rollDurationTimer >= rollDuration)
                    myState = PlayerState.Moving;
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (myState)
        {
            case PlayerState.Moving:
                MovePlayer();
            break;

            case PlayerState.Reloading:
                MovePlayer();
            break;

            case PlayerState.Rolling:
                MovePlayer(false);
            break;
        }
    }


    private void MovePlayer(bool calculateMoveDirection = true)
    {
        if (calculateMoveDirection)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }

        rigidbody.MovePosition(rigidbody.position + movementInput.normalized * currentMoveSpeed * Time.fixedDeltaTime);
        // rigidbody.linearVelocity = (movementInput * currentMoveSpeed);
    }

    void Reload(int amount, bool maxReload = false)
    {
        var newCount = CurrentBullets;

        newCount += amount;
        newCount = Math.Clamp(newCount, 0, GunData.MaxBullets);

        if (maxReload)
            newCount = GunData.MaxBullets;

        CurrentBullets = newCount;
    }

    void Shoot()
    {
        if (CurrentBullets > 0)
        {
            CurrentBullets -= 1;
            Bullet bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletInstance.SetTarget(Mouse.Position);
        }
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

