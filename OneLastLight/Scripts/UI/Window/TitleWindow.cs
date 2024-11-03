using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleWindow : MonoBehaviour
{

    public void Start()
    {
        AudioManager.GetInstance().PlayBGM("Title");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        SaveData.Instance.Load();
    }

    public void EndGame()
    {
        Application.Quit();
    }


}
