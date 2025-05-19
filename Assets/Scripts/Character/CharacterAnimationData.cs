using System.IO.Enumeration;
using Animancer;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimationData",menuName ="Data/Player/AnimationData")]
public class CharacterAnimationData : ScriptableObject
{
    [SerializeField] ClipTransition idle;
    [SerializeField] ClipTransition idleAFK;
    [SerializeField] ClipTransition run;
    [SerializeField] ClipTransition jumpClip;
    [SerializeField] ClipTransition walk;
    [SerializeField] ClipTransition dashFront;
    [SerializeField] ClipTransition dashBack;
    [SerializeField] ClipTransition sprint;
    [SerializeField] ClipTransition turnBack;
    [SerializeField] ClipTransition sprintEnd;
    [SerializeField] ClipTransition combot1;
    [SerializeField] ClipTransition combot2;
    [SerializeField] ClipTransition combot3;
    [SerializeField] ClipTransition combot4;
    [SerializeField] ClipTransition combatIdle;


    public ClipTransition Idle => idle;
    public ClipTransition IdleAFK => idleAFK;
    public ClipTransition Run => run;
    public ClipTransition Jump => jumpClip;
    public ClipTransition Walk => walk;
    public ClipTransition DashFront => dashFront;
    public ClipTransition DashBack => dashBack;
    public ClipTransition Sprint => sprint;
    public ClipTransition TurnBack => turnBack;
    public ClipTransition SprintEnd => sprintEnd;
    public ClipTransition Combot1 => combot1;
    public ClipTransition Combot2 => combot2;
    public ClipTransition Combot3 => combot3;
    public ClipTransition Combot4 => combot4;
    
    public ClipTransition CombatIdle => combatIdle;
    
}
