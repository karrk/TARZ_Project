using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class InteractEquipment : MonoBehaviour, IInteractable
{
    private InGameUI gameUI;

    Transform player;

    [SerializeField] float getRange;

    bool isInteract;

    private void Start()
    {
        gameUI = FindObjectOfType<InGameUI>();
        SetInteractObservable();
    }

    public void SetInteractObservable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        this.UpdateAsObservable()
        .Where(x => (player.position - transform.position).sqrMagnitude <= getRange)
        .Where(x => !isInteract)
        .Subscribe(x => {

            isInteract = true;
            gameUI.EquipmentManager.interactEquipments.Add(this);

        });

        this.UpdateAsObservable()
       .Where(x => (player.position - transform.position).sqrMagnitude > getRange)
        .Where(x => isInteract)
       .Subscribe(x => {

           isInteract = false;
           gameUI.EquipmentManager.interactEquipments.Remove(this);

       });

    }

    public void RemoveInstance()
    {
        gameObject.SetActive(false);
    }


}
