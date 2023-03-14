using Links;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public interface INode
    {
        List<ILink> Links { get; }
        bool IsObstacle { get; }

        Vector3 GetCenter();
        void DrawPath();
        void ResetState();
    }
}
