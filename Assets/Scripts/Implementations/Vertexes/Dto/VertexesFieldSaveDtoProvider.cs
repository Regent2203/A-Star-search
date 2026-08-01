using ThisProject.Links;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.Mappers;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesFieldSaveDtoProvider : FieldSaveDtoProvider<VertexesFieldSaveDto, VertexData, VertexDataDto, LinkData<int>, LinkDataDto<int>, int>
    {
        public VertexesFieldSaveDtoProvider(IObjectsStorage<VertexData, int> nodes, IObjectsStorage<ILinkData<int>, LinkKey<int>> links, 
            IMapper<VertexData, VertexDataDto, int> mapper)
            : base(nodes, links, mapper)
        {
        }
    }
}