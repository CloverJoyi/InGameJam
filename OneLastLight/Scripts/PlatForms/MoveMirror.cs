using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMirror : MonoBehaviour {
    public float moveSpeed;
    private float waitTime;
    public float totalTime;
    //public Transform[] movePos;

    //public Transform[] movePos;
    private bool _collision;

    //private Transform playerTransform;
    private int moveRight = 1; //1为右，-1为左

    //i是1则右，是0则变成左
    //private int i;

    void Start() {
        //playerTransform = GameObject.Find("Player").transform.parent;
        //i = 1;
        _collision = false;
        waitTime = totalTime;
    }

    void Update() {
        if (_collision) {
            //且等待时间小于0
            if (waitTime < 0) {
                waitTime = totalTime;
                moveRight = -moveRight;
                _collision = false;
            }
            else {
                waitTime -= Time.deltaTime;
            }
        }
        else {
            transform.position = new Vector2(transform.position.x + moveRight * moveSpeed * Time.deltaTime,
                transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player")) {
            _collision = true;
            //moveRight = -moveRight;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
    }
}