using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPos : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "Player"){
            SaveData.Instance.SaveBackPos(other.transform);
        }
    }
}