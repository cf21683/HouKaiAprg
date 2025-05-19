
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Animancer;

    public enum PlayerStateList
    {
        Idle, Idle_AFK,
        Walk, Run,Dash_Front,Dash_Back,Sprint,TurnBack,SprintEnd,ComboAttack,CombatIdle
    }
    
    public class PlayerState : IState
    {
        
        protected PlayerController playerController;
        protected CharacterModel characterModel;
        
        public override void Init(IStateMachineOwner owner)
        {
            playerController = (PlayerController)owner;
            characterModel = playerController.characterModel;
        }

        public override void Enter()
        {
            // Debug.Log("State: " + GetType().Name);
            //
            AddInputActionsCallbacks();
            
        }



        public override void Exit()
        {
            RemoveInputActionsCallbacks();
        }


        public override void Update()
        {
            
            playerController.ReusableData.Move = playerController.playerInputActions.Character.Move.ReadValue<Vector2>();
            // Rotation();
            characterModel.characterController.Move(new Vector3(0, characterModel.gravity * Time.deltaTime, 0));
            
        }
        
        public override void UnInit()
        {

        }

        public override void FixedUpdate()
        {
            characterModel.characterController.Move(new Vector3(0, characterModel.gravity * Time.deltaTime, 0));
        }
        
        public override void LateUpdate()
        {
        }

        protected virtual void Rotation()
        {
            Vector3 movement = new Vector3(playerController.ReusableData.Move.x, 0f, playerController.ReusableData.Move.y);
            float cameraY = playerController.MainCamera.transform.rotation.eulerAngles.y;
            Vector3 rotate = Quaternion.Euler(0,cameraY,0) * movement;
            Quaternion targetRotation = Quaternion.LookRotation(rotate);
            float angle = Mathf.Abs(targetRotation.eulerAngles.y - playerController.characterModel.transform.eulerAngles.y);
            playerController.characterModel.transform.rotation = Quaternion.Slerp(playerController.characterModel.transform.rotation, targetRotation, Time.deltaTime * playerController.rotationSpeed);
        }
        

        #region 订阅按键
        protected virtual void AddInputActionsCallbacks()
        {
            playerController. playerInputActions.Character.ShouldWalk.started += OnShouldWalkStarted;

        }

        protected virtual void RemoveInputActionsCallbacks()
        {
            playerController.playerInputActions.Character.ShouldWalk.started -= OnShouldWalkStarted;
        }
        
        #endregion
        

        #region 按键响应方法
        protected virtual void OnShouldWalkStarted(InputAction.CallbackContext context)
        {
            playerController.ReusableData.ShouldWalk = !playerController.ReusableData.ShouldWalk;
            Debug.Log("shouldWalk :" + playerController.ReusableData.ShouldWalk);
        }
        

        #endregion
        

    }

