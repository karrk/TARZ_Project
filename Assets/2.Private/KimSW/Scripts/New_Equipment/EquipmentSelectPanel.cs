using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class EquipmentSelectPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject] InGameUI inGameUI;

    [SerializeField] GameObject firstSelected;

    [SerializeField] EquipmentSelectButton[] buttons;
    [SerializeField] EquipmentSlot[] slots;
    public GameObject slotsPanel;
    [SerializeField] GameObject selectsPanel;

    [SerializeField] TMP_Text rerollCountText;

    public int rerollCount;

    private void Awake()
    {
        buttons = GetComponentsInChildren<EquipmentSelectButton>(true);
        slots = GetComponentsInChildren<EquipmentSlot>(true);
    }

    public void OpenUIPanel()
    {
        inGameUI.StatusBarPanel.OffUIPanel();
        selectsPanel.SetActive(true);
        slotsPanel.SetActive(true);
        SetChoice();
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void CloseUIPanel()
    {
        Time.timeScale = 1.0f;
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
        selectsPanel.SetActive(false);
        slotsPanel.SetActive(false);
    }

    public void ReRoll()
    {
        if (rerollCount <= 0)
            return;
        rerollCount--;
        rerollCountText.text = rerollCount.ToString();

        SetChoice();
    }

    public void SetChoice()
    {
        int debug = 0;

        // 선택지가 없을때
        if (inGameUI.EquipmentManager.newEquipments.Count == 0){
            SetGoldSlot();
           

            return;
        }

        // 확률 적용
        int[] rarityProbability = new int[3];

        // 티어 별 남은 장비 개수
        int[] rarityCount = new int[(int)RarityTier.SIZE];

        //for (int i = 0; i < inGameUI.EquipmentManager.newEquipments.Count; i++)
        //{
        //    Debug.LogError($"순회 : {inGameUI.EquipmentManager.newEquipments[i]}");
        //    Debug.LogError($"이름 : {inGameUI.EquipmentManager.newEquipments[i].equipmentName}");
        //}

        //Debug.LogError($"인게임 UI {inGameUI}");
        //Debug.LogError($"equipment {inGameUI.EquipmentManager}");
        //Debug.LogError($"new equipment {inGameUI.EquipmentManager.newEquipments}"); // 씬 전환시 해당 데이터가 날아감

        for(int i = 0; i < inGameUI.EquipmentManager.newEquipments.Count; i++)
        {
            if(inGameUI.EquipmentManager.newEquipments[i] == null) // 에디터에선 안뜸
            {

                Debug.LogError($"빌드에선 뜸 {i}   {inGameUI.EquipmentManager.newEquipments.Count}");
            }

            //Debug.LogError($"갯수 {rarityCount.Length}");
            //Debug.LogError($"내부 {inGameUI.EquipmentManager.newEquipments[i]}");
            //Debug.LogError($"티어 {(int)inGameUI.EquipmentManager.newEquipments[i].rarityTier}");
            
            rarityCount[(int)inGameUI.EquipmentManager.newEquipments[i].rarityTier]++;
           
        }



        for (int i = 0; i < rarityProbability.Length; i++)
        {
            rarityProbability[i] = inGameUI.EquipmentManager.GetProbability();

        }
      
        List<NewEquipment> newEquipments = new List<NewEquipment>();

        for (int i = 0; i < 3; i++)
        {
            NewEquipment equipment = inGameUI.EquipmentManager.GetEquipment(rarityProbability[i]);
     

            if (equipment == null)
            {
              
                Debug.Log("null값 발생 " + i);
                Debug.Log(rarityProbability[i]);
                SetGoldSlot();
                return;
            }

            debug++;
            if (debug > 1000)
            {
                Debug.Log("무한루프발생!");
                Debug.Log(i);
                Debug.Log(equipment.rarityTier);
                Debug.Log(rarityCount[(int)equipment.rarityTier]);
                Debug.Log("무한루프발생");
                SetGoldSlot();
                return;
            }
            /*
            //레어도 체크
            if (rarityProbability[i] != (int)equipment.rarityTier)
            {
                i--;
                continue;
            }*/

            //중복체크
            bool overlap = false;

        

            foreach (NewEquipment equipmentItem in newEquipments)
            {
                if (equipmentItem.id == equipment.id)
                {
                    overlap = true;
                    break;
                }

               

            }


            if (!overlap)
            {
                newEquipments.Add(equipment);
            }
            else
            {
                // 선택지가 3개 미만일시 중복이여도 추가
                if(inGameUI.EquipmentManager.newEquipments.Count < 3)
                {
                    newEquipments.Add(equipment);
                }
                // 해당 티어의 남은 장비가 3개 미만일시 중복이여도 추가
                else if (rarityCount[(int)equipment.rarityTier] < 3)
                {
                    newEquipments.Add(equipment);
                }

                // 중복
                else
                {
                    i--;
                    continue;
                }

               
            }

        }

        for (int i = 0;i < buttons.Length; i++)
        {
            buttons[i].SetInfo(newEquipments[i]);
        }

      
    }

    public void SetGoldSlot()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetGold();
        }
    }

    public void SetSlot(int num)
    {
        
        slots[num].SetSlot(inGameUI.EquipmentManager.equipped[num]);
        
    }

    public void ChangeSlot(int num)
    {
        slots[num].ChangeSlot(inGameUI.EquipmentManager.equipped[num]);
    }

    public void LevelUpSlot(int num, int level)
    {
        slots[num].LevelUpSlot(level);
    }
}
