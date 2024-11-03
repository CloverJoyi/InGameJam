using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : DoorTrigger {
    private bool _inLight;

    private float reset_timer;
    private float active_timer;

    public GameObject _door;
    public float activeTime = 3f;

    // Start is called before the first frame update
    void Start() {
        _inLight = false;
    }

    // Update is called once per frame
    void Update() {
        if (_inLight) {
            reset_timer += Time.deltaTime;
            active_timer += Time.deltaTime;
            if (reset_timer > 0.2) {
                ReSet();
            }

            if (active_timer > activeTime) {
                _door.GetComponent<OrganDoor>().ActivateFlag(this.transform);
            }
        }
    }

    public void InLight() {
        //Debug.Log("Light Trigger");
        _inLight = true;
        reset_timer = 0;
    }

    private void ReSet() {
        _inLight = false;
    }
}