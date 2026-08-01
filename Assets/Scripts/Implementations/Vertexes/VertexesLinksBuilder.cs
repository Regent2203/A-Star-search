using ThisProject.Links;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.ObjectsStorages;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesLinksBuilder : LinksBuilder<VertexData, VertexView, LinkData<int>, int>
    {
        public VertexesLinksBuilder(LinksFactory<VertexData, int> linksFactory, StoredLinksProvider<VertexData, LinkData<int>, int> linksProvider, 
            DictTypeStorage<LinkData<int>, LinkKey<int>> linkDatas, DictTypeStorage<LinkView<int>, LinkKey<int>> linkViews,
            LinkDataPool<int> linkDatasPool, LinkViewPool<int> linkViewsPool,
            IObjectsStorage<VertexView, int> nodeViews)
            : base(linksFactory, linksProvider, linkDatas, linkViews, linkDatasPool, linkViewsPool, nodeViews)
        {
        }
    }
}