using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] UpgradeLayout upgradeLayout;
    [SerializeField] Image sprite;


    public void SetSlot(NewEquipment newEquipment)
    {
      
        upgradeLayout.SetLayout(newEquipment.upgradeLevel);
        sprite.sprite = newEquipment.illust;

    }

    public void ChangeSlot(NewEquipment newEquipment)
    {
        upgradeLayout.RemoveFill();
        upgradeLayout.SetLayout(newEquipment.upgradeLevel);
        sprite.sprite = newEquipment.illust;
    }

    public void LevelUpSlot(int level)
    {
       upgradeLayout.ChangeFill(level);
        
    }
}
