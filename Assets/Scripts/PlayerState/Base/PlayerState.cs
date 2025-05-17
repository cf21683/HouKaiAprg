
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Animancer;

    public enum PlayerStateList
    {
        Idle, Idle_AFK,
        Walk, Run,Dash_Front,Dash_Back
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
            Rotation();
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


        

        protected bool IsAnimationEnd(AnimancerState state)
        {
            return state.NormalizedTime >= 1f && !state.IsPlaying;
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

        #region  处理角色旋转
        
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = playerController.transform.eulerAngles.y;
            if (currentYAngle == playerController.ReusableData.CurrentTargetRotation.y)
                return;

            float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle,
                playerController.ReusableData.CurrentTargetRotation.y,
                ref playerController.ReusableData.DampedTargetRotationSpeed.y,
                playerController.ReusableData.TimeToReachTargetRotation.y -
                playerController.ReusableData.DampedTargetRoationPassTime.y);
            playerController.ReusableData.DampedTargetRoationPassTime.y += Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f, smoothYAngle, 0f);
            playerController.transform.rotation = targetRotation;
        }


        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != playerController.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        protected Vector3 GetMovementDirection()
        {
            return new Vector3(playerController.ReusableData.Move.x, 0f, playerController.ReusableData.Move.y);
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();
            // Debug.Log("Target rotation angle: " + directionAngle);

            return directionAngle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            playerController.ReusableData.CurrentTargetRotation.y = targetAngle;

            playerController.ReusableData.DampedTargetRoationPassTime.y = 0f;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += playerController.MainCamera.eulerAngles.y;

            if (angle > 360)
            {
                angle -= 360f;
            }

            return angle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0)
            {
                directionAngle += 360f;
            }

            return directionAngle;


        }

        private void Rotation()
        {
            if (playerController.ReusableData.Move == Vector2.zero)
            {
                return;
            }

            Vector3 movementDirection = GetMovementDirection();

            Rotate(movementDirection);

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

