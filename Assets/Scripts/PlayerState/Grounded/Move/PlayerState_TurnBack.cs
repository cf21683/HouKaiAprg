using UnityEngine;
using Animancer;
public class PlayerState_TurnBack : PlayerGroundedState
{
    private AnimancerState _turnBack;
    public override void Enter()
    {
        base.Enter();
        _turnBack = playerController.Animancer.Play(playerController.CharacterAnimationData.TurnBack,0.1f);
        _turnBack.NormalizedTime = 0f;
        if (_turnBack.Events(this, out var events))
        {
            events.OnEnd = () =>
            {
                Vector2 input = playerController.playerInputActions.Character.Move.ReadValue<Vector2>();
                playerController.ReusableData.Move = input;
                Debug.Log("转身动画结束，切换到 Sprint");
                playerController.SwitchState(PlayerStateList.Sprint);
            };
        }
    }



    public override void Update()
    {
        
        
    }

}
