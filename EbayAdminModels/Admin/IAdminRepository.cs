namespace Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminRepository
    {
        Task<int> AddAdminAsync(Admin Admin);
        Task<List<Admin>> GetAdminAsync();
        Task<Admin> GetAdminDetailsAsync(int value);
        Task<int> UpdateAdminAsync(Admin Admin);
        Task<int> DeleteAdminAsync(Admin Admin);
        Task<Admin> GetAdminAsync(string userName, string password);
    }
}
