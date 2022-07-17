using System.Threading.Tasks;
using Icon.Api.Models;

namespace Icon.Api.Services
{
    public interface IIconService
    {
        Task LoadHtmlDocument();
        IconlineAwesome GetIcons();
    }
}
