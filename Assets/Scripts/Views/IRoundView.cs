namespace ThisProject.Views
{
    public interface IRoundView : IView
    {
        public float GetRadius();
    }

    public interface IRoundView<TId> : IRoundView, IView<TId>
    {
    }
}