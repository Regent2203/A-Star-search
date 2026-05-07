using Core.Links;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Nodes
{
    public interface INode
    {
        public Vector2 Position { get; }
        public List<ILink> Links { get; }
        public bool IsBlocked { get; }
    }
}