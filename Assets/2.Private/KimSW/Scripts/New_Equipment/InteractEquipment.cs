using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class InteractEquipment : MonoBehaviour
{
    [Inject]
    EquipmentManager equipmentManager;

    Transform player;

    [SerializeField] float getRange;

    bool isInteract;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    
        this.UpdateAsObservable()
        .Where(x => (player.position - transform.position).sqrMagnitude <= getRange)
        .Where(x=> !isInteract)
        .Subscribe(x => {
            
                isInteract = true;
                equipmentManager.interactEquipments.Add(this);
            
        });

        this.UpdateAsObservable()
       .Where(x => (player.position - transform.position).sqrMagnitude > getRange)
        .Where(x => isInteract)
       .Subscribe(x => {
          
               isInteract = false;
               equipmentManager.interactEquipments.Remove(this);
           
       });

    }

 
    public void RemoveInstance()
    {
        gameObject.SetActive(false);
    }


}
