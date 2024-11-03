using LeTai.TrueShadow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        

        AudioManager.GetInstance().PlaySound("UI/Select");
        gameObject.GetComponent<TrueShadow>().enabled = true;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
         gameObject.GetComponent<TrueShadow>().enabled = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.GetInstance().PlaySound("UI/Click");
        gameObject.GetComponent<TrueShadow>().enabled = true;
    }

    public string Search_string(string s, string s1, string s2)
    {
        int n1, n2;
        n1 = s.IndexOf(s1, 0) + s1.Length;
        n2 = s.IndexOf(s2, n1);
        return s.Substring(n1, n2 - n1);
    }

}
