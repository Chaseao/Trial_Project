using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(ItemTester))]
public class ItemPickupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemTester itemPickup = (ItemTester)target;

        if (GUILayout.Button("Set Item"))
        {
            itemPickup.SetItem();
        }

        if (GUILayout.Button("Add Current Item"))
        {
            itemPickup.PickUpItem();
        }

        if(GUILayout.Button("Use Item"))
        {
            itemPickup.UseItem();
        }
    }
}
