using EasyField.Links;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem;
using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Mappers;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesFieldSaveDtoProvider : FieldSaveDtoProvider<VertexesFieldSaveDto, VertexData, VertexDataDto, LinkData<int>, LinkDataDto<int>, int>
    {
        public VertexesFieldSaveDtoProvider(IObjectsStorage<VertexData, int> nodes, IObjectsStorage<LinkData<int>, LinkKey<int>> links, 
            INodeMapper<VertexData, VertexDataDto, int> nodesMapper, ILinkMapper<LinkData<int>, LinkDataDto<int>, int> linksMapper)
            : base(nodes, links, nodesMapper, linksMapper)
        {
        }
    }
}