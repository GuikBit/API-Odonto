using DentistaApi.Models;

namespace DentistaApi.Services;

public interface IAuthService
{
    
    public Task<IReturn<string>> Login(UserInfo model);

    public interface IReturn<T>
    {
        public EReturnStatus Status { get; }
        public T Result { get; }
        public User Usuario { get; }
    }
}
