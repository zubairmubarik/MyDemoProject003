using MyDemoProject003.Domain.Entities;

namespace MyDemoProject003.Application.Common.Interfaces
{
    public interface IJwtHelperLibrary
    {
        string CreateJsonWebTokenWithSymetricEncryption(CustomerDocument customer);

        string CreateJsonWebTokenWithASymetricEncryption(CustomerDocument customer);
    }
}
