using UnityEngine;


public class PlayerReusableData
 {
     public bool ShouldWalk{get;set;}
     public bool HasLeftGround{get;set;} = false;

     public Vector2 Move{get;set;}

     private Vector3 _currentTargetRotation;
     private Vector3 _timeToReachTargetRotation;
     private Vector3 _dampedTargetRotationSpeed;
     private Vector3 _dampedTargetRoationPassTime;
     
     public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
     public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
     public ref Vector3 DampedTargetRotationSpeed => ref _dampedTargetRotationSpeed;
     public ref Vector3 DampedTargetRoationPassTime => ref _dampedTargetRoationPassTime;

     


 }

