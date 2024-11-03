using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    private bool flag;

    private Transform player;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (flag) {
            player.GetComponent<TarodevController.PlayerController>().Save();
            player.GetComponent<PlayerHealth>().setHealth();
        }
    }

    private void FixedUpdate() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<TarodevController.PlayerController>()) {
            flag = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<TarodevController.PlayerController>()) {
            flag = false;
        }
    }
}