using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HurtArea : MonoBehaviour{
    public int damage; //伤害

    public float hurtCD; //伤害间隔
    //public Transform backPos; //回退点
    //public float hurtDistance = 1;//回退距离

    public static HurtArea instance;
    private bool canHurt;
    private float timer;
    private Transform player;

    private void Awake(){
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        canHurt = true;
    }

    // Update is called once per frame
    void Update(){
        if (!canHurt){
            timer += Time.deltaTime;
            if (timer >= hurtCD){
                timer = 0;
                canHurt = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.GetComponent<PlayerHealth>() != null){
            if (canHurt){
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                canHurt = false;
            }
        }
    }

    // public void ChangeSavePoint(Transform point)
    // {
    //     backPos = point;
    // }
}