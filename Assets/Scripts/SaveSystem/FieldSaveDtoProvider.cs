using System.Linq;
using ThisProject.Links;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.Mappers;

namespace ThisProject.SaveSystem
{
    public interface IFieldSaveDtoProvider<TNodeDataDto, TLinkDataDto>
    {
        public T GetDto<T>()
            where T : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new();
    }

    public class FieldSaveDtoProvider<TNodeData, TNodeDataDto, TLinkDataDto, TId> : IFieldSaveDtoProvider<TNodeDataDto, TLinkDataDto>
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
    {
        private readonly IObjectsStorage<TNodeData, TId> _nodes;
        private readonly IMapper<TNodeData, TNodeDataDto, TId> _mapper;


        public FieldSaveDtoProvider(
            IObjectsStorage<TNodeData, TId> nodes,
            IMapper<TNodeData, TNodeDataDto, TId> mapper)
        {
            _nodes = nodes;
            _mapper = mapper;
        }

        public T GetDto<T>()
            where T : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new()
        {
            var fieldSaveDto = new T
            {
                Nodes = _nodes.AllItems.Select(node => _mapper.ToDto(node)).ToList(),
                //Links = null, //todo
            };

            return fieldSaveDto;
        }
    }
}