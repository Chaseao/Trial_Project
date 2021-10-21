using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseItemButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    Item currentItem;
    List<GameObject> children = new List<GameObject>();

    private void OnEnable()
    {
        InventoryEvents.ItemSetToBeUsed += SetCurrentItem;
        InventoryEvents.ItemLost += Clear;
        currentItem = null;
        GenerateChildren();
        DisableChildren();
    }

    private void OnDisable()
    {
        InventoryEvents.ItemSetToBeUsed -= SetCurrentItem;
        InventoryEvents.ItemLost -= Clear;
    }

    private void SetCurrentItem(Item item)
    {
        currentItem = item;
        textBox.text = item.itemObject.description;
        EnableChildren();
    }

    private void Clear(Item item)
    {
        if (item.Equals(currentItem))
        {
            textBox.text = "";
            currentItem = null;
            DisableChildren();
        }
    }

    public void UseCurrentItem()
    {
        if (currentItem != null)
        {
            InventoryEvents.UseItem(currentItem);
        }
    }

    private void GenerateChildren()
    {
        foreach (CanvasRenderer child in transform.GetComponentsInChildren<CanvasRenderer>())
        {
            children.Add(child.gameObject);
        }
    }

    private void EnableChildren()
    {
        foreach (GameObject child in children)
        {
            child.SetActive(true);
        }
    }

    private void DisableChildren()
    {
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
    }

}
