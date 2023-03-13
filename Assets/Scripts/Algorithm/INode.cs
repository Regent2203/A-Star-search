using Links;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Algorithm
{
    public interface INode
    {
        List<ILink> Links { get; }

        Vector3 GetCenter();
    }
}
