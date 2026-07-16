using System;
using ThisProject.SaveSystem.Dto;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    [Serializable]
    public class CellDataDto : NodeDataDto<Vector2Int>
    {
        private readonly Vector2Int _id;
        private readonly Vector2Dto _nodePosition;

        public CellDataDto(CellData nodeData)
        {
            _id = nodeData.Id;
            _nodePosition = nodeData.NodePosition;
        }

        public Vector2Int Id => _id;

        public Vector2Dto NodePosition => _nodePosition;
    }
}