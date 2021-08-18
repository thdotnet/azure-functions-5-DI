using System;

namespace DemoAzureFunctions5.Services
{
    public interface ISequentialGuidService
    {
        Guid Generate();
    }
}