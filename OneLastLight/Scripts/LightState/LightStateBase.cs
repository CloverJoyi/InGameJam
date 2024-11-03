using TarodevController;
using UnityEngine;

//灯光状态的基类
public class LightStateBase {
    protected Vector2 _mousePosition;
    protected Vector2 _originPosition;
    protected LightStateController m_lightStateControll;
    protected MonoBehaviour m_mono;
    protected PlayerController m_playerController;
    protected Rigidbody2D m_rb;

    protected Transform m_playerBody;
    protected Transform m_lightBeam;
    protected Transform m_lightCircle;

    protected LightColor m_color;
    public float m_angle;


    public LightStateBase(MonoBehaviour mono, Rigidbody2D _rb,
        PlayerController _playerController) {
        m_mono = mono;
        m_rb = _rb;
        m_playerController = _playerController;
        m_color = LightColor.Red;

        m_playerBody = m_rb.transform.Find("PlayerBody");
        m_lightBeam = m_rb.transform.Find("LightBeam");
        m_lightCircle = m_rb.transform.Find("LightCircle");
    }

    public virtual void SetLightStateController(LightStateController lightStateController) {
        m_lightStateControll = lightStateController;
    }

    public virtual void OnEnter() {
    }

    public virtual void OnExit() {
    }

    public virtual void OnUpdate() {
    }


    //存储当前状态的方法
    public StateSave SaveState(LightColor color, float angle, bool lightMode) {
        SaveData.Instance.m_stateSave._color = m_color;
        SaveData.Instance.m_stateSave._angle = m_angle;
        SaveData.Instance.m_stateSave.lightMode = lightMode;
        return SaveData.Instance.m_stateSave;
    }
}

//颜色枚举
public enum LightColor {
    white = 0,
    Red = 1,
    Blue = 2,
}