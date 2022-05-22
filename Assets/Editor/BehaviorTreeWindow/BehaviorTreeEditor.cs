using System;
using FogFormer.AI;
using FogFormer.AI.Nodes;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEditor.PackageManager.UI;
using UnityEngine;


namespace FogFormer.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        [MenuItem("Window/BehaviorTrees")]
        private static void OpenNewWindow()
        {
            var window = GetWindow<BehaviorTreeEditor>();
            window.maximized = true;
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("TEST");
        }
    }
}