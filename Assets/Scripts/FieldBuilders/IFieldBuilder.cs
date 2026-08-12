using EasyField.SaveSystem.Dto;

namespace EasyField.Fields.FieldBuilders
{
    public interface IFieldBuilder<TNodeDataDto, TLinkDataDto> : IFieldBuilder
    {
        public void BuildFromDto(FieldSaveDto<TNodeDataDto, TLinkDataDto> fieldSaveDto);
    }

    public interface IFieldBuilder<TNodeDataDto> : IFieldBuilder
    {
        public void BuildFromDto(FieldSaveDto<TNodeDataDto> fieldSaveDto);
    }

    public interface IFieldBuilder
    {
        public void CreateNewField(int sizeX, int sizeY);
        public void ClearAll();
    }
}
