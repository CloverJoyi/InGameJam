using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public static ChangeMusic instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.name)
        {
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
    }

    public void StartMusic()
    {
        AudioManager.GetInstance().PlayBGM("Story");
    }

    public void EndMusic()
    {
        AudioManager.GetInstance().PlayBGM("End");
    }

    public void AmazingMusic()
    {
        AudioManager.GetInstance().PlayBGM("Amazing");
    }

    public void RelaxMusic()
    {
        AudioManager.GetInstance().PlayBGM("Relax");
    }
    public void DangerousMusic()
    {
        AudioManager.GetInstance().PlayBGM("Dangerous");
    }
}
