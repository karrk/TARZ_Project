using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using System.IO;
public class SaveData
{
   
    public int exp;
    public bool[] passiveEnable;
    public int[] equipPassiveID;
}

public class LobbyData : MonoBehaviour
{
    [Inject]
    PlayerUIModel model;

 
    public int exp;
    public bool[] passiveEnable;
    public PassiveInfo[] equipPassive;

    public List<PassiveInfo> passives = new List<PassiveInfo>();

    public SaveData saveData = new SaveData(); // 플레이어 데이터 생성

    void Start()
    {
        equipPassive = new PassiveInfo[3];
        LoadData();
        SetExp();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    public void SaveData()
    {
        saveData.exp = exp;
        saveData.passiveEnable = passiveEnable;
        saveData.equipPassiveID = new int[equipPassive.Length];
        for (int i = 0; i < equipPassive.Length; i++)
        {
            if (equipPassive[i] == null)
            {
                saveData.equipPassiveID[i] = 0;
            }
            else
            {
                saveData.equipPassiveID[i] = equipPassive[i].id;
            }
           
        }
      

#if UNITY_EDITOR
        string path = $"{Application.dataPath}/2.Private/KimSW/Json";
#else
        string persPath = Application.persistentDataPath; 
#endif
        string data = JsonUtility.ToJson(saveData);
        File.WriteAllText($"{path}/Save1.json", data);
    }

    public void LoadData()
    {
#if UNITY_EDITOR
        string path = $"{Application.dataPath}/2.Private/KimSW/Json";
#else
        string persPath = Application.persistentDataPath; 
#endif

        if (File.Exists($"{path}/Save1.json") == false)
        {
            Debug.LogError("파일이 없습니다");
            return;
        }


        string data = File.ReadAllText($"{path}/Save1.json");
        saveData = JsonUtility.FromJson<SaveData>(data);

        exp = saveData.exp;
        passiveEnable = saveData.passiveEnable;

        for (int i = 0; i < equipPassive.Length; i++)
        {
            if (saveData.equipPassiveID[i] == 0)
            {
                equipPassive[i] = null;
            }
            else
            {
                equipPassive[i] = passives[saveData.equipPassiveID[i]-1];
            }

        }
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveData();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetExp();
    }

    public void SetExp()
    {
        model.TargetEXP.Value = exp;
    }
   
}
