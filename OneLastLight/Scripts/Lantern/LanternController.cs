using UnityEngine;
using System.Collections.Generic;

//此脚本挂载在提灯上，当提灯扔出时生效
public class LanternController : MonoBehaviour {
    private StateSave L_stateSave;

    //private Transform _lightBeam;
    //private Transform _lightCircle;
    private Rigidbody2D m_rb;


    private void Awake() {
        //_lightBeam = transform.Find("LightBeam");
        //_lightCircle = transform.Find("LightCircle");
        //_lightBeam.gameObject.SetActive(false);
        //_lightCircle.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        //Init();
    }

    private void Update() {
        //m_rb.AddForce(new Vector2(0, -1f) * 1000, ForceMode2D.Force);
    }

    public void GetState(StateSave stateSave) {
        L_stateSave = stateSave;
    }

    //根据存储的状态设置提灯的状态
    // private void Init() {
    //     if (L_stateSave.lightMode) {
    //         _lightBeam.gameObject.SetActive(true);
    //         _lightCircle.gameObject.SetActive(false);
    //         _lightBeam.GetComponent<BeamController>().GetState(L_stateSave);
    //     }
    //     else {
    //         _lightBeam.gameObject.SetActive(false);
    //         _lightCircle.gameObject.SetActive(true);
    //         _lightCircle.GetComponent<CircleController>().GetState(L_stateSave);
    //     }
    // }
    private GameObject player;
    public GameObject tip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tip.SetActive(false);
        }
    }

}