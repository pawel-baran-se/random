using HR.LeaveManagement.MVC.Models;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string passwoerd);
        Task<bool> Register(RegisterVM registerVM);
        Task Logout();
    }
}
