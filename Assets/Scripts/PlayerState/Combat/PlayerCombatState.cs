using UnityEngine;
using Animancer;
public class PlayerCombatState : PlayerState
{
    protected override void AddInputActionsCallbacks()
    {
        
    }

    protected override void RemoveInputActionsCallbacks()
    {
    }

    protected float NormalizedTime(AnimancerState state)
    {
        if (state != null)
            return state.NormalizedTime;
        return 0;
    }
    
}
