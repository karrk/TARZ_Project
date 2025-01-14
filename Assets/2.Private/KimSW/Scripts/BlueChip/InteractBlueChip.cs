using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class InteractBlueChip : MonoBehaviour, IInteractable
{
    [Inject]
    InGameUI inGameUI;

   
    [Inject]
    StaticBluechip staticBluechip;

    Transform player;

    [SerializeField] float getRange;
    bool isInteract;
    [SerializeField]  bool isPossess;

    [SerializeField] BlueChip[] blueChips;
    [SerializeField] float[] probability;

    public BlueChip blueChip;

    private void Awake()
    {
        SetProbability();

        blueChip = blueChips[GetProbability()];
    }

    private void Start()
    {
        CheckPossess();
        SetInteractObservable();
    }

    void CheckPossess()
    {
        // 블루칩 보유 확인
        foreach (bool check in staticBluechip.bluechipCheck)
        {
            if (check)
            {
                isPossess = true;
            }
        }

    }

    void SetProbability()
    {
        probability = new float[blueChips.Length];
        for (int i = 0; i < blueChips.Length; i++)
        {
            probability[i] = blueChips[i].dropRate;
        }
    }

    public int GetProbability()
    {
        float ran = Random.Range(1, 101);

        int result = 0;
        float temp = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            if (i == 0)
            {
                if (0 <= ran && ran <= probability[0])
                {

                    result = 0;
                    break;
                }
            }
            else
            {
                temp += probability[i - 1];

                if (temp <= ran && ran <= temp + probability[i])
                {
                    result = i;
                }
            }
        }

        return result;
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
            CheckPossess();
            isInteract = true;


            if (isPossess)
            {
                inGameUI.BlueChipSelectPanel.chip = this;

                int num = 0;

                for(int i =0; i<staticBluechip.bluechipCheck.Length; i++)
                {
                    if (staticBluechip.bluechipCheck[i])
                    {
                        num = i;
                    }
                }

                inGameUI.BlueChipSelectPanel.SetInfo( blueChips[num], blueChip);

                inGameUI.StatusBarPanel.OffUIPanel();

                inGameUI.CurrentMenu = inGameUI.BlueChipSelectPanel;

                inGameUI.CurrentMenu.OpenUIPanel();

                
            }
            else
            {
                inGameUI.CurrentMenu = inGameUI.BlueChipGetPanel;

                inGameUI.BlueChipGetPanel.SetInfo(blueChip);

                inGameUI.CurrentMenu.OpenUIPanel();

            }

        });

        this.UpdateAsObservable()
       .Where(x => (player.position - transform.position).sqrMagnitude > getRange)
        .Where(x => isInteract)
       .Subscribe(x =>
       {
           CheckPossess();
           isInteract = false;

           if (isPossess)
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
    .Where(x=> !isPossess)
    .Where (x=> inGameUI.CurrentMenu.Equals(inGameUI.BlueChipGetPanel))
    .Subscribe(x =>
    {

        // 뉴 인풋 시스템으로 변경
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeChip();

            inGameUI.CurrentMenu.CloseUIPanel();
            RemoveInstance();
        }

    });
    }

    public void ChangeChip()
    {
        for (int i = 0; i < staticBluechip.bluechipCheck.Length; i++)
        {
            staticBluechip.bluechipCheck[i] = false;  
        }

        staticBluechip.bluechipCheck[(int)blueChip.type] = true;
    }

    public void RemoveInstance()
    {
        staticBluechip.SetBlueChip();
        Destroy(gameObject);
    }

}
