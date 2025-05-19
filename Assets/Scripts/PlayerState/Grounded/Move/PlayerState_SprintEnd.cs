using UnityEngine;
using Animancer;

public class PlayerState_SprintEnd : PlayerGroundedState
{
    private AnimancerState _sprintEnd;
    public override void Enter()
    {
        base.Enter();
        _sprintEnd = playerController.Animancer.Play(playerController.characterModel.characterAnimationData.SprintEnd,0.25f);

        if (_sprintEnd.Events(this, out AnimancerEvent.Sequence events))
        {
            events.OnEnd = () =>
            {
                playerController.SwitchState(PlayerStateList.Idle);
            };
        }
    }
    

    protected override void AddInputActionsCallbacks()
    {
        playerController.playerInputActions.Character.Fire.started += OnFireStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        playerController.playerInputActions.Character.Fire.started -= OnFireStarted;
        
    }
}
