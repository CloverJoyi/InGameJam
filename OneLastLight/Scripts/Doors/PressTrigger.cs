using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class PressTrigger : DoorTrigger {
    public float thickness = 0.15f;
    public float _upForce = 6f;
    public GameObject _door;

    //private bool press;
    private Vector3 _startPos;
    private Rigidbody2D m_rb;
    private Vector3 _endPos;

    // Start is called before the first frame update
    void Start() {
        //press = false;
        _startPos = transform.position;
        m_rb = GetComponent<Rigidbody2D>();
        _endPos = _startPos - new Vector3(0, thickness + 0.2f, 0);
    }

    // Update is called once per frame
    void Update() {
        // if (_startPos.y > transform.position.y) {
        //     UpForce();
        // }
        // else {
        //     transform.position = _startPos;
        //     //_door.GetComponent<OrganDoor>().CloseDoor();
        // }
    }

    private void FixedUpdate() {
        if (math.abs(_startPos.y - transform.position.y) > thickness) {
            _door.GetComponent<OrganDoor>().ActivateFlag(this.transform);
            transform.position = _endPos;
        }
    }

    private void UpForce() {
        m_rb.AddForce(Vector3.up * _upForce, ForceMode2D.Force);
    }

    // private void OnCollisionStay2D(Collision2D other) {
    //     if (other != null) {
    //         Debug.Log(other.gameObject.name);
    //         press = true;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D other) {
    //     if (other != null) {
    //         if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Lantern")) {
    //             press = false;
    //         }
    //     }
    // }
}