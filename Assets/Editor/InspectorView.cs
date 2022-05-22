using FogFormer.AI.Nodes;
using UnityEngine.UIElements;

namespace FogFormer.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>{}

        private UnityEditor.Editor _editor;
        public void UpdateSelection(Node node)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_editor);            
            _editor = UnityEditor.Editor.CreateEditor(node);
            IMGUIContainer container = new IMGUIContainer(() => _editor.OnInspectorGUI());
            Add(container);
        }
    }
}