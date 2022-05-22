using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace FogFormer.Editor
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits>{}
        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditorWindow.uss");
            styleSheets.Add(styleSheet);
        }
    }
}