using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PickupCarry_Camera))]

public class CustomPickupCarryEditor : Editor
{
    private bool showInfo;

    public override void OnInspectorGUI()
    {
        PickupCarry_Camera ps = target as PickupCarry_Camera;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("OBJECT PICKUP & CARRY SYSTEM", EditorStyles.toolbarButton);
        EditorGUILayout.Space();
        if(!showInfo)
            if (GUILayout.Button("[Show Info]", EditorStyles.centeredGreyMiniLabel))
                showInfo = true;
        if(showInfo)
            if (GUILayout.Button("[Hide Info]", EditorStyles.centeredGreyMiniLabel))
                showInfo = false;
        EditorGUILayout.Space();


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("References", EditorStyles.boldLabel);
        ps.target = EditorGUILayout.ObjectField("Target", ps.target, typeof(GameObject), true) as GameObject;
        if (showInfo)
            EditorGUILayout.LabelField("    Target which the carried object will follow.", EditorStyles.miniLabel);
        if (ps.target == null)
        {
            EditorGUILayout.HelpBox("No target assigned!", MessageType.Error);
        }
        EditorGUILayout.Space();


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

        ps.carryMode = (PickupCarry_Camera.CarryModes)EditorGUILayout.EnumPopup("Pickup Mode", ps.carryMode);
        EditorGUILayout.Space();

        ps.allowRotation = EditorGUILayout.Toggle("Allow Rotation", ps.allowRotation);
        if(showInfo)
            EditorGUILayout.LabelField("    Allows the player to rotate the carried object.", EditorStyles.miniLabel);
        if (ps.allowRotation)
        {
            EditorGUILayout.Space();
            ps.rotateSpeed = EditorGUILayout.FloatField("    Rotation Speed", ps.rotateSpeed);
            if (showInfo)
                EditorGUILayout.LabelField("        Speed with which the carried object is being rotated.", EditorStyles.miniLabel);
        }
        EditorGUILayout.Space();

        ps.setObjectRotation = EditorGUILayout.Toggle("Set Object Rotation", ps.setObjectRotation);
        if (showInfo)
        {
            EditorGUILayout.LabelField("    Rotates carried object to (depending on which is closest) 0,", EditorStyles.miniLabel);
            EditorGUILayout.LabelField("    90, 180 or 270 degrees on pickup.", EditorStyles.miniLabel);
        }
        EditorGUILayout.Space();

        ps.followSpeed = EditorGUILayout.FloatField("Object Follow Speed", ps.followSpeed);
        if (showInfo)
            EditorGUILayout.LabelField("    Speed with which the carried object follows the target.", EditorStyles.miniLabel);
        EditorGUILayout.Space();

        ps.distance = EditorGUILayout.FloatField("Max Distance", ps.distance);
        if (showInfo)
            EditorGUILayout.LabelField("    Max distance to object while being able to pick it up.", EditorStyles.miniLabel);
        EditorGUILayout.Space();

        ps.distanceToDrop = EditorGUILayout.FloatField("Distance To Drop", ps.distanceToDrop);
        if (showInfo)
            EditorGUILayout.LabelField("    Carried object drops if distance to target is greater than the given vaule above.", EditorStyles.miniLabel);
        EditorGUILayout.Space();


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Keybinds", EditorStyles.boldLabel);

        ps.pickupButton = (KeyCode)EditorGUILayout.EnumPopup("Pickup Key", ps.pickupButton);
        EditorGUILayout.Space();

        if (ps.allowRotation)
        {
            ps.rotateL = (KeyCode)EditorGUILayout.EnumPopup("Rotate L Key", ps.rotateL);
            ps.rotateR = (KeyCode)EditorGUILayout.EnumPopup("Rotate R Key", ps.rotateR);
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Set Standard", EditorStyles.toolbarButton))
        {
            ps.allowRotation = true;
            ps.setObjectRotation = true;
            ps.carryMode = PickupCarry_Camera.CarryModes.toggle;
            ps.pickupButton = KeyCode.E;
            ps.rotateL = KeyCode.Mouse0;
            ps.rotateR = KeyCode.Mouse1;
            ps.followSpeed = 900;
            ps.rotateSpeed = 150;
            ps.distanceToDrop = 0.75f;
            ps.distance = 2.5f;
        }

        if(GUILayout.Button("Clear All", EditorStyles.toolbarButton))
        {
            ps.allowRotation = false;
            ps.setObjectRotation = false;
            ps.carryMode = PickupCarry_Camera.CarryModes.toggle;
            ps.pickupButton = KeyCode.None;
            ps.rotateL = KeyCode.None;
            ps.rotateR = KeyCode.None;
            ps.followSpeed = 0;
            ps.rotateSpeed = 0;
            ps.distanceToDrop = 0;
            ps.distance = 0;
            ps.target = null;
        }
        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(ps);
    }
}
