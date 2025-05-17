using UnityEngine;
using Animancer;

public class CharacterModel : MonoBehaviour
{
    public CharacterAnimationData characterAnimationData;
    public PlayerStateList currentState;
    [HideInInspector] public CharacterController characterController;
    public float gravity = -9.8f;

    void Awake()
    { 
        characterController = GetComponent<CharacterController>();           
    }

    void Update()
    {

    }
}
