using UnityEngine;
using System;

public class StateMachineManager : SingleBase<StateMachineManager>
{
    public Action updatAction;
    
    public Action fixedUpdatAction;
    
    public Action lateUpdatAction;

    
    public void AddUpdateAction(Action task)
    {
        updatAction += task;
    }

    
    public void RemoveUpdateAction(Action task)
    {
        updatAction -= task;
    }

    
    public void AddFixedUpdateAction(Action task)
    {
        fixedUpdatAction += task;
    }

    
    public void RemoveFixedUpdateAction(Action task)
    {
        fixedUpdatAction -= task;
    }

    
    public void AddLateUpdateAction(Action task)
    {
        lateUpdatAction += task;
    }

    
    public void RemoveLateUpdateAction(Action task)
    {
        lateUpdatAction -= task;
    }

    void Update()
    {
        updatAction?.Invoke();
    }

    void FixedUpdate()
    {
        fixedUpdatAction?.Invoke();
    }
    void LateUpdate()
    {
        lateUpdatAction?.Invoke();
    }
}
