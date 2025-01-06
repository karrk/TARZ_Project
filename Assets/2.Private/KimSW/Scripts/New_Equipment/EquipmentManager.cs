using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class EquipmentManager : MonoBehaviour
{

    [Inject]
    InGameUI gameUI;

    public List<InteractEquipment> interactEquipments = new List<InteractEquipment>();


    private void Start()
    {
        this.UpdateAsObservable()
     .Where(x => interactEquipments.Count > 0)
     .Subscribe(x =>
     {

         gameUI.EquipmentGetPanel.gameObject.SetActive(true);


         if (Input.GetKeyDown(KeyCode.F))
         {
             interactEquipments[0].RemoveInstance();
             interactEquipments.Remove(interactEquipments[0]);
             gameUI.EquipmentGetPanel.gameObject.SetActive(false);
             gameUI.EquipmentSelectPanel.gameObject.SetActive(true);
         }

     });


        this.UpdateAsObservable()
          .Where(x => interactEquipments.Count == 0)
          .Subscribe(x =>
          {
              gameUI.EquipmentGetPanel.gameObject.SetActive(false);

          });
            }


}
