using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    Button button;
    [SerializeField] Image itemPicture;
    [SerializeField] TextMeshProUGUI textBox;
    protected Slot slot = null;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.targetGraphic = itemPicture;

        ClearSlot();
    }

    public void UpdateSlot(Slot newSlot)
    {
        slot = newSlot;

        itemPicture.enabled = true;
        itemPicture.sprite = slot.item.ui_icon;

        if (slot.item.isStackable)
        {
            textBox.text = slot.amount.ToString();
        }
        else
        {
            textBox.text = "";
        }
    }

    public void ClearSlot()
    {
        itemPicture.enabled = false;
        textBox.text = "";
        slot = null;
    }

    public void UseItem()
    {
        if (slot != null)
        {
            InventoryEvents.SetItemToBeUsed(slot.item);
        }
    }
}

public class Slot
{
    public Item item;
    public int amount;

    public Slot(Item incomingItem, int amountOfItem)
    {
        item = incomingItem;
        amount = amountOfItem;
    }
}
