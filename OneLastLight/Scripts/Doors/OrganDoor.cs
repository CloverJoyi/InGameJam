using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganDoor : DoorTrigger{
    public Transform[] organ;
    private bool _isActive;
    private Dictionary<Transform, bool> _organDict;

    // Start is called before the first frame update
    void Start(){
        Init();
    }

    // Update is called once per frame
    void Update(){
    }

    private void FixedUpdate(){
        CheckOrgan();
    }

    public void ActivateFlag(Transform organ){
        _organDict[organ] = true;
    }
    // public void DisActivateFlag(Transform organ) {
    //     _organDict[organ] = false;
    // }

    private void OpenDoor(){
        AudioManager.GetInstance().PlaySound("Door");
        gameObject.SetActive(false);
    }

    private void CloseDoor(){
        gameObject.SetActive(true);
    }

    private void CheckOrgan(){
        foreach (var item in _organDict){
            if (!item.Value){
                return;
            }
        }

        OpenDoor();
    }

    private void Init(){
        _organDict = new Dictionary<Transform, bool>();

        foreach (Transform t in organ){
            _organDict.Add(t, false);
        }
    }
}