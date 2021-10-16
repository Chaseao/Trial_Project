using UnityEngine;
[CreateAssetMenu(fileName = "New Crystal Object", menuName = "Inventory System/Items/Crystal")]

public class CrystalObject : ItemObject
{
    //You will need to fill out this class here. YOu will need to derive from ItemObject.
    //A crystal object should be set to a type (water, fire, or nature) and have a limit 
    //to how many uses you get from the crystal.
    [SerializeField] int maxUses;
    [SerializeField] CrystalTypes crystalType;

    private void OnEnable()
    {
        ItemType = ItemType.Crystal;
    }

    override public void Use(Item item)
    {
        InventoryEvents.EquipCrystal(item);
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
