using AutoMapper;
using MediatR;
using Project.Domain.Abstractions;


namespace Project.Application.Features.TopicFeatures.Commands
{
    public class UpdateTopicCommand : IRequest<string>
    {
        public string title { get; set; }
        public Guid Chapterid { get; set; }
        public UpdateTopicCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
    public class UpdateTopicHandler : IRequestHandler<UpdateTopicCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateTopicHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _unitOfWorkDb.topicQueryRepository.GetByIdAsync(request.Id);
                if (data == null) return default;
                else
                {
                    data.title = request.title;
                    data.Chapterid=request.Chapterid;
                }
                await _unitOfWorkDb.topicCommandRepository.UpdateAsync(data);
                await _unitOfWorkDb.SaveAsync();
                return "Successfully upddate";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
