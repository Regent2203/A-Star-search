using Newtonsoft.Json;
using System;
using ThisProject.SaveSystem.Dto;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    [Serializable]
    public class VertexDataDto : INodeDataDto<int>
    {
        private readonly int _id;
        private readonly Vector2Dto _nodePosition;


        [JsonConstructor]
        public VertexDataDto(int id, Vector2Dto nodePosition)
        {
            _id = id;
            _nodePosition = nodePosition;
        }

        public VertexDataDto(VertexData nodeData)
        {
            Debug.Log(nodeData);
            _id = nodeData.Id;
            _nodePosition = nodeData.NodePosition;
        }

        public int Id => _id;

        public Vector2Dto NodePosition => _nodePosition;
    }
}