using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<DoorTrigger> DoorTriggers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = true;
        foreach (var trigger in DoorTriggers)
        {
            if (!trigger.open)
            {
                flag = false;
                break;
            }
        }

        if (flag)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            transform.gameObject.SetActive(true);
        }
    }
}
