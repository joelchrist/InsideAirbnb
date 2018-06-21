using System.Threading.Tasks;

namespace InsideAirbnb.Helpers.Cache

{
    public interface ICache
    {
        Task<T> Get<T, K>(K key, string identifier);
        void Set<T, K>(K key, T item, string identifier);
        void Invalidate(string key);
        void Validate(string key);
    }
}