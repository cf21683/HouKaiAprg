using System.Collections;
using Animancer;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerController : SingleBase<PlayerController>,IStateMachineOwner
{
    Vector3 _walk;
    public PlayerInputActions playerInputActions { get; private set; }
    
    private float dashTimer = 1;
    private StateMachine stateMachine;
    private CharacterController characterController;
    private AnimancerComponent animancer;
    
    public StateMachine StateMachine => stateMachine;
    public AnimancerComponent Animancer => animancer;
    // [SerializeField] private PlayerSO data;
    // public PlayerSO PlayerData => data;

    public Transform MainCamera {get; private set;}

    public CharacterModel characterModel;
    public PlayerReusableData ReusableData{get;set;}

    [SerializeField ] private CharacterAnimationData characterAnimationData;
    public CharacterAnimationData CharacterAnimationData => characterAnimationData;
    void Awake()
    {
        stateMachine = new StateMachine(this);
        ReusableData = new PlayerReusableData();
        playerInputActions = new PlayerInputActions();
        characterModel = GetComponentInChildren<CharacterModel>();
        animancer = GetComponentInChildren<AnimancerComponent>();
        characterController = GetComponentInChildren<CharacterController>();
        characterAnimationData = characterModel.characterAnimationData;

        MainCamera = Camera.main.transform;
        ReusableData.TimeToReachTargetRotation.y = 0.14f;
    }

    void Start()
    {
        
        playerInputActions.Character.Enable();
        SwitchState(PlayerStateList.Idle);
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
     public void SwitchState(PlayerStateList playerState)
        {
            characterModel.currentState = playerState;
            switch (playerState)
            {
                case PlayerStateList.Idle:
                case PlayerStateList.Idle_AFK:
                    stateMachine.EnterState<PlayerState_Idle>(true);
                    break;
                case PlayerStateList.Walk:
                    stateMachine.EnterState<PlayerState_Walk>(true);
                    break;
                case PlayerStateList.Run:
                    stateMachine.EnterState<PlayerState_Run>(true);
                    break;

                case PlayerStateList.Dash_Front:
                case PlayerStateList.Dash_Back:
                    if (dashTimer !=1)
                    {
                        return;
                    }
                    stateMachine.EnterState<PlayerState_Dash>();
                    dashTimer = 0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerState), playerState, null);
            }
        }
    
    
    
    
    
    void OnAnimatorMove()
    {
        if (animancer.Animator && characterController.enabled)
        {
            Vector3 rootMotion = animancer.Animator.deltaPosition;

        
            rootMotion.y += _walk.y * Time.deltaTime;

            characterController.Move(rootMotion);
        }
    }

    void DisableActionFor(InputAction action, float seconds){
        StartCoroutine(DisableAction(action,seconds));
    }

    private IEnumerator DisableAction(InputAction action, float seconds){
        action.Disable();
        yield return new WaitForSeconds(seconds);
        action.Enable();
    }


    private void FixedUpdate()
    {
        if (dashTimer < 1f) 
        {
            dashTimer += Time.deltaTime;
            dashTimer = Mathf.Min(dashTimer, 1f);
        }
    }
}
