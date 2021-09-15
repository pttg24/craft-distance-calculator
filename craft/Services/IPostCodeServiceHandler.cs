namespace craft.Services
{
    using CSharpFunctionalExtensions;
    using Domain;
    using System.Threading.Tasks;

    public interface IPostCodeServiceHandler
    {
        Task<Result<PostCodeResponse>> GetPostCodes(string postCode);
    }
}
