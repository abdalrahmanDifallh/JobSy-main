using JopSy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JopSy.Interface
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAll();
        Task<Address> GetByIdAsync(int id);
    }
}