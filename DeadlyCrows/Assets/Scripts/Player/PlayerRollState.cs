using UnityEngine;
using System;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player player) : base(player) { }

    public override void Enter()
    {
        player.RollDurationTimer = 0;
        player.RollCooldownTimer = player.RollCooldown;
        player.CalculateMoveDirection = false;

        Animator.PlayAnimation(Animator.roll, -1);
    }

    public override void Exit()
        => player.CalculateMoveDirection = true;

    public override void FixedUpdate() { }

    public override void HandleInput() { }

    public override void Update()
    {
        player.RollDurationTimer += Time.deltaTime;
        float t = player.RollDurationTimer / player.RollDuration;

        if (player.AddMathCurve)
            t = (float)(t < 0.5 ? 16 * t * t * t * t * t : 1 - Math.Pow(-2 * t + 2, 5) / 2);

        float speed = Mathf.Lerp(player.RollStartingSpeed, player.RollMaxSpeed, t);
        player.CurrentMoveSpeed = speed;

        if (player.RollDurationTimer >= player.RollDuration)
            player.PlayerState = Player.WalkState;
    }
}
