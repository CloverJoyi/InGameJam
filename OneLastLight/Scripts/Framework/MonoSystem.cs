using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Mono中心的实际实现
/// </summary>
public class MonoSystem : SingletonMono<MonoSystem>
{
    // Start is called before the first frame update
    private event UnityAction updateEvent;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void DontDestroy(GameObject obj)
    {
        DontDestroyOnLoad(obj);
    }
    // Update is called once per frame
    void Update()
    {
        if(updateEvent!=null)
        {
            updateEvent();
        }
    }

    public void AddUpdateEventListener(UnityAction listener)
    {
        updateEvent+=listener;
    }

    public void RemoveUpdateEventListener(UnityAction listener)
    {
        updateEvent-=listener;
    }
}
