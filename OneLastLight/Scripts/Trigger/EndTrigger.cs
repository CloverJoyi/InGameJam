using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject StoryWindow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StoryWindow.SetActive(true);
            StoryWindow.GetComponent<StoryWindow>().End();
        }
    }
}
