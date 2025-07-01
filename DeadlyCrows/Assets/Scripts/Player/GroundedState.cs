using UnityEngine;

public class GroundedState : PlayerState
{
    public GroundedState(Player player) : base(player) { }

    public override void Enter() 
        => player.CalculateMoveDirection = true;

    public override void Exit() { }

    public override void FixedUpdate() { }
   
    public override void HandleInput()
    {
        if (Input.GetButtonDown("Roll") && player.RollCooldownTimer <= 0)
            player.PlayerState = Player.RollState;

        if (Input.GetButtonDown("Fire1") && player.Shoot())
        {
            if (player.MovementInput.x == 0 && player.MovementInput.z == 0)
                Animator.PlayAnimation(Animator.shoot, Animator.LockShootDuration);
            else
                Animator.PlayAnimation(Animator.runShoot, Animator.LockShootDuration);
        }
    }

    public override void Update() { }

}
