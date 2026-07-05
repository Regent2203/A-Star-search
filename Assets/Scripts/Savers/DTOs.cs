using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.Savers
{
    [Serializable]
    public struct Vector2DTO
    {
        public float X;
        public float Y;

        public Vector2DTO(Vector2 vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public Vector2 ToVector2() => new Vector2(X, Y);
    }

    [Serializable]
    public struct NodeDataDTO<TId>
    {
        public TId Id;
        public Vector2DTO NodePosition;
    }

    [Serializable]
    public class NodesRootWrapper<T>
    {
        public List<NodeDataDTO<T>> Nodes { get; set; }
    }
}
