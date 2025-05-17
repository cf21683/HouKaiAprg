using System;
using System.Collections.Generic;
using UnityEngine;


public interface IStateMachineOwner { }


public class StateMachine
{
    private IState currentState;
    
    public bool HasState => currentState != null;

    private IStateMachineOwner owner;

    private Dictionary<Type, IState> stateDic = new Dictionary<Type, IState>();

    public StateMachine(IStateMachineOwner owner)
    {
        Init(owner);
        
        if (owner == null)
        {
            Debug.LogError($"{GetType().Name}: PlayerController 强转失败，owner 未初始化！");
        }
    }

    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }


    public void EnterState<T>(bool reLoad = false) where T : IState, new()
    {
        if (HasState && currentState.GetType() == typeof(T) && !reLoad)
            return;

        if (HasState)
            ExitCurrentState();

        currentState = LoadState<T>();
        EnterCurrentState();
    }


    private IState LoadState<T>() where T : IState, new()
    {
        Type type = typeof(T);
        if (!stateDic.TryGetValue(type, out IState state))
        {
            state = new T();
            state.Init(owner);
            stateDic.Add(type, state);
        }
        return state;
    }

    private void EnterCurrentState()
    {
        currentState.Enter();
        StateMachineManager.INSTANCE.AddUpdateAction(currentState.Update);
        StateMachineManager.INSTANCE.AddFixedUpdateAction(currentState.FixedUpdate);
        StateMachineManager.INSTANCE.AddLateUpdateAction(currentState.LateUpdate);
    }

    private void ExitCurrentState()
    {
        currentState.Exit();
        StateMachineManager.INSTANCE.RemoveUpdateAction(currentState.Update);
        StateMachineManager.INSTANCE.RemoveFixedUpdateAction(currentState.FixedUpdate);
        StateMachineManager.INSTANCE.RemoveLateUpdateAction(currentState.LateUpdate);
    }


    public void Clear()
    {
        ExitCurrentState();
        currentState = null;
        foreach (var state in stateDic.Values)
            state.UnInit();

        stateDic.Clear();
    }
}
