using ThisProject.SaveSystem.Mappers;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellDataMapper : IMapper<CellData, CellDataDto, Vector2Int>
    {
        public CellDataDto ToDto(CellData nodeData)
        {
            return new CellDataDto(nodeData);
        }
    }
}