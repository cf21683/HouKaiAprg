using UnityEngine;
using Animancer;

public class PlayerState_ComboAttack : PlayerCombatState
{
    private int _comboNumber;
    private bool _nextCombo;
    private AnimancerState _state;
    public override void Enter()
    {
        Debug.Log("Enter ComboAttack");
        base.Enter();
        _nextCombo = false;
        _comboNumber = playerController.ReusableData.ComboNumber;
        switch (_comboNumber)
        {
            case 1:
                _state = playerController.Animancer.Play(characterModel.characterAnimationData.Combot1);
                break;
            case 2:
                _state = playerController.Animancer.Play(characterModel.characterAnimationData.Combot2);
                break;
            case 3:
                _state = playerController.Animancer.Play(characterModel.characterAnimationData.Combot3);
                break;
            case 4:
                _state = playerController.Animancer.Play(characterModel.characterAnimationData.Combot4);
                break;
            default:
                Debug.LogError("Invalid combo number");
                break;
        }
        
    }

    public override void Update()
    {
        base.Update();
        
        
        float normTime = _state.NormalizedTime;

        // 在动画播放 40% ~ 90% 之间监听输入（Combo 输入窗口）
        if (!_nextCombo &&
            normTime >= 0.2f && normTime <= 0.9f &&
            playerController.playerInputActions.Character.Fire.triggered)
        {
            _nextCombo = true;
            _comboNumber = _comboNumber % 4 + 1;
            playerController.ReusableData.ComboNumber = _comboNumber;
            Debug.Log("准备进入下一段 Combo：" + _comboNumber);
        }

        // 当动画播放完后，根据是否有下一段切状态
        if (normTime < 0.4f)
        {
            return;
        }

        if (_nextCombo)
        {
            playerController.SwitchState(PlayerStateList.ComboAttack);
        }
        else
        {
            playerController.SwitchState(PlayerStateList.CombatIdle);
        }
        
        
    }
}
