using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    public static PlayerHealth instance;

    public int maxHealth;

    public int currentHealth;

    private TarodevController.PlayerController playerCtr;

    private void Awake(){
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        currentHealth = maxHealth;
        playerCtr = GetComponent<TarodevController.PlayerController>();
    }

    // Update is called once per frame
    void Update(){
    }

    //造成伤害
    public void TakeDamage(int damage){
        currentHealth -= damage;
        if (currentHealth <= 0){
            Death();
        }
        else{
            SetPos();
        }
    }

    //加血
    public void Heal(int heal){
        currentHealth += heal;
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    //将血量设置为满血
    public void setHealth(){
        currentHealth = maxHealth;
    }

    private void Death(){
        PlayingWindow.instance.OpenOver();
    }

    public void ReStart(){
        playerCtr.Load();
        setHealth();
    }

    private void SetPos(){
        this.gameObject.transform.position = SaveData.Instance._backPos;
    }
}