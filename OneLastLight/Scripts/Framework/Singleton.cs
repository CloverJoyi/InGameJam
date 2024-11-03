using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T :new()
{
    private static T instance;
    /// <summary>
    /// 获取实例以调用单例
    /// </summary>
    /// <returns></returns>
    public static T GetInstance()
    {
        if(instance == null)
            instance = new T();
        return instance;
    }
}
