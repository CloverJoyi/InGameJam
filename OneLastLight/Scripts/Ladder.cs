using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            other.gameObject.GetComponent<TarodevController.PlayerController>().GetClimb(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            other.gameObject.GetComponent<TarodevController.PlayerController>().GetClimb(false);
        }
    }
}