using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Mono管理中心，处理非Mono的协程和帧更新
/// </summary>
public class MonoCenter : Singleton<MonoCenter>
{
    public MonoSystem monoSys;//外部请不要调用MonoSystem
    
    public MonoCenter()
    {
        GameObject obj = new GameObject("MonoSystem");
        monoSys = obj.AddComponent<MonoSystem>();
        
    }

    public void DontDestroyOnLoad(GameObject obj)
    {
        monoSys.DontDestroy(obj);
    }
    public void AddUpdateEventListener(UnityAction listener)
    {
        monoSys.AddUpdateEventListener(listener);
    }

    public void RemoveUpdateEventListener(UnityAction listener)
    {
        monoSys.RemoveUpdateEventListener(listener);
    }

    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoSys.StartCoroutine(coroutine);
    }
    
    public Coroutine StartCoroutine(string methodName,[DefaultValue("null")] object value)
    {
        return monoSys.StartCoroutine(methodName,value);
    }
    public Coroutine StartCoroutine(string methodName)//无法用string开启monoSys外的协程
    {
        return monoSys.StartCoroutine(methodName);
    }
}
