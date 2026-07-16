using ThisProject.Nodes;
using ThisProject.SaveSystem.Dto;

namespace ThisProject.SaveSystem.Mappers
{    
    public interface IMapper<T, TNodeDataDto, TId>
        where T : INodeData<TId>
        where TNodeDataDto : INodeDataDto<TId>
    {
        public TNodeDataDto ToDto(T nodeData);
    }
}