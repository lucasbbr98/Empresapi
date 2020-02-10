using Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IService<T> where T : Model
    {
        public Task<IEnumerable<T>> GetAll(string fields = null);
        public Task<T> Get(int id, string fields = null);
        public Task<IEnumerable<T>> GetAllWhere(object list, string fields = null);
        public Task<T> GetWhere(object list, string fields = null);
        public Task<bool?> Exists(int id);
        public Task<bool?> ExistsWhere(object list);
        public Task<HttpStatusCode> Add(T obj);
        public Task<HttpStatusCode> Deactivate(T obj);
        public Task<HttpStatusCode> Delete(T obj);
        public Task<HttpStatusCode> DeleteWhere(object list);
        public Task<HttpStatusCode> Update(T obj, string fields = null);
    }
}
