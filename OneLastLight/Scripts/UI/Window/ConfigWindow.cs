using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ConfigWindow : MonoBehaviour
{
    public static float volume = 1f; // 静态变量用于存储Slider的Value
    public static float se = 1f;
    public Slider volumeSlider; // 在Inspector中拖拽Slider组件到这里
    public Slider seSlider;
    public TextMeshProUGUI[] quanping;
    public TextMeshProUGUI[] huazhi;

    private void Awake()
    {

    }
    void Start()
    { 
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        seSlider.onValueChanged.AddListener(OnSeSliderChanged);
    }
    private void OnEnable()
    {
        volumeSlider.value = volume;
        seSlider.value = se;
    }

    private void OnVolumeSliderChanged(float newValue)
    {
        volume = newValue;
        AudioManager.GetInstance().SetBGMVolume(volume * 0.5f);
    }

    public void OnSeSliderChanged(float newValue)
    {
        se = newValue;
        AudioManager.GetInstance().SetSoundVolume(volume * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseFullScreen()
    {
        Screen.fullScreen = false;  //退出全屏
        quanping[0].color = new Color(144,144,144);
        quanping[1].color = new Color(255, 255, 255);
    }

    public void OpenFullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
        quanping[0].color = new Color(255, 255, 255);
        quanping[1].color = new Color(144, 144, 144);
    }

    public void SetQuality(int i)
    {
        switch(i)
        {
            case 0:
                QualitySettings.SetQualityLevel(1, true);
                huazhi[0].color = new Color(255,255,255);
                huazhi[1].color = new Color(144, 144, 144);
                huazhi[2].color = new Color(144, 144, 144);
                break;
            case 1:
                QualitySettings.SetQualityLevel(3, true);
                huazhi[2].color = new Color(255, 255, 255);
                huazhi[1].color = new Color(144, 144, 144);
                huazhi[0].color = new Color(144, 144, 144);
                break;
            case 2:
                QualitySettings.SetQualityLevel(5, true);
                huazhi[1].color = new Color(255, 255, 255);
                huazhi[0].color = new Color(144, 144, 144);
                huazhi[2].color = new Color(144, 144, 144);
                break;
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
