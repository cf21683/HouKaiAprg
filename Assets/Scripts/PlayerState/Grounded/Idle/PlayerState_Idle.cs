using Animancer;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerState_Idle : PlayerGroundedState
{
    private float _idleTime;
    private AnimancerState _state;

    public override void Enter()
    {
        base.Enter();
        _idleTime = 0f;
        playerController.ReusableData.ComboNumber = 1;
        switch (characterModel.currentState)
        {
            case PlayerStateList.Idle:
                _state = playerController.Animancer.Play(playerController.CharacterAnimationData.Idle, 0.25f);
                break;
            case PlayerStateList.Idle_AFK:
                _state = playerController.Animancer.Play(playerController.CharacterAnimationData.IdleAFK, 0.25f);
                break;
            case PlayerStateList.CombatIdle:
                _state = playerController.Animancer.Play(playerController.CharacterAnimationData.CombatIdle, 0.5f);
                break;
        }
        
    }

    public override void Update()
    {
        base.Update();
        Idle();
    }
    public override void FixedUpdate(){
        base.FixedUpdate();
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        // playerStateMachine.SwitchState(typeof(PlayerState_IdleJump));
    }
    
    private void Idle(){

        if (playerController.ReusableData.Move == Vector2.zero && _idleTime <= 3f)
        {
            _idleTime += Time.deltaTime;
            return;
        }

        if (playerController.ReusableData.Move != Vector2.zero)
        {
            Move();
            return;
        }

        switch (characterModel.currentState)
        {
            case PlayerStateList.Idle:
                if (_idleTime > 3) 
                {
                    //切换到挂机状态
                    _idleTime = 0;
                    playerController.SwitchState(PlayerStateList.Idle_AFK);
                    return;
                }
                break;
            case PlayerStateList.Idle_AFK:
                
                if (_state.Events(this, out var events))
                {
                    events.OnEnd = () =>
                    {
                        playerController.SwitchState(PlayerStateList.Idle);
                    };
                }
                break;
        }

    }
    
    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
        playerController.SwitchState(PlayerStateList.Dash_Back);
    }

    
    
    
}
