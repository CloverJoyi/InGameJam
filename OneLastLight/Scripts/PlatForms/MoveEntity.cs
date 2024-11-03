using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEntity : MonoBehaviour {
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start() {
        playerTransform = GameObject.Find("Player").transform.parent;
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            //将movingPlateform作为player的父对象
            other.gameObject.transform.parent = gameObject.transform.parent.transform.parent;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D") {
            //将movingPlateform作为player的父对象
            other.gameObject.transform.parent = playerTransform;
        }
    }
}