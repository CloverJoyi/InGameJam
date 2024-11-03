using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;


public class StateMachine : MonoBehaviour {
    public enum state {
        onGround,
        inAir,
        wait
    }

    public enum animState {
        idel,
        jump,
        walk
    }

    public GameObject FootPoint;
    public GameObject sprite;
    public Animator sa;
    public float gracetime;
    public bool waitFlag;

    public state playerState;
    public animState playerAnim;

    //[SerializeField] float groundHeight = 0.3f;

    private Rigidbody2D rb;
    private PlayerController pc;
    private float gTime;

    private void Awake() {
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        sa = sprite.GetComponent<Animator>();
    }

    private void Update() {
        StateMac();
        AnimMac();
        LightMac();
    }

    private void LightMac() {
        if (SaveData.Instance.m_stateSave.isLight) {
            if (SaveData.Instance.m_stateSave._color == LightColor.Blue) {
                sa.SetInteger("LightState", 2);
            }
            else if (SaveData.Instance.m_stateSave._color == LightColor.Red) {
                sa.SetInteger("LightState", 1);
            }
            else {
                sa.SetInteger("LightState", 2);
            }
        }
        else {
            sa.SetInteger("LightState", 0);
        }
    }

    private void StateMac() {
        if (waitFlag) {
            playerState = state.wait;
            GetComponent<PlayerController>().enabled = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        else if (SaveData.Instance.groundHit) {
            playerState = state.onGround;
            GetComponent<PlayerController>().enabled = true;
        }
        else {
            playerState = state.inAir;
            GetComponent<PlayerController>().enabled = true;
        }
    }

    private void AnimMac() {
        // if (rb.velocity.x > 0.1) {
        sprite.GetComponent<SpriteRenderer>().flipX = true;
        // }
        // else if (rb.velocity.x < -0.1) {
        //     sprite.GetComponent<SpriteRenderer>().flipX = false;
        // }

        if (playerState == state.inAir) {
            if (rb.velocity.y > 0) {
                //sprite.transform.localScale = new Vector3(1.5f, 1.5f, transform.localScale.z);
                sa.SetInteger("AnimState", 1);
            }
            else {
                //sprite.transform.localScale = new Vector3(1.5f, 1.5f, transform.localScale.z);
                sa.SetInteger("AnimState", 0);
            }
        }
        else {
            if (Mathf.Abs(rb.velocity.x) <= 0.1f) {
                //sprite.transform.localScale = new Vector3(1.5f, 1.5f, transform.localScale.z);
                sa.SetInteger("AnimState", 0);
            }
            else if (rb.velocity.x > 0.1f) {
                //sprite.transform.localScale = new Vector3(1.5f, 1.5f, transform.localScale.z);
                sa.SetInteger("AnimState", 2);
            }
            else if (rb.velocity.x < -0.1f) {
                //sprite.transform.localScale = new Vector3(1.5f, 1.5f, transform.localScale.z);
                sa.SetInteger("AnimState", 2);
            }
        }
    }

    // private bool IsOnGround() {
    //     RaycastHit hitInfo;
    //     if (Physics.Raycast(FootPoint.transform.position, new Vector3(0, -1, 0), out hitInfo, groundHeight)) {
    //         gTime = 0;
    //         return true;
    //     }
    //     else {
    //         gTime += Time.deltaTime;
    //         if (gTime < gracetime) {
    //             return true;
    //         }
    //         else return false;
    //     }
    // }
}