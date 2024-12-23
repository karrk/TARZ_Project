using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISlot : MonoBehaviour
{

    ISlotPanel slotPanel;

    [SerializeField] GameObject uiVFX;

    [SerializeField] private Image uiImage;
    [SerializeField] private ButtonSelectCallback selectCallback;
    [SerializeField] public Button slotButton;
    [SerializeField] private int slotNumber;

    Color defaultColor;
    private void Awake()
    {

        selectCallback = GetComponentInChildren<ButtonSelectCallback>();
        slotButton = GetComponentInChildren<Button>();
        slotPanel = GetComponentInParent<ISlotPanel>();
        defaultColor = slotButton.image.color;
    }

    public int SlotNumber { get { return slotNumber; } set { slotNumber = value; } } 


    

    public void SetSlotImage(Sprite sprite)
    {
        uiImage.enabled = true;
        uiImage.sprite = sprite;
    }

    public void SetDefaultColor()
    {
        slotButton.image.color = defaultColor;
    }

    public void RemoveSlotImage()
    {
        uiImage.enabled = false;
        uiImage.sprite = null;
    }


    public Sprite GetSprite()
    {
        return uiImage.sprite;
    }

    public Transform GetImageTransform()
    {
        return uiImage.transform;
    }

    public void ChangeItemInformation()
    {
        slotPanel.SlotSelectCallback(SlotNumber);
    }


    public void OnVFX()
    {
        uiVFX.SetActive(false);
        uiVFX.SetActive(true);
    }

    private void OnEnable()
    {
        selectCallback.selectEvent += ChangeItemInformation;
    }


    private void OnDisable()
    {
        if (uiVFX)
        {
            uiVFX.SetActive(false);
        }
        selectCallback.selectEvent -= ChangeItemInformation;
    }
}
