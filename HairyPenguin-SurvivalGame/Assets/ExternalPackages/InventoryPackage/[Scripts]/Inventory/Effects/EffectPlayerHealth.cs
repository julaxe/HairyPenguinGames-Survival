using UnityEngine;

namespace ExternalPackages.InventoryPackage._Scripts_.Inventory.Effects
{
    [CreateAssetMenu(fileName = "NewEffect", menuName = "ItemSystem/EffectPlayerHealth")]
    public class EffectPlayerHealth : ItemEffect
    {
        public int healthToApply = 0;
        public override void Apply()
        {
            var health = FindObjectOfType<PlayerHealthController>();
            health.setHealth(healthToApply);
            //Debug.Log(health.getCurrentHealth());
        }
    }
}