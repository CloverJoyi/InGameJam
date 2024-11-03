using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class HoldTrigger : DoorTrigger{
    public float thickness = 0.15f;
    public float _upForce = 6f;
    public GameObject _door;

    //private bool press;
    private Vector3 _startPos;
    private Rigidbody2D m_rb;
    private Vector3 _endPos;
    public LayerMask targetLayer;

    // Start is called before the first frame update
    void Start(){
        //press = false;
        _startPos = transform.position;
        m_rb = GetComponent<Rigidbody2D>();
        _endPos = _startPos - new Vector3(0, thickness * 0.5f, 0);
    }

    // Update is called once per frame
    void Update(){
        // if (_startPos.y > transform.position.y) {
        //     UpForce();
        // }
        // else {
        //     transform.position = _startPos;
        //     //_door.GetComponent<OrganDoor>().CloseDoor();
        // }
    }

    private void FixedUpdate(){
        //if (math.abs(_startPos.y - transform.position.y) > thickness) {
        //    _door.GetComponent<OrganDoor>().ActivateFlag(this.transform);
        //    transform.position = _endPos;
        //}
        //else
        //{
        //    _door.GetComponent<OrganDoor>().DisActivateFlag(this.transform);
        //    transform.position = _startPos;
        //}
    }

    private void UpForce(){
        m_rb.AddForce(Vector3.up * _upForce, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(1);
        if (((1 << other.gameObject.layer) & targetLayer) != 0){
            //Debug.Log(2);
            _door.GetComponent<OrganDoor>().ActivateFlag(this.transform);
            transform.position = _endPos;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        //Debug.Log(-1);
        if (((1 << other.gameObject.layer) & targetLayer) != 0){
            //Debug.Log(-2);
            // _door.GetComponent<OrganDoor>().DisActivateFlag(this.transform);
            transform.position = _startPos;
        }
    }
}