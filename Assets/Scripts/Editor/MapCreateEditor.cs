using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractMap), true)]
public class MapCreateEditor : Editor
{
    AbstractMap generator;

    private void Awake()
    {
        generator = (AbstractMap)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Map"))
        {
            generator.GenerateMap();
        }
    }
}
