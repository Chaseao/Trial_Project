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

        InventorySystem inventorySystem = (InventorySystem)target;
        if(GUILayout.Button("Print Current Items"))
        {
            inventorySystem.PrintItems();
        }
    }
}
