using EasyField.SaveSystem.Mappers;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellDataMapper : INodeMapper<CellData, CellDataDto, Vector2Int>
    {
        public CellDataDto ToDto(CellData nodeData)
        {
            return new CellDataDto(nodeData.Id, nodeData.NodePosition, nodeData.CellType);
        }
    }
}