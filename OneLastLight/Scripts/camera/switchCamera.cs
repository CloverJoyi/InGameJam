using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class switchCamera : MonoBehaviour{
    public CinemachineVirtualCamera defaultCamera; // 默认虚拟相机

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")) // 检测是否是玩家
        {
            switch (gameObject.tag){
                case "PartOne":
                    AudioManager.GetInstance().PlayBGM("PartOne");
                    break;
                case "PartTwo":
                    AudioManager.GetInstance().PlayBGM("PartTwo");
                    break;
                case "PartThree":
                    AudioManager.GetInstance().PlayBGM("PartThree");
                    break;
            }

            defaultCamera.Priority = 15; // 设置默认相机的优先级低于目标相机
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            defaultCamera.Priority = 10;
        }
    }
}