using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    [Header("Properties")]
    [field: SerializeField] public float LockShootDuration { get; private set; }
    public bool IsFlipped => spriteRenderer.flipX;

    [SerializeField] Animator animator;
    [SerializeField] Player player;
    [SerializeField] SpriteRenderer spriteRenderer;

    public readonly int idle = Animator.StringToHash("Player_Idle");
    public readonly int run = Animator.StringToHash("Player_Run");
    public readonly int runShoot = Animator.StringToHash("Player_Run_Shoot");
    public readonly int runGun = Animator.StringToHash("Player_Run_Gun");
    public readonly int hurt = Animator.StringToHash("Player_Hurt");
    public readonly int die = Animator.StringToHash("Player_Die");
    public readonly int roll = Animator.StringToHash("Player_Roll");
    public readonly int shoot = Animator.StringToHash("Player_Shoot");
    public readonly int walk = Animator.StringToHash("Player_Walk");
    public readonly int crouch = Animator.StringToHash("Player_Crouch");

    float lockState;
    public void PlayAnimation(int animation, float lockState = 0)
    {
        if (this.lockState > 0 && lockState == 0)
            return;

        this.lockState = lockState;
        animator.CrossFade(animation, 0, 0);
    }

    public void Update()
    {
        lockState -= Time.deltaTime;

        if (Mouse.GetPosition().x < player.transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
