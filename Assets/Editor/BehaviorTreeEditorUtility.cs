using System.Collections.Generic;
using System.Reflection;
using FogFormer.AI;
using FogFormer.AI.Nodes;
using UnityEngine;

namespace FogFormer.Editor
{
    public static class BehaviorTreeEditorUtility
    {
        private static readonly FieldInfo CompositeChildrenField = typeof(CompositeNode).GetField("children", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo DecoratorChildField = typeof(DecoratorNode).GetField("child", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo RootChildField = typeof(RootNode).GetField("child", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo TreeRootNodeField = typeof(BehaviorTree).GetField("rootNode", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void AddChild(Node parent, Node child)
        {
            switch (parent)
            {
                case CompositeNode composite:
                {
                    var childrenList = (List<Node>)CompositeChildrenField.GetValue(composite);
                    childrenList.Add(child);
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
            }
        }
    }
}