using UnityEngine;
[CreateAssetMenu(fileName = "New Crystal Object", menuName = "Inventory System/Items/Crystal")]

public class CrystalObject : ItemObject
{
    [SerializeField] int maxUses;
    [SerializeField] CrystalTypes crystalType;

    private void OnEnable() => ItemType = ItemType.Crystal;

    override public void Use(Item item)
    {
        InventoryEvents.CrystalEquipped(item);
        Debug.Log($"Equiping {crystalType.name} with {CalculateRemainingUses(item)} left");
    }
        
    override public void UseEquipped(Item item) {
        if (CalculateRemainingUses(item) > 0)
        {
            Debug.Log("Firing crystal!!!");
            item.itemUses++;
            Debug.Log($"You have {CalculateRemainingUses(item)} charge(s) remaining");
        }
        else
        {
            Debug.Log("Crystal is empty");
        }
    }

    public int CalculateRemainingUses(Item item)
    {
        return maxUses + 1 - item.itemUses;
    }

}
