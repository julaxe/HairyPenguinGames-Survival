using UnityEngine;

namespace ExternalPackages.InventoryPackage._Scripts_.Inventory
{
    interface IEffect 
    {
        void Apply();
    }

    public class ItemEffect : ScriptableObject, IEffect
    {
        public virtual void Apply()
        {
            
        }
    }
}