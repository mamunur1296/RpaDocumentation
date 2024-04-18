using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domain.Abstractions;


namespace Project.Application.Features.TopicFeatures.Queries
{
    public class GetTopicByIdQuery : IRequest<TopicDTO>
    {
        public GetTopicByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetTopicByIdHandler : IRequestHandler<GetTopicByIdQuery, TopicDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetTopicByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<TopicDTO> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            var topics = await _unitOfWorkDb.topicQueryRepository.GetByIdAsync(request.Id);
            var topicsDto = _mapper.Map<TopicDTO>(topics);
            return topicsDto;
        }
    }

}
