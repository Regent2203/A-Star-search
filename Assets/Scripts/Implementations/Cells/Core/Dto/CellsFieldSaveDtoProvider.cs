using EasyField.Fields;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Dto.FieldSaveDtoProviders;
using EasyField.SaveSystem.Dto.Mappers;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellsFieldSaveDtoProvider : FieldSaveDtoProvider<CellsFieldSaveDto, CellData, CellDataDto, Vector2Int>
    {
        private readonly GridField _field;


        public CellsFieldSaveDtoProvider(GridField field,
            IObjectsStorage<CellData, Vector2Int> nodeDatas,
            INodeDataMapper<CellData, CellDataDto, Vector2Int> nodesMapper)
            : base(nodeDatas, nodesMapper)
        {
            _field = field;
        }

        public override CellsFieldSaveDto GetDto()
        {
            var dto = new CellsFieldSaveDto();

            PrepareFieldSize(dto);
            PrepareNodes(dto);

            return dto;
        }

        protected void PrepareFieldSize(CellsFieldSaveDto dto)
        {
            dto.FieldSize = new Vector2IntDto(_field.Size);
        }
    }
}