using ThisProject.Nodes;
using ThisProject.SaveSystem.Dto;

namespace ThisProject.SaveSystem.Mappers
{
    //todo: common easier converter?
    public interface IMapper<TNodeData, TNodeDataDto, TId>
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
    {
        public TNodeDataDto ToDto(TNodeData nodeData);
    }
}