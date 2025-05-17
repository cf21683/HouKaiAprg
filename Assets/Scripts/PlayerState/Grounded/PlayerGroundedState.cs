using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerState
{


    protected override void AddInputActionsCallbacks(){
        base.AddInputActionsCallbacks();

        playerController.playerInputActions.Character.Move.canceled += OnMoveCanceled;
        playerController.playerInputActions.Character.Dash.started += OnDashStarted;
    }


    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        playerController.playerInputActions.Character.Move.canceled -= OnMoveCanceled;
        playerController.playerInputActions.Character.Dash.started -= OnDashStarted;
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        playerController.SwitchState(PlayerStateList.Idle);
    }

    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {
        playerController.SwitchState(PlayerStateList.Dash_Front);
    }


    protected virtual void Move(){
        if(playerController.ReusableData.ShouldWalk){
            playerController.SwitchState(PlayerStateList.Walk);
            return;
        }

        playerController.SwitchState(PlayerStateList.Run);
    }

    
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }
    
}
