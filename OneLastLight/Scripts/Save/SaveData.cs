using UnityEngine;

public class SaveData : MonoBehaviour{
    public static SaveData Instance;

    private void Awake(){
        Instance = this;
    }

    public bool _haveLight;
    public bool _haveBlue;
    public StateSave m_stateSave;
    public Vector3 _playerPosition;
    public bool groundHit;
    public Vector3 _backPos;


    #region Save

    [System.Serializable]
    class SaveDataClass{
        public Vector3 playerPosition;
        public bool haveLight;
        public bool haveBlue;
        public StateSave stateSave;
    }

    private const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";


    //Json存档
    void SaveByJson(){
        SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());
    }

    //Json读档
    void LoadFromJson(){
        var saveData = SaveSystem.LoadFromJson<SaveDataClass>(PLAYER_DATA_FILE_NAME);
        LoadData(saveData);
        //LoadPlayerState(saveData);
    }

    void LoadData(SaveDataClass saveData){
        if (saveData != null){
            _playerPosition = saveData.playerPosition;
            _haveLight = saveData.haveLight;
            _haveBlue = saveData.haveBlue;
            m_stateSave = saveData.stateSave;
        }
    }

    SaveDataClass SavingData(){
        var saveData = new SaveDataClass();

        saveData.playerPosition = _playerPosition;
        saveData.haveLight = _haveLight;
        saveData.haveBlue = _haveBlue;
        saveData.stateSave = m_stateSave;
        return saveData;
    }

    // void LoadPlayerState(SaveDataClass saveData) {
    //     if (saveData.haveLight) {
    //         if (saveData.stateSave.lightMode) {
    //             m_stateSave._color = saveData.stateSave._color;
    //             _lightStateController.TransitionState(StateType.LightBeamState);
    //         }
    //         else {
    //             m_stateSave._color = saveData.stateSave._color;
    //             _lightStateController.TransitionState(StateType.LightCircleState);
    //         }
    //     }
    //     else {
    //         m_stateSave._color = saveData.stateSave._color;
    //         _lightStateController.TransitionState(StateType.NoLantern);
    //     }
    // }


    public void Save(){
        SaveByJson();
    }


    public void Load(){
        LoadFromJson();
        GameObject.Find("Player").transform.position = SaveData.Instance._playerPosition;
    }

    public static void DeletePlayerDataSaveFile(){
        SaveSystem.DeleteSave(PLAYER_DATA_FILE_NAME);
    }

    #endregion

    public void SaveBackPos(Transform player){
        _backPos = player.position;
        //Debug.Log("BackPos:" + _backPos);
    }
}

//结构体用于存储灯的状态
public struct StateSave{
    public LightColor _color;
    public float _angle;
    public bool lightMode; //true:Beam false:circle
    public bool isLight;
}