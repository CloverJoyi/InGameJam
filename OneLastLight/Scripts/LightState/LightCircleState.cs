using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

//光圈状态
public class LightCircleState : LightStateBase{
    public LightCircleState(MonoBehaviour mono, Rigidbody2D _rb, PlayerController _playerController) : base(mono, _rb,
        _playerController){
    }

    //状态进入时执行
    public override void OnEnter(){
        m_lightBeam.gameObject.SetActive(false);
        m_lightCircle.gameObject.SetActive(true);
        //状态读取
        m_color = SaveData.Instance.m_stateSave._color;
        m_angle = SaveData.Instance.m_stateSave._angle;
        //光圈无白色，将白色转化为红色
        ColorTrans();
        //设置颜色
        m_lightCircle.GetComponent<LightCircleController>().ChangeColor(m_color);
        SaveData.Instance.m_stateSave = SaveState(m_color, m_angle, false);
    }

    //状态退出时执行
    public override void OnExit(){
        //保存状态（灯光模式、角度、颜色）
        SaveData.Instance.m_stateSave = SaveState(m_color, m_angle, false);

        CloseLight();
    }

    public override void OnUpdate(){
        GetPosition();
        SetDirection();
        if (Input.GetMouseButtonDown(1)){
            AudioManager.GetInstance().PlaySound("Light/LightShapeSwitch");
            m_lightStateControll.TransitionState(StateType.LightBeamState);
        }

        if (Input.GetMouseButtonDown(2)){
            m_lightStateControll.TransitionState(StateType.NoLantern);
        }

        if (Input.GetMouseButtonDown(0)){
            AudioManager.GetInstance().PlaySound("Light/LightFireSwitch");
            ChangeLightColor();
        }
    }


    // protected void GetAngle() {
    //     _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     _originPosition = m_lightBeam.position;
    //
    //     var direction = _mousePosition - _originPosition;
    //     m_angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     m_angle += 90;
    //}

    //获取鼠标世界坐标
    protected void GetPosition(){
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    //设置角色面朝向
    private void SetDirection(){
        float flag;

        flag = _mousePosition.x - m_playerBody.position.x;

        if (flag > 0)
            m_rb.transform.localScale = new Vector3(-1, 1, 1);
        else
            m_rb.transform.localScale = new Vector3(1, 1, 1);
    }

    //关闭所有灯光
    private void CloseLight(){
        m_lightCircle.gameObject.SetActive(false);
        m_lightBeam.gameObject.SetActive(false);
    }


    //光圈没有白灯，如果光束为白色时切换光圈会转化为红色
    private void ColorTrans(){
        if (m_color == LightColor.white){
            m_color = LightColor.Red;
        }
    }

    //更改灯光颜色
    private void ChangeLightColor(){
        m_color = (LightColor)((int)m_color + 1);
        if (SaveData.Instance._haveBlue){
            if ((int)m_color > 2) m_color = 0;
        }
        else{
            if ((int)m_color > 1) m_color = 0;
        }

        ColorTrans();

        SaveData.Instance.m_stateSave = SaveState(m_color, m_angle, false);

        m_lightCircle.GetComponent<LightCircleController>().ChangeColor(m_color);
    }
}