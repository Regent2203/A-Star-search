using EasyField.Nodes;
using EasyField.SaveSystem.Dto;

namespace EasyField.SaveSystem.Mappers
{
    public interface INodeDataMapper<TNodeData, TNodeDataDto, TId>
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
    {
        public TNodeDataDto ToDto(TNodeData nodeData);
    }
}