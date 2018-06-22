using System.Threading.Tasks;

namespace InsideAirbnb.Helpers.Cache
{
    public class NoCache : ICache

    {
        public Task<T> Get<T, K>(K key, string identifier)
        {
            return Task.Run<T>(() => default(T));
        }

        public void Set<T, K>(K key, T item, string identifier)
        {
            // noop
        }

        public void Invalidate(string key)
        {
            //noop
        }

        public void Validate(string key)
        {
            // noop
        }
    }
}    