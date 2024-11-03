using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 资源管理器
/// </summary>
public class ResManager : Singleton<ResManager>
{
    // Start is called before the first frame update
    public T Load<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        if(res is GameObject)
            return GameObject.Instantiate(res);
        return res;
    }
    
    public void LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
    {
        MonoCenter.GetInstance().StartCoroutine(LoadAsyncCoroutine<T>(path, callback));
    }

    private IEnumerator LoadAsyncCoroutine<T>(string path, UnityAction<T> callback) where T : Object
    {
        ResourceRequest res = Resources.LoadAsync<T>(path);
        yield return res;
        if(res.asset is GameObject)
            callback(GameObject.Instantiate(res.asset)as T);
        else
            //非GameObject资源
            callback(res.asset as T);

    }
}
