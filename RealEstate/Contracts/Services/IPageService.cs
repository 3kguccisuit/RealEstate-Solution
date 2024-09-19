using System.Windows.Controls;

namespace RealEstate.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
