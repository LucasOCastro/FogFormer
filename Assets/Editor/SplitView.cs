using UnityEngine.UIElements;

namespace FogFormer.Editor
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, UxmlTraits>{}
    }
}