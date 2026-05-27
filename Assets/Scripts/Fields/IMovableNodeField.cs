using System;
using ThisProject.Fields.NodeMovers;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields
{
    public interface IMovableNodeField
    {
        public abstract INodeMover NodeMover { get; }

        public event Action<INode, Vector2> NodeMoved;
    }
}