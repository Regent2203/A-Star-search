using Core.Links;
using Core.Weightables;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Nodes
{
    public interface INode : IWeightable
    {
        public Vector2 Position { get; }
        public IEnumerable<ILink> GetLinks();
        public bool IsBlocked { get; }
    }
}