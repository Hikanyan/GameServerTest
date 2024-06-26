﻿namespace HikanyanLaboratory.System
{
    public interface ISingleton<T> where T : class
    {
        static T Instance { get; }
        void OnAwake();
        void OnDestroyed();
    }
}