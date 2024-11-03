using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBlueLight : MonoBehaviour {
    private GameObject player;
    public GameObject tip;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void FixedUpdate() {
        if (player != null) {
            if (Input.GetKeyDown(KeyCode.F)) {
                player.gameObject.GetComponent<TarodevController.PlayerController>().GetBlueLight();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            player = other.gameObject;
            tip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            player = null;
            tip.SetActive(false);
        }
    }
}