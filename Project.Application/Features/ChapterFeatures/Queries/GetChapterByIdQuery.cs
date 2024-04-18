using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Features.QuestionsFeatures.Queries;
using Project.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.ChapterFeatures.Queries
{
    public class GetChapterByIdQuery : IRequest<ChapterDTO>
    {
        public GetChapterByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetChapterByIdHandler : IRequestHandler<GetChapterByIdQuery, ChapterDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetChapterByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<ChapterDTO> Handle(GetChapterByIdQuery request, CancellationToken cancellationToken)
        {
            var chapters = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);
            var chaptersDto = _mapper.Map<ChapterDTO>(chapters);
            return chaptersDto;
        }
    }
}
