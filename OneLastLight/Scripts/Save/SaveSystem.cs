using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Path = System.IO.Path;


public class SaveSystem {
    //存档
    public static void SaveByJson(string saveFileName, object data) {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);


#if UNITY_EDITOR
        Debug.Log($"Susscessfully saved data to {path}.");
#endif
    }


    //读取存档
    public static T LoadFromJson<T>(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        var json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<T>(json);
        return data;
    }

    //删除存档
    public static void DeleteSave(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.Delete(path);
    }

    // public static void SaveByPlayerPrefs(string key, object data) {
    //     var json = JsonUtility.ToJson(data);
    //     PlayerPrefs.SetString(key, json);
    //     PlayerPrefs.Save();
    // }
    //
    // public static string LoadFromPlayerPrefs(string key) {
    //     return PlayerPrefs.GetString(key, null);
    // }
}