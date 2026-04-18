using Links;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public interface INode : IView
    {
        public List<ILink> Links { get; }
        public bool IsObstacle { get; } //todo

        public void DrawPath(bool show);
        public void ResetState();
    }
}
