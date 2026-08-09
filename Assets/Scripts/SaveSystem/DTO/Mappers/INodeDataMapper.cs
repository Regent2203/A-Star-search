using EasyField.Nodes;

namespace EasyField.SaveSystem.Dto.Mappers
{
    public interface INodeDataMapper<TNodeData, TNodeDataDto, TId>
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
    {
        public TNodeDataDto ToDto(TNodeData nodeData);
    }
}