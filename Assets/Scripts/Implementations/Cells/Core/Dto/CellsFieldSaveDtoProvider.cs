using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto.FieldSaveDtoProviders;
using EasyField.SaveSystem.Dto.Mappers;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellsFieldSaveDtoProvider : FieldSaveDtoProvider<CellsFieldSaveDto, CellData, CellDataDto, Vector2Int>
    {
        public CellsFieldSaveDtoProvider(
            IObjectsStorage<CellData, Vector2Int> nodeDatas,
            INodeDataMapper<CellData, CellDataDto, Vector2Int> nodesMapper)
            : base(nodeDatas, nodesMapper)
        { }
    }
}