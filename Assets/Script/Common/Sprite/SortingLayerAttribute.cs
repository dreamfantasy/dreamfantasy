using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SortingLayerAttribute : PropertyAttribute{}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SortingLayerAttribute))]
public class SortingLayerDrawerAttribute : PropertyDrawer
{
    private static SerializedProperty sortinglayer = null;
    public static SerializedProperty SortingLayer
    {
        get
        {
            if (sortinglayer == null)
            {
                var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                sortinglayer = tagManager.FindProperty("m_SortingLayers");
            }
            return sortinglayer;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var list = AllSortingLayer;
        var selectedIndex = list.FindIndex(item => item.Equals(property.stringValue));
        if (selectedIndex == -1)
            selectedIndex = list.FindIndex(item => item.Equals("Default"));

        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, list.ToArray());

        property.stringValue = list[selectedIndex];
    }

    private List<string> AllSortingLayer
    {
        get
        {
            var layerNameList = new List<string>();
            for (int i = 0; i < SortingLayer.arraySize; i++)
            {
                var tag = SortingLayer.GetArrayElementAtIndex(i);
                layerNameList.Add(tag.displayName);
            }
            return layerNameList;
        }
    }
}
#endif