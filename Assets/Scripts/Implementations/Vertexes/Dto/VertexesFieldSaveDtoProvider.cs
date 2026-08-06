using EasyField.Links;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem;
using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Mappers;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesFieldSaveDtoProvider : FieldSaveDtoProvider<VertexesFieldSaveDto, VertexData, VertexDataDto, LinkData<int>, LinkDataDto<int>, int>
    {
        public VertexesFieldSaveDtoProvider(
            IObjectsStorage<VertexData, int> nodeDatas,
            IObjectsStorage<LinkData<int>, LinkKey<int>> linkDatas, 
            INodeDataMapper<VertexData, VertexDataDto, int> nodesMapper,
            ILinkDataMapper<LinkData<int>, LinkDataDto<int>, int> linksMapper)
            : base(nodeDatas, linkDatas, nodesMapper, linksMapper)
        {
        }
    }
}