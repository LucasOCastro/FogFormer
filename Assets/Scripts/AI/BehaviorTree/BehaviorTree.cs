using System;
using System.Collections.Generic;
using UnityEngine;
using FogFormer.AI.Nodes;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FogFormer.AI
{
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        
        [HideInInspector]
        [SerializeField] private RootNode rootNode;
        [SerializeField] private List<Node> allNodes = new();
        public IEnumerable<Node> AllNodes => allNodes;
        public void Tick(BehaviorRunData data)
        {
            rootNode.Tick(data);
        }

        public BehaviorTree Clone()
        {
            var tree = Instantiate(this);
            tree.rootNode = (RootNode)rootNode.Clone();
            return tree;
        }

#if UNITY_EDITOR
        public Node CreateNode(System.Type type)
        {
            Node node = (Node)CreateInstance(type);
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            allNodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            allNodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}