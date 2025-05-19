using Animancer;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerState_Walk : PlayerGroundedState
{
    private AnimancerState _state;
     public override void Enter()
    {
        base.Enter();
        _state = playerController.Animancer.Play(playerController.CharacterAnimationData.Walk, 0.25f);
        // _state.ApplyFootIK = true;
    }

    public override void Update()
    {
        base.Update();
        Rotation();
    }

    protected override void OnShouldWalkStarted(InputAction.CallbackContext context)
    {
        base.OnShouldWalkStarted(context);
        playerController.SwitchState(PlayerStateList.Run);
    }
    
    
}
