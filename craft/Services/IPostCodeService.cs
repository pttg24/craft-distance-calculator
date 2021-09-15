namespace craft.Services
{
    using System.Collections.Generic;
    using CSharpFunctionalExtensions;
    using Domain;
    using System.Threading.Tasks;

    public interface IPostCodeService
    {
        Task<Result<PostCodeResponse>> GetPostCodes(string postCode);

        Task<PostCodeRecord> CreatePostCodeRecordAsync(PostCodeRecord record);

        Task<IEnumerable<PostCodeRecord>> GetPostCodeRecords();
    }
}
