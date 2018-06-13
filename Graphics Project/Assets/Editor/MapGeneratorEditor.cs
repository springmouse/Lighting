using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (ProceduralMap))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        ProceduralMap mapGen = (ProceduralMap)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
