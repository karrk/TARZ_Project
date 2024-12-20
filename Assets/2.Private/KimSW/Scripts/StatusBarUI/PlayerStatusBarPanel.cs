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
    }

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);
        inGameUI.CurrentMenu = inGameUI.InGameMenuPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
    }
}

