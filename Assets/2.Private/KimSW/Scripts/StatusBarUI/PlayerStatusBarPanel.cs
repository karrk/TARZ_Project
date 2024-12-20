using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerStatusBarPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;


    private PlayerHpSliderView playerHpSliderView;
    private PlayerStaminaSliderView playerStaminaSliderView;
    private PlayerSkillSliderView playerSkillSliderView;
    private PlayerExpView playerExpView;
    private GarbageInventoryView garbageInventoryView;


    public PlayerHpSliderView PlayerHpSliderView { get { return playerHpSliderView; } }
    public PlayerStaminaSliderView PlayerStaminaSliderView { get { return playerStaminaSliderView; } }
    public PlayerSkillSliderView PlayerSkillSliderView { get { return playerSkillSliderView; } }

    public PlayerExpView PlayerExpView { get { return playerExpView; } }
    public GarbageInventoryView GarbageInventoryView { get { return garbageInventoryView; } }



    [SerializeField] AnimatedUI[] animatedUIs;


    private void Awake()
    {
        playerHpSliderView = GetComponentInChildren<PlayerHpSliderView>();
        playerStaminaSliderView = GetComponentInChildren<PlayerStaminaSliderView>();
        playerSkillSliderView = GetComponentInChildren<PlayerSkillSliderView>();
        playerExpView = GetComponentInChildren<PlayerExpView>();
        garbageInventoryView = GetComponentInChildren<GarbageInventoryView>();

    }
  


    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOnUI();
        }

    }

    public void CloseUIPanel()
    {
        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOffUI();
        }

        inGameUI.CurrentMenu = inGameUI.InGameMenuPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
    }

    public void OffUIPanel()
    {
        for (int i = 0; i < animatedUIs.Length; i++)
        {
            animatedUIs[i].MoveOffUI();
        }
    }
}

