using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class EquipmentManager : MonoBehaviour
{

    [Inject]
    InGameUI inGameUI;

    public List<InteractEquipment> interactEquipments = new List<InteractEquipment>();


    private void Start()
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
