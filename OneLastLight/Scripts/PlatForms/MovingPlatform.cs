using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour{
    public float moveSpeed;
    private Transform playerTransform;
    public float dir;
    public bool isPlayerOn;
    public bool dirlock;
    private float timer;

    void Start(){
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update(){
        if (isPlayerOn){
            timer += Time.deltaTime;
            if (timer > 0.4f){
                isPlayerOn = false;
                timer = 0;
            }
        }
    }

    void FixedUpdate(){
        transform.position = new Vector2(transform.position.x + dir * moveSpeed * Time.deltaTime, transform.position.y);
        if (isPlayerOn && playerTransform.GetComponent<PlayerController>()._grounded){
            playerTransform.position = new Vector2(playerTransform.position.x + dir * moveSpeed * Time.deltaTime,
                playerTransform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            if (!dirlock){
                dir = -dir;
                StartCoroutine(lockdir());
            }
        }
    }

    IEnumerator lockdir(){
        dirlock = true;
        yield return new WaitForSeconds(0.2f);
        dirlock = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            if (!dirlock){
                dir = -dir;
                StartCoroutine(lockdir());
            }
        }
    }


    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            isPlayerOn = true;
            timer = 0;
        }
        else{
            isPlayerOn = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            isPlayerOn = false;
        }
    }
}