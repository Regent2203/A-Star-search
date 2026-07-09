using System;
using ThisProject.SaveSystem.Dto;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    [Serializable]
    public class CellDataDto : NodeDataDto<Vector2Int>
    {
        public CellDataDto(CellData nodeData)
        {
            Id = nodeData.Id;
            NodePosition = nodeData.NodePosition;
        }
    }
}