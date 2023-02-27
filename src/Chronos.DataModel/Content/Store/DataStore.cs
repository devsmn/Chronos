using Chronos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel
{
    public static class DataStore
    {
        private static Dictionary<Type, ChronosRepository> stores = new Dictionary<Type, ChronosRepository>();

        public static void Register<TRepository>()
            where TRepository : ChronosRepository
        {
            var instance = Activator.CreateInstance<TRepository>();

            stores.Add(typeof(TRepository), instance);
        }

        public static async Task InitializeAsync()
        {
            foreach (var instance in stores.Values)
            {
                // TODO: Parallel
                await instance.InitializeAsync();
            }
        }

        internal static TRepository Resolve<TRepository>()
            where TRepository : ChronosRepository
        {
            foreach (var store in stores)
            {
                if (store.Key is TRepository || store.Key.IsSubclassOf(typeof(TRepository)))
                    return (TRepository)store.Value;
            }

            return null;
        }
    }
}
