using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBeamController : MonoBehaviour{
    public float viewRadius = 10f; //视野距离
    public int viewAngleStep = 20; //射线密度
    [Range(0, 360)] public float viewAngle = 30f; //视野角度
    private float m_angle;
    public int _colorID;
    private PlayerController playerController;
    private Light2D _light2D;

    void Awake(){
        if (gameObject.transform.parent != null){
            playerController = gameObject.transform.parent.GetComponent<PlayerController>();
        }

        _light2D = GetComponent<Light2D>();
    }


    void Update(){
        if (playerController != null){
            m_angle = playerController._lightBeamState.m_angle;
        }
        else{
            // 处理 playerController 为 null 的情况
            Debug.LogWarning("PlayerController not found on parent object.");
        }

        SetLightBeamAngle(m_angle);
        DrawFieldOfView();
    }


    //此方法用于实现光线激活平台、阻挡、穿透
    void DrawFieldOfView(){
        // 计算最左侧方向的向量
        Vector3 forward_down = Quaternion.Euler(0, 0, -(viewAngle / 2f)) * -transform.up * viewRadius;

        for (int i = 0; i <= viewAngleStep; i++){
            Vector3 v = Quaternion.Euler(0, 0, (viewAngle / viewAngleStep) * i + 180f) * forward_down; // 根据当前角度计算方向向量
            Vector3 pos = transform.position + v; // 计算射线终点

            // 在Scene中绘制线条
            Debug.DrawLine(transform.position, pos, Color.red);

            // 射线检测
            Ray2D ray = new Ray2D(transform.position, v);

            RaycastHit2D[] hits =
                Physics2D.RaycastAll(ray.origin, v, viewRadius, LayerMask.GetMask("Trigger"));

            for (int j = 0; j < hits.Length; j++){
                RaycastHit2D hitInfo = hits[j];
                if (hitInfo.collider != null){
                    if (hitInfo.collider.CompareTag("BluePlatform") || hitInfo.collider.CompareTag("RedPlatform") ||
                        hitInfo.collider.CompareTag("LightTrigger")){
                        switch (_colorID){
                            case 0:
                                if (hitInfo.collider.CompareTag("BluePlatform"))
                                    hitInfo.collider.gameObject.GetComponent<PlatFormController>().BoxAppear();
                                if (hitInfo.collider.CompareTag("RedPlatform"))
                                    hitInfo.collider.gameObject.GetComponent<PlatFormController>().BoxDisAppear();
                                if (hitInfo.collider.CompareTag("LightTrigger"))
                                    hitInfo.collider.gameObject.GetComponent<LightTrigger>().InLight();
                                break;
                            case 1:
                                if (hitInfo.collider.CompareTag("BluePlatform"))
                                    hitInfo.collider.gameObject.GetComponent<PlatFormController>().BoxDisAppear();
                                if (hitInfo.collider.CompareTag("RedPlatform"))
                                    hitInfo.collider.gameObject.GetComponent<PlatFormController>().BoxAppear();
                                if (hitInfo.collider.CompareTag("LightTrigger"))
                                    hitInfo.collider.gameObject.GetComponent<LightTrigger>().InLight();
                                break;
                            case 2:
                                if (hitInfo.collider.CompareTag("BluePlatform"))
                                    hitInfo.collider.gameObject.GetComponent<PlatFormController>().BoxAppear();
                                if (hitInfo.collider.CompareTag("RedPlatform"))
                                    hitInfo.collider.gameObject.GetComponent<PlatFormController>().BoxAppear();
                                if (hitInfo.collider.CompareTag("LightTrigger"))
                                    hitInfo.collider.gameObject.GetComponent<LightTrigger>().InLight();
                                break;
                        }
                    }
                    else if (hitInfo.collider.CompareTag("Mirror")){
                        Vector3 rayPoint = hitInfo.point;
                        Vector3 direction = rayPoint - transform.position;
                        Vector3 reflect = Vector3.Reflect(direction, hitInfo.normal);
                        reflect = Vector3.Normalize(reflect);
                        hitInfo.collider.GetComponent<MirrorReflect>().ReflectLight(rayPoint, reflect, _colorID);
                    }
                    else break;
                }
            }
        }
    }


    //将颜色状态转换为id
    public void ChangeColor(LightColor m_color){
        switch (m_color){
            case LightColor.Red:
                _colorID = 0;
                //Debug.Log("红色");
                _light2D.color = Color.magenta;
                break;
            case LightColor.Blue:
                _colorID = 1;
                //Debug.Log("蓝色");
                _light2D.color = Color.cyan;
                break;
            case LightColor.white:
                _colorID = 2;
                //Debug.Log("白色");
                _light2D.color = Color.white;
                break;
        }
    }


    //设置光束角度
    public void SetLightBeamAngle(float _angle){
        this.transform.eulerAngles = new Vector3(0, 0, _angle + 180f);
    }
}