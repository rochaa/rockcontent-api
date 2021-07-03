using System.Threading.Tasks;

namespace RockContent.Shared.Data
{
    public interface IUnitOfWork
    {
         Task<int> Commit();
    }
}