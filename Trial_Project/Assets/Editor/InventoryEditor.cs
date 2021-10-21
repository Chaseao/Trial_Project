using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventorySystem))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        InventorySystem inventorySystem = (InventorySystem)target;
        CreatePrintButton(inventorySystem);

        GUILayout.Space(5);
        CreateActivateButton(inventorySystem);
        CreateDeactivateButton(inventorySystem);
    }

    private static void CreatePrintButton(InventorySystem inventorySystem)
    {
        if (GUILayout.Button("Print Current Items"))
        {
            inventorySystem.PrintItems();
        }
    }
    private static void CreateActivateButton(InventorySystem inventorySystem)
    {
        if (!inventorySystem.Active)
        {
            if (GUILayout.Button("Activate"))
            {
                inventorySystem.Activate();
            }
        }
    }

    private static void CreateDeactivateButton(InventorySystem inventorySystem)
    {
        if (inventorySystem.Active)
        {
            if (GUILayout.Button("Deactivate"))
            {
                inventorySystem.Deactivate();
            }
        }
    }
}
