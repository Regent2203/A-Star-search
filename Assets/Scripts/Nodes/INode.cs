using Links;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public interface INode
    {
        public List<ILink> Links { get; }
        public bool IsObstacle { get; }

        public Vector3 GetCenterCoords(); //todo
        public void DrawPath(bool draw);
    }
}
