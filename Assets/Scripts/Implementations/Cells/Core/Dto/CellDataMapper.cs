using EasyField.SaveSystem.Dto.Mappers;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellDataMapper : INodeDataMapper<CellData, CellDataDto, Vector2Int>
    {
        public CellDataDto ToDto(CellData nodeData)
        {
            return new CellDataDto(nodeData.Id, nodeData.NodePosition, nodeData.CellType.Id);
        }
    }
}