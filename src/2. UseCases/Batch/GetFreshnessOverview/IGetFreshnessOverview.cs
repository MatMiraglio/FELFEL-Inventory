using FELFEL.Domain;
using System.Threading.Tasks;

namespace FELFEL.UseCases.GetFreshnessOverview
{
    public interface IGetFreshnessOverview
    {
        Task<FreshnessOverview> ExecuteAsync();
    }
}