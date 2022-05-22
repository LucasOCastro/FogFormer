
using FogFormer.AI;
using FogFormer.AI.Nodes;
using UnityEditor;
using UnityEditor.UIElements;
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
        private ObjectField _treeSelector;
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

            _treeSelector = root.Q<ObjectField>();
            _treeSelector.objectType = typeof(BehaviorTree);
            _treeSelector.RegisterValueChangedCallback(e => SelectTree(e.newValue as BehaviorTree));

            _treeView.OnNodeSelected += OnNodeSelectionChange;
        }

        private void SelectTree(BehaviorTree tree)
        {
            if (_treeSelector.value != tree)
            {
                _treeSelector.SetValueWithoutNotify(tree);
            }
            _treeView.PopulateView(tree);
        }
        
        
        [UnityEditor.Callbacks.OnOpenAsset]
        private static bool OpenEditorWindow(int instanceID, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not BehaviorTree tree)
            {
                return false;
            }
            var window = OpenWindow();
            window.SelectTree(tree);
            return true;
        }

        private void OnSelectionChange()
        {
            if (Selection.activeObject is BehaviorTree tree)
            {
                SelectTree(tree);
            }
        }

        private void OnNodeSelectionChange(Node node)
        {
            _inspectorView.UpdateSelection(node);
        }
    }    
}
