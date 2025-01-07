using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;

public class EquipmentManager : MonoBehaviour
{

    [Inject]
    InGameUI inGameUI;

    public List<InteractEquipment> interactEquipments = new List<InteractEquipment>();

    public List<NewEquipment> newEquipments = new List<NewEquipment>();
    
    public List<NewEquipment> equipped = new List<NewEquipment>();
    public int[] equippedLevel = new int[5];

    private void Start()
    {
        SetInteract();
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

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (newEquipments.Count > 0)
            {
                AddEquipped(GetEquipment());
            }
            else
            {
                Debug.Log("장비 풀 골드획득");
            }
            
        }
        */
    }

    public NewEquipment GetEquipment()
    {
        return newEquipments[Random.Range(0, newEquipments.Count)];
    }

    public void AddEquipped(NewEquipment newEquipment)
    {

       for(int i = 0; i < equipped.Count; i++)
        {
            if (equipped[i].id.Equals(newEquipment.id))
            {

                equippedLevel[i]++;

                // 장착중인 아이템의 레벨이 최대치가 되었을때 해당 아이템 리스트에서 제거
                if (equippedLevel[i] >= newEquipment.upgradeLevel)
                {
                    newEquipments.Remove(newEquipment);
                }

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

                return;
            }

        }

        equipped.Add(newEquipment);
        equippedLevel[equipped.Count - 1]++;

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
     .Subscribe(x =>
     {

         inGameUI.EquipmentGetPanel.gameObject.SetActive(true);


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


}