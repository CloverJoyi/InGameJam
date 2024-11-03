using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolList//封装容器
{
    private GameObject type;
    public List<GameObject> pool ;

    public PoolList(GameObject target,GameObject billboard)
    {
        type = new GameObject(target.name);//创建一个目标类型名字的空物体当做容器
        type.transform.SetParent(billboard.transform);//将容器放到对象池billboard
        pool = new List<GameObject>();//实例化对应对象池
        PushObj(target);
        
    }
    public void PushObj(GameObject obj)
    {
        pool.Add(obj);
        obj.transform.SetParent(type.transform);//将目标放入容器
        obj.SetActive(false);//将目标设为不显示
    }
    public GameObject GetObj()
    {
        GameObject obj = pool[0];
        pool.RemoveAt(0);
        return obj;
    }
}
public interface SinglePool{}

public class SinglePool<T>: SinglePool//封装容器，储存独特对象
{
    public Dictionary<string, T> pool = new Dictionary<string, T>();

    public T GetObj(string key)
    {
        T temp = pool[key];
        pool.Remove(key);
        return temp;
    }
}
/// <summary>
/// 对象池
/// </summary>
public class ObjectPool: Singleton<ObjectPool>
{
    public GameObject billboard;
    /// <summary>
    /// 重复池，key为类型名，一个字典储存多种同质对象
    /// </summary>
    private Dictionary<string, PoolList> RepeatObjPool = new Dictionary<string, PoolList>();
    /// <summary>
    /// 非重复池，key为类型名，一个字典储存多种独特对象
    /// </summary>
    private Dictionary<string, SinglePool> SingleObjPool = new Dictionary<string, SinglePool>();
    /// <summary>
    /// 通过资源文件从对象池获取对象
    /// </summary>
    /// <param name="targetName">文件名</param>
    /// <param name="path">路径默认为Resources根目录</param>
    /// <returns></returns>
    public GameObject GetObj(string targetName, string path=null)
    {
        GameObject temp = null;
        if (RepeatObjPool.ContainsKey(targetName)&&RepeatObjPool[targetName].pool.Count>0)
        {
            temp = RepeatObjPool[targetName].GetObj();
        }
        else
        {
            temp = GameObject.Instantiate(Resources.Load<GameObject>(path+targetName));
            temp.name = targetName;
        }
        return temp;
    }
    /// <summary>
    /// 通过GameObject从对象池获取对象
    /// </summary>
    /// <param name="obj">你的Prafab</param>
    /// <param name="path">路径不是必须</param>
    /// <returns></returns>
    public GameObject GetObj(GameObject obj, string path=null)
    {
        GameObject temp = null;
        if (RepeatObjPool.ContainsKey(obj.name)&&RepeatObjPool[obj.name].pool.Count>0)
        {
            temp = RepeatObjPool[obj.name].GetObj();
        }
        else
        {
            if(path != null)
                temp = GameObject.Instantiate(Resources.Load<GameObject>(path+obj.name));
            else
                temp = GameObject.Instantiate(obj);
            temp.name = obj.name;
        }
        return temp;
    }

    public void PushObj(string listName, GameObject obj)
    {
        if(billboard == null)
        {
            billboard = new GameObject("ObjectPool");
        }

        if (RepeatObjPool.ContainsKey(listName))//字典有对应容器
        {
            RepeatObjPool[listName].PushObj(obj);//调用容器方法
        }
        else
        {
            PoolList newPool = new PoolList(obj, billboard);//实例化容器
            RepeatObjPool.Add(listName, newPool);//添加一个新的容器
        }
    }
    /// <summary>
    /// 获取T类型的一个独特对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetName">目标名称</param>
    /// <returns></returns>
    public T GetObj<T>(string targetName)
    {
        SinglePool<T> temp = (SingleObjPool[typeof(T).Name] as SinglePool<T>);
        if(SingleObjPool.ContainsKey(typeof(T).Name) && temp.pool.Count>0)
        {
            return temp.GetObj(targetName);
        }
        return default(T);
    }
    /// <summary>
    /// 储存T类型的一个独特对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetName">目标名称</param>
    /// <param name="target">目标</param>
    public void PushObj<T>(string targetName, T target)
    {
        if (SingleObjPool.ContainsKey(typeof(T).Name) && !(SingleObjPool[typeof(T).Name] as SinglePool<T>).pool.ContainsKey(typeof(T).Name))//字典有对应容器
        {
            (SingleObjPool[typeof(T).Name] as SinglePool<T>).pool.Add(typeof(T).Name, target);//调用容器方法
        }
        else
        {
            SinglePool<T> newDic = new SinglePool<T>();
            if(!SingleObjPool.ContainsKey(typeof(T).Name))
                SingleObjPool.Add(typeof(T).Name, newDic);//添加一个新的容器
            newDic.pool.Add(targetName, target);
        }
    }
    public void Clear()
    {
        RepeatObjPool.Clear();
        SingleObjPool.Clear();
    }
}
