using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowPlatformController : MonoBehaviour {
    public Rigidbody2D _goal;


    private float speed;
    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        speed = math.abs(_goal.velocity.x);
        if (GetDir() > 0.2) {
            _rb.velocity = new Vector2(-speed, 0);
        }
        else if (GetDir() < -0.2) {
            _rb.velocity = new Vector2(speed, 0);
        }
        else {
            _rb.velocity = Vector2.zero;
        }
    }

    private float GetDir() {
        float dir;
        dir = _rb.position.x - _goal.position.x;
        return dir;
    }
}