using System.Threading.Tasks;
using Models;
using Repository.UnitOfWork;
using Services.Interfaceses;

namespace Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetUserByIdAsync(id);
        }

        public async Task CreateUserAsync(UserDTO model)
        {
            //model.Password = EncryptPassword(model.Password);
            await _unitOfWork.Users.CreateUserAsync(model);
            await _unitOfWork.CommitAsync();
        }

        private string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
