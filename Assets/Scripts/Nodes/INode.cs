using System;
using UnityEngine;

namespace ThisProject.Nodes
{
    public interface INode
    {
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }
    }

    public interface INode<TId> : INode
    {
        TId Id { get; }
    }
}