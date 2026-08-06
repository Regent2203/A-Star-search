using System;
using Zenject;

namespace EasyField.Starters
{
    public abstract class StarterBase : IInitializable, IDisposable
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
