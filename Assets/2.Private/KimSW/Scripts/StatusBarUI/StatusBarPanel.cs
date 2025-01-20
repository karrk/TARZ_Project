using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StatusBarPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;


    private PlayerHpSliderView playerHpSliderView;
    private PlayerStaminaSliderView playerStaminaSliderView;
    private PlayerSkillSliderView playerSkillSliderView;
    private PlayerExpView playerExpView;
    private GarbageInventoryView garbageInventoryView;
    private CoolTimeView coolTimeView;

    public PlayerHpSliderView PlayerHpSliderView { get { return playerHpSliderView; } }
    public PlayerStaminaSliderView PlayerStaminaSliderView { get { return playerStaminaSliderView; } }
    public PlayerSkillSliderView PlayerSkillSliderView { get { return playerSkillSliderView; } }

    public PlayerExpView PlayerExpView { get { return playerExpView; } }
    public GarbageInventoryView GarbageInventoryView { get { return garbageInventoryView; } }
    public CoolTimeView CoolTimeView { get { return coolTimeView; } }


    [SerializeField] AnimatedUI[] animatedUIs;


    private void Awake()
    {
        playerHpSliderView = GetComponentInChildren<PlayerHpSliderView>();
        playerStaminaSliderView = GetComponentInChildren<PlayerStaminaSliderView>();
        playerSkillSliderView = GetComponentInChildren<PlayerSkillSliderView>();
        playerExpView = GetComponentInChildren<PlayerExpView>();
        garbageInventoryView = GetComponentInChildren<GarbageInventoryView>();
        coolTimeView = GetComponentInChildren<CoolTimeView>();
    }
  


    public void OpenUIPanel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(true);
        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOnUI();
        }

        inGameUI.OnEnemyHP();
    }

    public void CloseUIPanel()
    {
        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOffUI();
        }

        inGameUI.CurrentMenu = inGameUI.InGameMenuPanel;
        inGameUI.CurrentMenu.OpenUIPanel();


        inGameUI.OffEnemyHP();
    }

    public void OffUIPanel()
    {
       

        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOffUI();
        }

        inGameUI.OffEnemyHP();
    }
}

