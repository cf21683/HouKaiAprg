using System;
using Animancer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Run : PlayerGroundedState
{

    private float startTime;
    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
        playerController.Animancer.Play(playerController.CharacterAnimationData.Run, 0.25f);
    }

    public override void Update()
    {
        base.Update();
        Rotation();
    }

    protected override void OnShouldWalkStarted(InputAction.CallbackContext context)
    {
        base.OnShouldWalkStarted(context);
        playerController.SwitchState(PlayerStateList.Walk);
    }
    
    
}
