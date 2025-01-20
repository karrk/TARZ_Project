using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using System.IO;
using System;
public class SaveData
{

    public int exp;
    public bool[] passiveEnable;
    public int[] equipPassiveID;
    public bool[] achieves;
    public bool[] equipSlotEnable;
}

public class LobbyData : MonoBehaviour
{
    [Inject]
    PlayerUIModel model;


    public int exp;
    public bool[] passiveEnable;

    public PassiveInfo[] equipPassive = new PassiveInfo[3];

    public List<PassiveInfo> passives = new List<PassiveInfo>();

    public bool[] achieves = new bool[5];

    public bool[] equipSlotEnable = new bool[3];

    public SaveData saveData = new SaveData(); // 플레이어 데이터 생성

    public int saveNumber;

    public event Action OnChanged;

    void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void SetSaveDataNumber(int num)
    {
        saveNumber = num;
    }

    public void NewSaveData()
    {
        saveData.exp = 0;

        // 추가

        saveData.passiveEnable = new bool[passiveEnable.Length];
        for (int i = 0; i < passiveEnable.Length; i++)
        {
            saveData.passiveEnable[i] = false;
        }

        saveData.equipPassiveID = new int[equipPassive.Length];
        for (int i = 0; i < equipPassive.Length; i++)
        {
            saveData.equipPassiveID[i] = 0;
        }

        saveData.achieves = new bool[achieves.Length];
        for (int i = 0; i < achieves.Length; i++)
        {
            saveData.achieves[i] = false;
        }

        saveData.equipSlotEnable = new bool[3];
        saveData.equipSlotEnable[0] = true;

        WriteData();
    }

    public void SaveData()
    {
        saveData.exp = exp;
        saveData.passiveEnable = passiveEnable;
        // 추가
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

        saveData.achieves = achieves;

        saveData.equipSlotEnable = equipSlotEnable;

        WriteData();
    }

    public void Renewal()
    {
        OnChanged?.Invoke();
    }

    public void WriteData()
    {

#if UNITY_EDITOR
        string path = $"{Application.dataPath}/2.Private/KimSW/Json";
#else
        string path = Application.persistentDataPath; 
#endif
        string data = JsonUtility.ToJson(saveData);
        File.WriteAllText($"{path}/Save{saveNumber}.json", data);

        OnChanged?.Invoke();
    }

    public void LoadData(int num)
    {
#if UNITY_EDITOR
        string path = $"{Application.dataPath}/2.Private/KimSW/Json";
#else
        string path = Application.persistentDataPath; 
#endif

        if (File.Exists($"{path}/Save{num + 1}.json") == false)
        {
            Debug.LogError("파일이 없습니다");
            return;
        }

        saveNumber = num + 1;

        string data = File.ReadAllText($"{path}/Save{num + 1}.json");
        saveData = JsonUtility.FromJson<SaveData>(data);

        exp = saveData.exp;

        passiveEnable = saveData.passiveEnable;
        achieves = saveData.achieves;
        equipSlotEnable = saveData.equipSlotEnable;

        for (int i = 0; i < equipPassive.Length; i++)
        {
            if (saveData.equipPassiveID[i] == 0)
            {
                equipPassive[i] = null;
            }
            else
            {
                equipPassive[i] = passives[saveData.equipPassiveID[i] - 1];
            }
        }

        OnChanged?.Invoke();

        SetExp();
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetExp();
        if (equipSlotEnable == null)
        {
            return;
        }
        equipSlotEnable[0] = true;
    }

    public void SetExp()
    {
        model.TargetEXP.Value = exp;
    }

}