using Core.Links;
using Core.Weightables;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Nodes
{
    public interface INode<T, TId> : IWeightable where T: class, INode<T, TId>
    {
        public TId Id { get; }
        public Vector2 Position { get; }
        public bool IsBlocked { get; }
        public IEnumerable<ILink<T, TId>> GetLinks();
    }
}