namespace ThisProject.Fields.FieldBuilders
{
    public interface IFieldBuilder
    {
        public void CreateFromDto();
        public void CreateEmpty();
        public void ClearAll();
        //todo
        public void CreateFieldItem();
        public void DeleteFieldItem();
    }
}
