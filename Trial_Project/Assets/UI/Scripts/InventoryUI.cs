using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DisplayUI))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventorySystem connectedInventory;
    [SerializeField] DisplayUI displayUI;
    int totalSlotsInSet;
    int totalSetsOfSlots;

    Slot[][] slots;
    int currentSlotSet = 0;

    List<GameObject> childrenToControl = new List<GameObject>();

    private void OnEnable()
    {
        InventoryEvents.InventoryUpdated += UpdateInventoryUI;
        InventoryEvents.InventoryOpened += ShowInventory;
        InventoryEvents.InventoryClosed += HideInventory;
    }

    private void OnDisable()
    {
        InventoryEvents.InventoryUpdated -= UpdateInventoryUI;
        InventoryEvents.InventoryOpened -= ShowInventory;
        InventoryEvents.InventoryClosed -= HideInventory;
    }

    private void Start()
    {
        SetListOfChildrenToControl();
        HideInventory();
        UpdateInventoryUI(connectedInventory);
    }

    private void SetListOfChildrenToControl()
    {
        CanvasRenderer[] childrenWithCanvasRenderer = transform.GetComponentsInChildren<CanvasRenderer>();

        foreach (CanvasRenderer child in childrenWithCanvasRenderer)
        {
            bool isAInventorySlot = child.gameObject.GetComponent<InventorySlotUI>();
            if (isAInventorySlot == false)
            {
                childrenToControl.Add(child.gameObject);
            }
        }
    }

    private void CreateEmptyInventorySlotsArray()
    {
        SetArrayDimensions();

        slots = new Slot[totalSetsOfSlots][];
        for (int i = 0; i < totalSetsOfSlots; i++)
        {
            slots[i] = new Slot[totalSlotsInSet];
        }
    }

    private void SetArrayDimensions()
    {
        totalSlotsInSet = displayUI.GetAmountOfSlots();
        totalSetsOfSlots = 1 + connectedInventory.TotalAmountOfSlots / totalSlotsInSet;
    }

    private void FillArraySlotsWithItemsFromInventory(Dictionary<Item, int> inventory)
    {
        int i = 0;
        int j = 0;

        foreach (Item item in inventory.Keys)
        {
            Slot slot = new Slot(item, inventory[item]);
            slots[j][i] = slot;
            i++;

            if (i >= totalSlotsInSet)
            {
                i = 0;
                j++;
            }

            if (j >= totalSetsOfSlots)
            {
                break;
            }
        }
    }

    public void UpdateInventoryUI(InventorySystem updatedInventory)
    {
        if (updatedInventory.Equals(connectedInventory))
        {
            Dictionary<Item, int> inventory = connectedInventory.GetItemsInInventory();

            CreateEmptyInventorySlotsArray();
            FillArraySlotsWithItemsFromInventory(inventory);

            displayUI.UpdateSlots(slots[currentSlotSet]);
            UpdateEquipmentUI();
        }
    }

    public void UpdateEquipmentUI()
    {
        if (connectedInventory.CurrentlyEquippedCrystal != null)
        {
            Slot newSlot = new Slot(connectedInventory.CurrentlyEquippedCrystal, 1);
            displayUI.SetEquipmentSlot(newSlot);
        }
    }

    private void HideInventory()
    {
        foreach(GameObject child in childrenToControl)
        {
            child.SetActive(false);
        }
    }

    private void ShowInventory()
    {
        foreach (GameObject child in childrenToControl)
        {
            child.SetActive(true);
        }
    }

    public void PreviousInventorySlots()
    {
        if(currentSlotSet > 0)
        {
            currentSlotSet--;
        }

        displayUI.UpdateSlots(slots[currentSlotSet]);
    }

    public void NextInventorySlots()
    {
        if(currentSlotSet < totalSetsOfSlots - 1)
        {
            currentSlotSet++;
        }

        displayUI.UpdateSlots(slots[currentSlotSet]);
    }
}
