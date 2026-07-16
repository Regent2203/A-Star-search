using System.Linq;
using ThisProject.Links;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.Mappers;

namespace ThisProject.SaveSystem
{
    public interface IFieldSaveDtoProvider<TNodeDto, TLinkDto>
    {
        IFieldSaveDto<TNodeDto, TLinkDto> GetDto();
    }

    public class FieldSaveDtoProvider<TNodeData, TNodeDataDto, TLinkDataDto, TId> : IFieldSaveDtoProvider<TNodeDataDto, TLinkDataDto>
        where TNodeData : INodeData<TId>
        where TNodeDataDto : INodeDataDto<TId>
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

        public IFieldSaveDto<TNodeDataDto, TLinkDataDto> GetDto()
        {
            var fieldSaveDto = new FieldSaveDto<TNodeDataDto, TLinkDataDto>
            {
                Nodes = _nodes.AllItems.Select(node => _mapper.ToDto(node)).ToList(),
                //Links = null, //todo
            };

            return fieldSaveDto;
        }
    }
}