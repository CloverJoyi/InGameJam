using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingWindow : MonoBehaviour{
    public static PlayingWindow instance;

    public GameObject OverWindow;
    public GameObject ConfigWindow;

    [Header("ͼ��")] public bool isGreen;
    public GameObject green;
    public GameObject red;
    public GameObject[] greenL;
    public GameObject[] redL;
    public Image[] hpImage;

    private void Awake(){
        if (instance == null)
            instance = this;
    }

    private void Start(){
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            OpenConfig();
        }

        HpUpdate();
        ColorJudge();
        ChangeLightShape();
        ChangeLightColor();
    }

    public void ColorJudge(){
        if (SaveData.Instance.m_stateSave._color == LightColor.Blue ||
            SaveData.Instance.m_stateSave._color == LightColor.white){
            isGreen = true;
        }
        else if (SaveData.Instance.m_stateSave._color == LightColor.Red){
            isGreen = false;
        }

        green.SetActive(isGreen);
        red.SetActive(!isGreen);
    }

    public void HpUpdate(){
        hpImage[0].fillAmount = (float)PlayerHealth.instance.currentHealth / (float)PlayerHealth.instance.maxHealth;
        hpImage[1].fillAmount = (float)PlayerHealth.instance.currentHealth / (float)PlayerHealth.instance.maxHealth;
    }

    public void ChangeLightColor(){
    }

    public void ChangeLightShape(){
        if (isGreen){
            if (SaveData.Instance.m_stateSave.lightMode){
                greenL[0].SetActive(false);
                greenL[1].SetActive(true);
            }
            else{
                greenL[0].SetActive(true);
                greenL[1].SetActive(false);
            }
        }
        else{
            if (SaveData.Instance.m_stateSave.lightMode){
                redL[0].SetActive(false);
                redL[1].SetActive(true);
            }
            else{
                redL[0].SetActive(true);
                redL[1].SetActive(false);
            }
        }
    }

    public void OpenConfig(){
        AudioManager.GetInstance().PlaySound("UI/Click");
        ConfigWindow.SetActive(true);
    }


    public void OpenOver(){
        OverWindow.SetActive(true);
    }
}