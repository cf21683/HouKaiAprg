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
        _dashState = playerController.Animancer.Play(playerController.CharacterAnimationData.DashBack, 0.25f);
        _dashState.NormalizedTime = 0f;
        if (_dashState.Events(this, out AnimancerEvent.Sequence events))
        {
            events.Add(0.7f, DashMove);
        }
    }
    

    private void DashMove()
    {
        
            if (playerController.ReusableData.Move != Vector2.zero)
            {
                Move();
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
