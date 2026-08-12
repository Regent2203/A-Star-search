using EasyField.Fields;
using EasyField.Implementations.Links;
using EasyField.Links;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Dto.FieldSaveDtoProviders;
using EasyField.SaveSystem.Dto.Mappers;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesFieldSaveDtoProvider : FieldSaveDtoProvider<VertexesFieldSaveDto, VertexData, VertexDataDto, LinkData<int>, LinkDataDto<int>, int>
    {
        private readonly SpatialField _field;

        public VertexesFieldSaveDtoProvider(SpatialField field,
            IObjectsStorage<VertexData, int> nodeDatas,
            IObjectsStorage<LinkData<int>, DualKey<int>> linkDatas, 
            INodeDataMapper<VertexData, VertexDataDto, int> nodesMapper,
            ILinkDataMapper<LinkData<int>, LinkDataDto<int>, int> linksMapper)
            : base(nodeDatas, linkDatas, nodesMapper, linksMapper)
        {
            _field = field;
        }

        public override VertexesFieldSaveDto GetDto()
        {
            var dto = new VertexesFieldSaveDto();
            
            PrepareFieldSize(dto);
            PrepareNodes(dto);
            PrepareLinks(dto);

            return dto;
        }

        protected void PrepareFieldSize(VertexesFieldSaveDto dto)
        {
            dto.FieldSize = new Vector2Dto(_field.Size);
        }
    }
}