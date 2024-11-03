using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
/// <summary>
/// 单例Mono,Awake重写后使用
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
    /// <summary>
    /// //非getinstance产生，就必须调用这个方法
    /// </summary>
    /// <param name="var"></param>
    public static void SetInstance(GameObject var)
    {
        instance = var.GetComponent<T>();
    }
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
}
