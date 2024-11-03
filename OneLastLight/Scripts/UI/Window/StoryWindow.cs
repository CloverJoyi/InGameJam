using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StoryWindow : MonoBehaviour{
    public static StoryWindow instance;

    public TextMeshProUGUI t;

    private void Awake(){
        if (instance == null) instance = this;
    }

    private void Start(){
        Begin();
    }

    public void Begin(){
        AudioManager.GetInstance().PlayBGM("Story");
        t.DOFade(1, 0.5f);
        t.text = "谁在乎又一盏灯光暗淡，若天际中群星闪烁";
        Invoke("BeginTwo", 3f);
    }

    public void BeginTwo(){
        Disappear();
        Invoke("Appear", 0.5f);
        t.text = "谁会在意一个人的时间走到尽头，若生命皆短暂如蜉蝣";
        Invoke("DisappearStart", 3f);
    }

    public void Appear(){
        t.DOFade(1, 0.5f);
    }

    public void DisappearStart(){
        t.DOFade(0, 0.5f);
        gameObject.SetActive(false);
        AudioManager.GetInstance().PlayBGM("PartOne");
    }

    public void DisappearEnd(){
        t.DOFade(0, 0.5f);
        Over();
    }


    public void Disappear(){
        t.DOFade(0, 0.5f);
    }

    public void End(){
        AudioManager.GetInstance().PlayBGM("End");
        t.DOFade(1, 0.5f);
        t.text = "";
        Invoke("BeginTwo", 3f);
    }

    public void EndTwo(){
        Disappear();
        Invoke("Appear", 0.5f);
        t.text ="";
        Invoke("Disappear", 3f);
    }

    public void Over(){
        SceneManager.LoadScene(0);
    }
}