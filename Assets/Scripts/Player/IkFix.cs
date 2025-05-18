using System;
using UnityEngine;

public class IkFix : MonoBehaviour
{
    private Animator _animator;
    [Range(0,1f)] public float weight;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,weight);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,weight);
        
    }
}
