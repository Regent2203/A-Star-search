using EasyField.Nodes;
using EasyField.SaveSystem.Dto;

namespace EasyField.SaveSystem.Mappers
{
    public interface INodeMapper<TNodeData, TNodeDataDto, TId>
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
    {
        public TNodeDataDto ToDto(TNodeData nodeData);
    }
}