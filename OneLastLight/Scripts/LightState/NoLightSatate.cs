using TarodevController;
using UnityEngine;

//已经将提灯扔出，角色无灯状态
public class NoLightState : LightStateBase {
    private readonly float _groundCheckRadius = 3f; //可拾取提灯的范围
    private bool _canPickLantern;

    public NoLightState(MonoBehaviour mono, Rigidbody2D _rb, PlayerController _playerController)
        : base(mono, _rb, _playerController) {
    }

    public override void OnEnter() {
        //M_ThrowLantern(m_playerBody); //在玩家位置生成一个提灯
        //SaveData.Instance.m_stateSave.isLight = false;
        CloseLight();
    }

    public override void OnExit() {
        //OpenLight();
    }

    public override void OnUpdate() {
        CheckSurroundings();
        GetPosition();
        SetDirection();
        SaveData.Instance.m_stateSave.isLight = false;

        if (SaveData.Instance._haveLight) {
            if (Input.GetMouseButtonDown(2)) {
                SaveData.Instance.m_stateSave.isLight = true;
                if (SaveData.Instance.m_stateSave.lightMode) {
                    m_lightStateControll.TransitionState(StateType.LightBeamState);
                }
                else {
                    m_lightStateControll.TransitionState(StateType.LightCircleState);
                }
                //m_lightStateControll.TransitionState(StateType.LightBeamState);
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.F) && _canPickLantern) {
                SaveData.Instance._haveLight = true;
                m_playerController.Save();
                m_lightStateControll.TransitionState(StateType.LightBeamState);
                DestroyLantern();
            }
        }
    }


//获取鼠标世界坐标
    protected void GetPosition() {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


//设置角色转向
    private void SetDirection() {
        float flag;

        flag = _mousePosition.x - m_playerBody.position.x;

        if (flag > 0)
            m_rb.transform.localScale = new Vector3(-1, 1, 1);
        else
            m_rb.transform.localScale = new Vector3(1, 1, 1);
    }


//检测是否在可以捡起提灯的范围内
    private void CheckSurroundings() {
        _canPickLantern =
            Physics2D.OverlapCircle(m_playerBody.position, _groundCheckRadius, LayerMask.GetMask("Lantern"));
    }

//销毁外部提灯
    private void DestroyLantern() {
        var lantern = GameObject.Find("lantern(External)");
        m_playerController.MyDestroy(lantern);
    }

//扔出提灯
// private void M_ThrowLantern(Transform m_playerBody) {
//     m_playerController.ThrowLantern(m_playerBody);
// }

//关灯
    private void CloseLight() {
        m_lightCircle.gameObject.SetActive(false);
        m_lightBeam.gameObject.SetActive(false);
    }
}