using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public enum StateType {
    LightBeamState,
    LightCircleState,
    NoLantern,
}


public class LightStateController {
    private readonly Animator animator;
    private readonly PlayerController m_player;
    protected LightStateBase currentState; //当前状态

    protected Dictionary<StateType, LightStateBase>
        states = new(); //状态字典

    public LightStateController(PlayerController player) {
        m_player = player;
    }

    public void Init() {
        //注册状态
        states.Add(StateType.LightBeamState, m_player._lightBeamState);
        states.Add(StateType.LightCircleState, m_player._lightCircleState);
        states.Add(StateType.NoLantern, m_player._noLightState);

        //设置初始状态为光束状态
        TransitionState(StateType.NoLantern);
    }

    public void Update() {
        currentState.OnUpdate();
    }

    //控制状态转变
    public void TransitionState(StateType Type) {
        if (currentState != null) currentState.OnExit();

        currentState = states[Type];
        currentState.OnEnter();
    }

    //（预留）播放动画
    public void PlayAnimation(string animation) {
        animator.Play(animation);
    }
}