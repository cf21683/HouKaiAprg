using UnityEngine;

public class PlayerState_Sprint : PlayerGroundedState
{
    
    public override void Enter()
    {
        base.Enter();
        playerController.Animancer.Play(playerController.CharacterAnimationData.Sprint, 0.25f);
        
        
    }


    public override void Update()
    {
        base.Update();
        
        Vector3 movement = new Vector3(playerController.ReusableData.Move.x, 0f, playerController.ReusableData.Move.y);
        float cameraY = playerController.MainCamera.transform.rotation.eulerAngles.y;
        Vector3 rotate = Quaternion.Euler(0,cameraY,0) * movement;
        Quaternion targetRotation = Quaternion.LookRotation(rotate);
        float angle = Mathf.Abs(targetRotation.eulerAngles.y - playerController.characterModel.transform.eulerAngles.y);
        Debug.Log("rotate :"+rotate);
        if (angle > 145f && angle < 215f && characterModel.currentState == PlayerStateList.Sprint)
        {
            playerController.SwitchState(PlayerStateList.TurnBack);
        }else
        {
            playerController.characterModel.transform.rotation = Quaternion.Slerp(playerController.characterModel.transform.rotation, targetRotation,
                Time.deltaTime * playerController.rotationSpeed);
        }
        
    }
}
