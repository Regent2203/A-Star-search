using EasyField.SaveSystem;
using EasyField.SceneControllers;

namespace EasyField.Implementations.Vertexes.Core.Dto
{
    public class VertexesSaveLoadManager : SaveLoadManager<VertexesFieldSaveDto>
    {
        public VertexesSaveLoadManager(ISaver saver, ILoader loader, VertexesFieldSaveDtoProvider dtoProvider) : base(saver, loader, dtoProvider)
        {
        }
    }
}