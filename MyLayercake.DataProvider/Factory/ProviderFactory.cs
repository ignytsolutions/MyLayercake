﻿using MyLayercake.Core;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public abstract class ProviderFactory<TEntity> where TEntity : IEntity {
        public abstract DataProvider<TEntity> GetDataProvider();
    }
}
