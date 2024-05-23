using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Application.Response;
using Project.Domain.Abstractions;
using Project.Domain.Entities;
using System.Net;



namespace Project.Application.Features.ChapterFeatures.Queries
{
    public class GetAllChapterQuery : IRequest<IEnumerable<ChapterDTO>>
    {
    }
    public class GetAllChapterHandler : IRequestHandler<GetAllChapterQuery, IEnumerable<ChapterDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllChapterHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChapterDTO>> Handle(GetAllChapterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve all chapters asynchronously from the repository
                var chapterList = await _unitOfWorkDb.chapterQueryRepository.GetAllAsync();

                // Map the chapters to DTOs
                var chapterDtos = chapterList.Select(chapter => _mapper.Map<ChapterDTO>(chapter)).ToList();

                // Return the list of ChapterDTOs
                return chapterDtos;
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                // Re-throw the exception to be handled at a higher level
                throw new Exception($"An error occurred while retrieving chapters: {ex.Message}", ex);
            }
        }

    }
}
