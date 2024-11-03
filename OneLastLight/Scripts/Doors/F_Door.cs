using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Door : DoorTrigger {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<TarodevController.PlayerController>() != null) {
            if (Input.GetKeyDown(KeyCode.F)) {

                if (gameObject.activeSelf)
                {
                    Debug.Log(1);
                    open = true;
                }
                else
                {
                    open = false;
                }
            }
        }
    }
}