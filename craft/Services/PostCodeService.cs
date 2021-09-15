namespace craft.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CSharpFunctionalExtensions;
    using Domain;
    using Configuration;
    using MongoDB.Driver;
    using System.Threading.Tasks;
    using MongoDB.Driver.Linq;

    public class PostCodeService : IPostCodeService
    {
        private readonly IPostCodeServiceHandler _postCodeServiceHandler;
        private readonly IMongoCollection<PostCodeRecord> _records;

        public PostCodeService(IPostCodeServiceHandler postCodeServiceHandler, IDatabaseConfiguration settings)
        {
            _postCodeServiceHandler = postCodeServiceHandler;

            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _records = database.GetCollection<PostCodeRecord>(settings.CollectionName);
        }

        public async Task<Result<PostCodeResponse>> GetPostCodes(string postCode) =>
            await (await _postCodeServiceHandler.GetPostCodes(postCode))
                .OnFailure(e => Result.Fail<PostCodeResponse>(e))
                .OnSuccess(async r =>
                {
                    return Result.Ok(r);
                });

        public async Task<PostCodeRecord> CreatePostCodeRecordAsync(PostCodeRecord record)
        {
            await _records.InsertOneAsync(record);
            return await Task.FromResult(record);
        }

        public async Task<IEnumerable<PostCodeRecord>> GetPostCodeRecords()
        {
            var records = await _records.AsQueryable().OrderByDescending(c => c.ProcessedOn).ToListAsync();
            return await Task.FromResult(records.Take(3));
        }
    }
}
