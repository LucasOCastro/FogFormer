
using FogFormer.AI;
using FogFormer.AI.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FogFormer.Editor
{
    public class BehaviorTreeEditorWindow : EditorWindow
    {
        [MenuItem("Window/Behavior Trees")]
        public static BehaviorTreeEditorWindow OpenWindow()
        {
            BehaviorTreeEditorWindow wnd = GetWindow<BehaviorTreeEditorWindow>();
            wnd.titleContent = new GUIContent("Behavior Tree Editor");
            return wnd;
        }
    
        private BehaviorTreeView _treeView;
        private InspectorView _inspectorView;
        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;


            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviorTreeEditorWindow.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditorWindow.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<BehaviorTreeView>();
            _inspectorView = root.Q<InspectorView>();

            _treeView.OnNodeSelected += OnNodeSelectionChange;

            if (_treeView.tree == null)
            {
                OnSelectionChange();    
            }
        }
        
        
        [UnityEditor.Callbacks.OnOpenAsset]
        private static bool OpenEditorWindow(int instanceID, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not BehaviorTree tree)
            {
                return false;
            }
            var window = OpenWindow();
            window._treeView.PopulateView(tree);
            return true;
        }

        private void OnSelectionChange()
        {
            if (Selection.activeObject is BehaviorTree tree)
            {
                _treeView.PopulateView(tree);
            }
        }

        private void OnNodeSelectionChange(Node node)
        {
            _inspectorView.UpdateSelection(node);
        }
    }    
}
