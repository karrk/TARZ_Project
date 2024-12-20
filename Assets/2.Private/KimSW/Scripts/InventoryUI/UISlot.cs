using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISlot : MonoBehaviour
{

    ISlotPanel slotPanel;

    [SerializeField] private Image uiImage;
    [SerializeField] private ButtonSelectCallback selectCallback;
    [SerializeField] public Button slotButton;
    [SerializeField] private int slotNumber;

    private void Awake()
    {
        selectCallback = GetComponentInChildren<ButtonSelectCallback>();
        slotButton = GetComponentInChildren<Button>();
        slotPanel = GetComponentInParent<ISlotPanel>();
    }

    public int SlotNumber { get { return slotNumber; } set { slotNumber = value; } } 


  

    public void SetSlotImage(Sprite sprite)
    {
        uiImage.enabled = true;
        uiImage.sprite = sprite;
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

    private void OnEnable()
    {
        selectCallback.selectEvent += ChangeItemInformation;
    }


    private void OnDisable()
    {
        selectCallback.selectEvent -= ChangeItemInformation;
    }
}
