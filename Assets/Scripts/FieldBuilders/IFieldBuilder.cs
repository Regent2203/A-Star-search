namespace EasyField.Fields.FieldBuilders
{
    public interface IFieldBuilder<TFieldSaveDto>
    {
        public void BuildFromDto(TFieldSaveDto dto);
        public void CreateNewField(int sizeX, int sizeY);
        public void ClearAll();
    }
}
