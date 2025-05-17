using System.Collections.Generic;
using UnityEngine;

public class StateMachineRemove : MonoBehaviour
{
    IState currentState;
    protected Dictionary<System.Type, IState> stateTable;
    public IState CurrentState => currentState;
    
    void Update()
    {
        currentState.Update();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    protected void SwitchOn (IState newState)
    {
        currentState = newState;
        currentState.Enter();
        
    }

    public void SwitchState(IState newState){
        currentState.Exit();
        SwitchOn(newState);
    }
    
    public void SwitchState(System.Type newStateType){
        SwitchState(stateTable[newStateType]);
    }
}
