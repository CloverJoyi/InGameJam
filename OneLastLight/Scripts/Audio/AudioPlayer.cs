using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlayFootStep()
    {
        int i = Random.Range(0,5);
        switch(i)
        {
            case 0:
                AudioManager.GetInstance().PlaySound("FootStep/FootStep1");
                break;
            case 1:
                AudioManager.GetInstance().PlaySound("FootStep/FootStep2");
                break;
            case 2:
                AudioManager.GetInstance().PlaySound("FootStep/FootStep3");
                break;
            case 3:
                AudioManager.GetInstance().PlaySound("FootStep/FootStep4");
                break;
            case 4:
                AudioManager.GetInstance().PlaySound("FootStep/FootStep5");
                break;
        }
    }
}
