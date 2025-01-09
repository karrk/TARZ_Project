using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] UpgradeLayout upgradeLayout;
    [SerializeField] Image sprite;

    [SerializeField] Image slotImage;
    [SerializeField] Color[] rarityColor;
    public void SetSlot(NewEquipment newEquipment)
    {
      
        upgradeLayout.SetLayout(newEquipment.upgradeLevel);
        sprite.sprite = newEquipment.illust;

        slotImage.color = rarityColor[(int)newEquipment.rarityTier];
    }

    public void ChangeSlot(NewEquipment newEquipment)
    {
        upgradeLayout.RemoveFill();
        upgradeLayout.SetLayout(newEquipment.upgradeLevel);
        sprite.sprite = newEquipment.illust;

        slotImage.color = rarityColor[(int)newEquipment.rarityTier];
    }

    public void LevelUpSlot(int level)
    {
       upgradeLayout.ChangeFill(level);
        
    }
}
