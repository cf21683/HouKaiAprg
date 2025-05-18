using UnityEngine;
using Animancer;
using UnityEngine.InputSystem;
using System;

public class PlayerState_Dash : PlayerGroundedState
{
    private AnimancerState _dashState;
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("播放dash");
        switch (characterModel.currentState)
        {
            case PlayerStateList.Dash_Front:
                _dashState = playerController.Animancer.Play(playerController.CharacterAnimationData.DashFront, 0.25f);
                break;
            case PlayerStateList.Dash_Back:
                _dashState = playerController.Animancer.Play(playerController.CharacterAnimationData.DashBack, 0.25f);
                break;
        }
        _dashState.NormalizedTime = 0f;
        if (_dashState.Events(this, out AnimancerEvent.Sequence events))
        {
            events.Add(0.5f, DashMove);
        }
    }
    

    private void DashMove()
    {
        playerController.ReusableData.Move = playerController.playerInputActions.Character.Move.ReadValue<Vector2>();
            if (playerController.ReusableData.Move != Vector2.zero)
            {
                switch (characterModel.currentState)
                {
                    case PlayerStateList.Dash_Front:
                        playerController.SwitchState(PlayerStateList.Sprint);
                        break;
                    case PlayerStateList.Dash_Back:
                        playerController.SwitchState(PlayerStateList.Run);
                        break;
                }
                return;
            }
            else
            {
                playerController.SwitchState(PlayerStateList.Idle);
            }

        
    }

    #region 禁用父级的按键监听

    protected override void AddInputActionsCallbacks() { }
    protected override void RemoveInputActionsCallbacks() { }

    #endregion
    



}
