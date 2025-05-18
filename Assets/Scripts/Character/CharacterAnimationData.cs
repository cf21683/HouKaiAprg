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
    [SerializeField] ClipTransition attack1;
    [SerializeField] ClipTransition walk;
    [SerializeField] ClipTransition dashFront;
    [SerializeField] ClipTransition dashBack;
    [SerializeField] ClipTransition sprint;
    [SerializeField] ClipTransition turnBack;


    public ClipTransition Idle => idle;
    public ClipTransition IdleAFK => idleAFK;
    public ClipTransition Run => run;
    public ClipTransition Jump => jumpClip;
    public ClipTransition Attack1 => attack1;
    public ClipTransition Walk => walk;
    public ClipTransition DashFront => dashFront;
    public ClipTransition DashBack => dashBack;
    public ClipTransition Sprint => sprint;
    public ClipTransition TurnBack => turnBack;
}
