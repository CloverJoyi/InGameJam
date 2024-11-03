using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFarmController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }


    //用碰撞检测判定是否可以乘风飞
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            other.gameObject.GetComponent<TarodevController.PlayerController>().GetCanFly(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            other.gameObject.GetComponent<TarodevController.PlayerController>().GetCanFly(false);
        }
    }
}