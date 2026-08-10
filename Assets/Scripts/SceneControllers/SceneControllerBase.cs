using System;
using Zenject;

namespace EasyField.SceneControllers
{
    public abstract class SceneControllerBase : IInitializable, IDisposable
    {
        public void Initialize()
        {
            SubscribeAll();
            InitDefaultStates();
        }

        public void Dispose() 
        {
            UnsubscribeAll();
        }

        protected abstract void SubscribeAll();

        protected abstract void InitDefaultStates();

        protected abstract void UnsubscribeAll();
    }
}
