public abstract class IState
{
    public abstract void Init(IStateMachineOwner owner);
    public abstract void UnInit();
    
    public abstract void Enter();
    
    public abstract void Exit();

    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();
}
