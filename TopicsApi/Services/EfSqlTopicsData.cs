using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TopicsApi.Services;

public class EfSqlTopicsData : IProvideTopicsData
{
    private readonly TopicsDataContext _context;

    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public EfSqlTopicsData(TopicsDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }


    private IQueryable<Topic> GetTopics()
    {
        return _context.Topics!.Where(t => t.IsDeleted ==false);
    }

    public async Task<GetTopicListItemModel> AddTopicAsync(PostTopicRequestModel request)
    {
        var topic = _mapper.Map<Topic>(request);
        _context.Topics!.Add(topic);
        await _context.SaveChangesAsync();
        var result = _mapper.Map<GetTopicListItemModel>(topic);
        return result;
    }

    public async Task<GetTopicsModel> GetAllTopics()
    {
        var data = await GetTopics().ProjectTo<GetTopicListItemModel>(_config).ToListAsync();

        return new GetTopicsModel(data);
    }

    public async Task<Maybe<GetTopicListItemModel>> GetTopicByIdAsync(int topicId)
    {
        var data = await GetTopics().Where(t => t.Id == topicId).ProjectTo<GetTopicListItemModel>(_config).SingleOrDefaultAsync();

        return data switch
        {
            null => new Maybe<GetTopicListItemModel>(false, null),
            _ => new Maybe<GetTopicListItemModel>(true, data)
        };
    }
}
