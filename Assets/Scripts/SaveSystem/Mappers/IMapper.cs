using ThisProject.Nodes;
using ThisProject.SaveSystem.Dto;

namespace ThisProject.SaveSystem.Mappers
{
    public interface IMapper<T, Dto, TId>
        where T : INodeData<TId>
        where Dto : NodeDataDto<TId>
    {
        public Dto ToDto(T nodeData);
    }
}