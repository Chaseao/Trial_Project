using UnityEngine;
[CreateAssetMenu(fileName = "New Crystal Object", menuName = "Inventory System/Items/Crystal")]

public class CrystalObject : ItemObject
{
    //You will need to fill out this class here. YOu will need to derive from ItemObject.
    //A crystal object should be set to a type (water, fire, or nature) and have a limit 
    //to how many uses you get from the crystal.
    [SerializeField] int maxDurability;
    [SerializeField] CrystalTypes crystalType;
    public int MaxDurability => maxDurability;

    private void OnEnable()
    {
        ItemType = ItemType.Crystal;
    }


    override public void Use(Item item)
    {
        Debug.Log($"Equiping {crystalType.name}");
    }

}
