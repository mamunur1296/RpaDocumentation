using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


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

            try
            {
                // Retrieve the Chapter asynchronously by ID
                var chapter = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);

                // Check if the Chapter was found
                if (chapter == null)
                {
                    throw new NotFoundException("No chapter found.");
                }

                // Map the Chapter to a DTO
                var chapterDto = _mapper.Map<ChapterDTO>(chapter);

                // Create a successful response
                return chapterDto;
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                throw new Exception($"An error occurred while retrieving chapters: {ex.Message}", ex);
            }
        }
    }
}
