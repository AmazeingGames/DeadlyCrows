using UnityEngine;

public class PlayerWalkState : GroundedState
{
    public PlayerWalkState(Player player) : base(player) { }


    public override void Enter() 
        => base.Enter();

    public override void Exit() { }

    public override void FixedUpdate() { }

    public override void HandleInput()
    {
        if (Input.GetButtonDown("Reload"))
            player.PlayerState = Player.ReloadState;
        else
            base.HandleInput();
    }

    public override void Update()
        => player.CurrentMoveSpeed = player.WalkMoveSpeed;
}
