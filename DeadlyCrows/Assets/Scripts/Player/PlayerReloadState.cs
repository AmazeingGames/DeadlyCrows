using UnityEngine;

public class PlayerReloadState : GroundedState
{
    float reloadTimer;

    public PlayerReloadState(Player player) : base(player) { }

    public override void Enter()
    {
        player.CurrentMoveSpeed = player.PlayerReloadSpeed;

        base.Enter();
    }
    public override void Exit() { }

    public override void FixedUpdate() { }

    public override void HandleInput()
        => base.HandleInput();

    public override void Update()
    {
        reloadTimer -= Time.deltaTime;

        if (reloadTimer < 0)
        {
            reloadTimer = player.GunData.BulletLoadTime;
            player.Reload(1);
        }

        if (player.CurrentBullets == player.GunData.MaxBullets)
            player.PlayerState = Player.WalkState;
    }
}
