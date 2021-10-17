using UnityEngine;
[CreateAssetMenu(fileName = "New Heal Object", menuName = "Inventory System/Items/Heal")]

public class HealObject : ItemObject
{
    [SerializeField] int healAmount;

    private void OnEnable() => ItemType = ItemType.Heal;

    public override void Use(Item item)
    {
        Debug.Log($"Healing {healAmount} hp");
        InventoryEvents.LoseItem(item);
    }

    public override void UseEquipped(Item item) { }
}
