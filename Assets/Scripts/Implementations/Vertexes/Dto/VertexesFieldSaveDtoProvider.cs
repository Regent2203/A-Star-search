using ThisProject.Links;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.Mappers;

namespace ThisProject.Implementations.Vertexes
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