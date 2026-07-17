using ThisProject.SaveSystem.Dto;

namespace ThisProject.SaveSystem
{
    public interface IFieldSaveDtoProvider<TFieldSaveDto, TNodeDataDto, TLinkDataDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new()
    {
        public TFieldSaveDto GetDto();
    }
}