using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OverWindow : MonoBehaviour
{
    public void ReStart()
    {
        PlayerHealth.instance.ReStart();
        gameObject.SetActive(false);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }




}
