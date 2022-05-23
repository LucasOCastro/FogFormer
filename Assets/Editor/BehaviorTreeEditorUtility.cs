using System.Collections.Generic;
using System.Reflection;
using FogFormer.AI;
using FogFormer.AI.Nodes;
using UnityEditor;
using UnityEngine;

namespace FogFormer.Editor
{
    public static class BehaviorTreeEditorUtility
    {
        private static FieldInfo CompositeChildrenField => typeof(CompositeNode).GetField("children", BindingFlags.Instance | BindingFlags.NonPublic);
        private static FieldInfo DecoratorChildField => typeof(DecoratorNode).GetField("child", BindingFlags.Instance | BindingFlags.NonPublic);
        private static FieldInfo RootChildField => typeof(RootNode).GetField("child", BindingFlags.Instance | BindingFlags.NonPublic);
        private static FieldInfo TreeRootNodeField => typeof(BehaviorTree).GetField("rootNode", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void AddChild(Node parent, Node child)
        {
            switch (parent)
            {
                case CompositeNode composite:
                {
                    var value = CompositeChildrenField.GetValue(composite);
                    if (value is List<Node> childrenList)
                    {
                        childrenList.Add(child);
                    }
                    break;
                }
                case DecoratorNode decorator:
                {
                    DecoratorChildField.SetValue(decorator, child);
                    break;
                }
                case RootNode root:
                {
                    RootChildField.SetValue(root, child);
                    break;
                }
            }
            AssetDatabase.SaveAssets();
        }

        public static void RemoveChild(Node parent, Node child)
        {
            switch (parent)
            {
                case CompositeNode composite:
                {
                    var childrenList = CompositeChildrenField.GetValue(composite) as List<Node>;
                    childrenList?.Remove(child);
                    break;
                }
                case DecoratorNode decorator:
                {
                    DecoratorChildField.SetValue(decorator, null);
                    break;
                }
                case RootNode root:
                {
                    RootChildField.SetValue(root, null);
                    break;
                }
            }
            AssetDatabase.SaveAssets();
        }

        public static IEnumerable<Node> GetChildren(Node node)
        {
            switch (node)
            {
                case CompositeNode composite:
                {
                    if (CompositeChildrenField.GetValue(composite) is List<Node> childrenList)
                    {
                        foreach (var compChild in childrenList)
                            yield return compChild;
                    }
                    break;
                }
                case DecoratorNode decorator:
                {
                    if (DecoratorChildField.GetValue(decorator) is Node child)
                    {
                        yield return child;
                    }
                    break;
                }
                case RootNode root:
                {
                    if (RootChildField.GetValue(root) is Node child)
                    {
                        yield return child;
                    }
                    break;
                }
            }
        }

        public static void EnsureTreeHasRoot(BehaviorTree tree)
        {
            var value = TreeRootNodeField.GetValue(tree);
            if (value is null)
            {
                RootNode node = (RootNode)tree.CreateNode(typeof(RootNode));
                TreeRootNodeField.SetValue(tree, node);
                AssetDatabase.SaveAssets();
            }
            
        }
    }
}