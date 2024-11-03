using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflect : MonoBehaviour {
    public float refectRadius = 10f;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void ReflectLight(Vector3 orgPoint, Vector3 m_direction, int _colorID) {
        // 计算最左侧方向的向量
        //Vector3 forward_down = Quaternion.Euler(0, 0, -(viewAngle / 2f)) * -transform.up * viewRadius;

        // for (int i = 0; i <= viewAngleStep; i++) {
        //     Vector3 v = Quaternion.Euler(0, 0, (viewAngle / viewAngleStep) * i) * forward_down; // 根据当前角度计算方向向量
        Vector3 pos = (orgPoint + m_direction) * refectRadius; // 计算射线终点

        // 在Scene中绘制线条
        Debug.DrawLine(orgPoint, pos, Color.blue);

        // 射线检测
        Ray2D ray = new Ray2D(orgPoint, m_direction);

        RaycastHit2D[] hits =
            Physics2D.RaycastAll(ray.origin, m_direction, refectRadius, LayerMask.GetMask("Trigger"));

        for (int j = 0; j < hits.Length; j++) {
            RaycastHit2D hitInfo = hits[j];
            if (hitInfo.collider != null) {
                if (hitInfo.collider.CompareTag("BluePlatform") || hitInfo.collider.CompareTag("RedPlatform") ||
                    hitInfo.collider.CompareTag("LightTrigger")) {
                    switch (_colorID) {
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
                else if (hitInfo.collider.CompareTag("Mirror")) {
                    Vector3 rayPoint = hitInfo.point;
                    Vector3 direction = rayPoint - transform.position;
                    Vector3 reflect = Vector3.Reflect(direction, hitInfo.normal);
                }
                else break;
            }
        }
    }
}