using ThisProject.Links;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.ObjectsStorages;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesLinksBuilder : LinksBuilder<VertexData, VertexView, int>
    {
        public VertexesLinksBuilder(LinksFactory<VertexData, int> linksFactory, StoredLinksProvider<VertexData, int> linksProvider, 
            DictTypeStorage<ILinkData<int>, LinkKey<int>> links, DictTypeStorage<LinkView<int>, LinkKey<int>> views,
            LinkViewPool<int> viewsPool, IObjectsStorage<VertexView, int> nodeViews)
            : base(linksFactory, linksProvider, links, views, viewsPool, nodeViews)
        {
        }
    }
}