using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerStatusBarPanel : FadePanel
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

        SetComponent();
    }
    public override void SetComponent()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TMP_Text>();
    }



    public override void FadeOutUI()
    {
   
        foreach (Image image in images)
        {
            image.DOKill();
            image.DOFade(0, fadeOutDuration).OnComplete(()=>playerHpSliderView.ResetColor());
         
        }

        foreach(TMP_Text text in texts)
        {
            text.DOKill();
            text.DOFade(0, fadeOutDuration);
        }
        
    }

    public override void FadeInUI()
    {
      
        foreach (Image image in images)
        {
            image.DOKill();
            image.DOFade(1, fadeInDuration).OnComplete(() => playerHpSliderView.ResetColor());
           
        }

        foreach (TMP_Text text in texts)
        {
            text.DOKill();
            text.DOFade(1, fadeInDuration);
        }
    }
}

