using EasyField.Fields;
using EasyField.Implementations.Links;
using EasyField.Links;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Dto.FieldSaveDtoProviders;
using EasyField.SaveSystem.Dto.Mappers;
using UnityEngine;

namespace EasyField.Implementations.Cells.DynamicCells
{
    public class DynamicCellsFieldSaveDtoProvider : FieldSaveDtoProvider<DynamicCellsFieldSaveDto, CellData, CellDataDto, 
        LinkData<Vector2Int>, LinkDataDto<Vector2Int>, Vector2Int>
    {
        private readonly GridField _field;

        public DynamicCellsFieldSaveDtoProvider(GridField field,
            IObjectsStorage<CellData, Vector2Int> nodeDatas, IObjectsStorage<LinkData<Vector2Int>, DualKey<Vector2Int>> linkDatas,
            INodeDataMapper<CellData, CellDataDto, Vector2Int> nodesMapper, ILinkDataMapper<LinkData<Vector2Int>, LinkDataDto<Vector2Int>, Vector2Int> linksMapper)
            : base(nodeDatas, linkDatas, nodesMapper, linksMapper)
        {
            _field = field;
        }

        public override DynamicCellsFieldSaveDto GetDto()
        {
            var dto = new DynamicCellsFieldSaveDto();

            PrepareFieldSize(dto);
            PrepareNodes(dto);
            PrepareLinks(dto);

            return dto;
        }

        protected void PrepareFieldSize(DynamicCellsFieldSaveDto dto)
        {
            dto.FieldSize = new Vector2IntDto(_field.Size);
        }
    }
}