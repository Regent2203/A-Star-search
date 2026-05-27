using System;
using ThisProject.Fields.NodeMovers;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields
{
    public interface IMovableNodeField<T>
        where T : class, INode
    {
        public abstract INodeMover NodeMover { get; }

        public event Action<T, Vector2> NodeMoved;
    }
}