using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class EquipmentManager : MonoBehaviour
{

    [Inject]
    InGameUI inGameUI;

    [Inject]
    StaticEquipment staticEquipment;

    [SerializeField] EquipmentCSVParser csvParser;

    public List<InteractEquipment> interactEquipments = new List<InteractEquipment>();

    public List<NewEquipment> newEquipments = new List<NewEquipment>();
    
    public List<NewEquipment> equipped = new List<NewEquipment>();
    public int[] equippedLevel = new int[5];

    [SerializeField] float[] rarityProbability;

    List<NewEquipment> getEquipmentList = new List<NewEquipment>();
    private void Start()
    {
        staticEquipment.manager = this;

        SetInteract();

        LoadEquipment();

    }

    private void OnDestroy()
    {
        staticEquipment.manager = null;

        SaveEquipment();
    }

    void LoadEquipment()
    {
        if (staticEquipment.firstInit)
        {
            newEquipments = staticEquipment.newEquipments;
            equipped = staticEquipment.equipped;
            equippedLevel = staticEquipment.equippedLevel;

            for (int i = 0; i < equipped.Count; i++)
            {
                inGameUI.EquipmentSelectPanel.ChangeSlot(i);
                inGameUI.EquipmentSelectPanel.LevelUpSlot(i, equippedLevel[i]);
            }


        }
        else
        {
            newEquipments = csvParser.newEquipments;
            staticEquipment.firstInit = true;

            staticEquipment.newEquipments = newEquipments;

        }
    }

    void SaveEquipment()
    {
        staticEquipment.newEquipments = newEquipments;
        staticEquipment.equipped = equipped;
        staticEquipment.equippedLevel = equippedLevel;
    }


    // 2. 장착중인 아이템의 등급보다 낮은 같은 타입의 아이템들 제거


    //  아이템 가득 찼을시 해당 되지 않는 아이템 타입의 아이템들 제거
    public void EquippedFullSizeCheck()
    {
        for (int i = newEquipments.Count - 1; i >= 0; i--)
        {
           
                bool isPossess = false;
                foreach (var equippedItem in equipped)
                {
                    if (equippedItem.equipmentType.Equals(newEquipments[i].equipmentType))
                    {
                        isPossess = true;
                    }


                }
                if (!isPossess)
                {
                  newEquipments.Remove(newEquipments[i]);
                }

            
        }
    }

    

    public NewEquipment GetEquipment(int rarity)
    {
        getEquipmentList.Clear();

        foreach (var equipment in newEquipments)
        {
            if((int)equipment.rarityTier == rarity)
            {
                getEquipmentList.Add(equipment);
            }
        }

        if (getEquipmentList.Count > 0)
        {

            return getEquipmentList[Random.Range(0, getEquipmentList.Count)];
        }
        else
        {
            return null;
        }

    }

    public void AddEquipped(NewEquipment newEquipment)
    {
        SetProbability();
        for (int i = 0; i < equipped.Count; i++)
        {
            if (equipped[i].id.Equals(newEquipment.id))
            {

                equippedLevel[i]++;

                // 장착중인 아이템의 레벨이 최대치가 되었을때 해당 아이템 리스트에서 제거
                if (equippedLevel[i] >= newEquipment.upgradeLevel)
                {
                    newEquipments.Remove(newEquipment);
                }
                inGameUI.EquipmentSelectPanel.LevelUpSlot(i, equippedLevel[i]);
                staticEquipment.InvokeOnChangedEq();
                return;
            }
            // 높은 등급의 아이템이 왔을때 교체 되도록
            else if (equipped[i].equipmentType.Equals(newEquipment.equipmentType))
            {
                equipped[i] = newEquipment;
                RemoveLowRarity(newEquipment);

                //레벨 초기화
                equippedLevel[i] = 1;
                if (newEquipment.upgradeLevel <= equippedLevel[i])
                {
                    newEquipments.Remove(newEquipment);
                }

                inGameUI.EquipmentSelectPanel.ChangeSlot(i);
                inGameUI.EquipmentSelectPanel.LevelUpSlot(i, equippedLevel[i]);
                staticEquipment.InvokeOnChangedEq();
                return;
            }

        }

        equipped.Add(newEquipment);
        equippedLevel[equipped.Count - 1]++;

        inGameUI.EquipmentSelectPanel.SetSlot(equipped.Count - 1);
        inGameUI.EquipmentSelectPanel.LevelUpSlot(equipped.Count - 1, equippedLevel[equipped.Count - 1]);

        // 1레벨이 최대치면 습득 하자마자 리스트에서 제거
        if (newEquipment.upgradeLevel <= equippedLevel[equipped.Count-1])
        {
            newEquipments.Remove(newEquipment);
        }


        RemoveLowRarity(newEquipment);

        if (equipped.Count == 5)
        {
            EquippedFullSizeCheck();
        }

        staticEquipment.InvokeOnChangedEq();
    }


  
    // 낮은 레어리티 아이템 제거
    void RemoveLowRarity(NewEquipment newEquipment)
    {
        for(int i = newEquipments.Count-1 ; i >= 0; i--)
        {
            if (newEquipments[i].equipmentType == newEquipment.equipmentType && 
                newEquipments[i].rarityTier <= newEquipment.rarityTier && 
                newEquipments[i].id != newEquipment.id)
            {
                newEquipments.Remove(newEquipments[i]);
            }
        }
      
    }
    void SetInteract()
    {
        this.UpdateAsObservable()
     .Where(x => interactEquipments.Count > 0)
     .Where(x=>inGameUI.CurrentMenu.Equals(inGameUI.StatusBarPanel))
     .Subscribe(x =>
     {
        

         inGameUI.EquipmentGetPanel.gameObject.SetActive(true);

         // 뉴 인풋 시스템으로 변경
         if (Input.GetKeyDown(KeyCode.F))
         {
           
             
             interactEquipments[0].RemoveInstance();
             interactEquipments.Remove(interactEquipments[0]);
             inGameUI.EquipmentGetPanel.gameObject.SetActive(false);

             inGameUI.CurrentMenu = inGameUI.EquipmentSelectPanel;
             inGameUI.CurrentMenu.OpenUIPanel();

         }

     });


        this.UpdateAsObservable()
          .Where(x => interactEquipments.Count == 0)
          .Subscribe(x =>
          {
              inGameUI.EquipmentGetPanel.gameObject.SetActive(false);

          });
    }


    public void SetProbability()
    {
        for (int i = 0; i < rarityProbability.Length; i++)
        {
            bool isStock = false;
            foreach (var item in newEquipments)
            {
                if ((int)item.rarityTier == i )
                {
                    isStock = true;
                }

            }

            if (!isStock)
            {
                if (rarityProbability[i] > 0)
                {
                    if (rarityProbability.Length - 1 == i)
                    {
                        if (rarityProbability[0] < 0)
                        {
                            rarityProbability[0] = 0;
                        }

                        rarityProbability[0] += rarityProbability[i];
                        rarityProbability[i] = 0;

                        i = 0;
                        continue;
                    }
                    else
                    {
                        if(rarityProbability[i + 1] < 0)
                        {
                            rarityProbability[i + 1] = 0;
                        }

                        rarityProbability[i + 1] += rarityProbability[i];
                        rarityProbability[i] = 0;
                        i--;
                        continue;
                    }
                }
            }
        }

    }
    public int GetProbability()
    {
        SetProbability();

        float ran = Random.Range(1, 101);

        int result = 0;
        float temp = 0;
        for (int i = 0; i < rarityProbability.Length; i++)
        {
            if (i == 0)
            {
                if (0 <= ran && ran <= rarityProbability[0])
                {
                    
                    result = 0;
                    break;
                }
            }
            else
            {
                temp += rarityProbability[i - 1];
               
                if (temp <= ran && ran <= temp + rarityProbability[i])
                {
                    result = i;
                }
            }
        }


        

        return result;
    }
}
