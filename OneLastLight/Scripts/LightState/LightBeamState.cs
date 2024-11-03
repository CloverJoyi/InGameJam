using TarodevController;
using UnityEngine;


//光束状态
public class LightBeamState : LightStateBase{
    public LightBeamState(MonoBehaviour mono, Rigidbody2D _rb,
        PlayerController _playerController) : base(mono,
        _rb, _playerController){
    }

    //进入光束状态时执行
    public override void OnEnter(){
        m_lightBeam.gameObject.SetActive(true);
        m_lightCircle.gameObject.SetActive(false);
        //从存储的状态中读取状态
        m_color = SaveData.Instance.m_stateSave._color;
        m_angle = SaveData.Instance.m_stateSave._angle;
        //根据状态设置
        m_lightBeam.GetComponent<LightBeamController>().ChangeColor(m_color);
        m_lightBeam.GetComponent<LightBeamController>().SetLightBeamAngle(m_angle);
        SaveData.Instance.m_stateSave = SaveState(m_color, m_angle, true);
    }


    //退出光束状态时执行
    public override void OnExit(){
        //保存状态（灯光模式、角度、颜色）
        SaveData.Instance.m_stateSave = SaveState(m_color, m_angle, true);

        CloseLight();
    }

    //光束状态运行时执行
    public override void OnUpdate(){
        GetAngle();

        SetDirection();
        //按下J键切换为光圈状态
        if (Input.GetMouseButtonDown(1)){
            AudioManager.GetInstance().PlaySound("Light/LightShapeSwitch");
            m_lightStateControll.TransitionState(StateType.LightCircleState);
        }

        //按下L键开关灯
        if (Input.GetMouseButtonDown(2)){
            m_lightStateControll.TransitionState(StateType.NoLantern);
        }

        //按下E键切换颜色
        if (Input.GetMouseButtonDown(0)){
            AudioManager.GetInstance().PlaySound("Light/LightFireSwitch");
            ChangeLightColor();
        }
    }

    //获取鼠标位置与光束中心点之间的角度
    protected void GetAngle(){
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _originPosition = m_lightBeam.position;

        var direction = _mousePosition - _originPosition;
        m_angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_angle += 90;
    }

    //设置人物面朝向
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

    //更改灯光颜色
    private void ChangeLightColor(){
        m_color = (LightColor)((int)m_color + 1);
        if (SaveData.Instance._haveBlue){
            if ((int)m_color > 2) m_color = 0;
        }
        else{
            if ((int)m_color > 1) m_color = 0;
        }

        SaveData.Instance.m_stateSave = SaveState(m_color, m_angle, true);

        m_lightBeam.GetComponent<LightBeamController>().ChangeColor(m_color);
    }
}