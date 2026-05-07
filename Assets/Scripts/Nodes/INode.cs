using Core.Links;
using Core.Weightables;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Nodes
{
    public interface INode<T> : IWeightable where T : INode<T>
    {
        public Vector2 Position { get; }
        public IEnumerable<ILink<T>> GetLinks();
        public bool IsBlocked { get; }
    }
}