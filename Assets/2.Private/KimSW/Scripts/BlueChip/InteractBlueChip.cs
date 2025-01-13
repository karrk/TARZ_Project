using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class InteractBlueChip : MonoBehaviour, IInteractable
{
    [Inject]
    InGameUI inGameUI;

    Transform player;

    [SerializeField] float getRange;
    bool isInteract;
    [SerializeField]  bool isFull;

    private void Start()
    {
        SetInteractObservable();
    }

    public void SetInteractObservable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        this.UpdateAsObservable()
        .Where(x => (player.position - transform.position).sqrMagnitude <= getRange)
          .Where(x => !isInteract)
          .Where(x => inGameUI.CurrentMenu.Equals(inGameUI.StatusBarPanel))
        .Subscribe(x =>
        {
            isInteract = true;


            if (isFull)
            {
                inGameUI.StatusBarPanel.OffUIPanel();

                inGameUI.CurrentMenu = inGameUI.BlueChipSelectPanel;

                inGameUI.CurrentMenu.OpenUIPanel();

                inGameUI.BlueChipSelectPanel.chip = this;
            }
            else
            {
                inGameUI.CurrentMenu = inGameUI.BlueChipGetPanel;

                inGameUI.CurrentMenu.OpenUIPanel();

            }

        });

        this.UpdateAsObservable()
       .Where(x => (player.position - transform.position).sqrMagnitude > getRange)
        .Where(x => isInteract)
       .Subscribe(x =>
       {
           isInteract = false;

           if (isFull)
           {
               inGameUI.CurrentMenu.CloseUIPanel();

               inGameUI.BlueChipSelectPanel.chip = null;
           }
           else
           {
               inGameUI.CurrentMenu = inGameUI.BlueChipGetPanel;
               inGameUI.CurrentMenu.CloseUIPanel();
           }
       });


        this.UpdateAsObservable()
    .Where(x => isInteract)
    .Where(x=> !isFull)
    .Where (x=> inGameUI.CurrentMenu.Equals(inGameUI.BlueChipGetPanel))
    .Subscribe(x =>
    {

        // 뉴 인풋 시스템으로 변경
        if (Input.GetKeyDown(KeyCode.F))
        {
            inGameUI.CurrentMenu.CloseUIPanel();
            RemoveInstance();
        }

    });
    }

    public void RemoveInstance()
    {
        gameObject.SetActive(false);
    }

}
