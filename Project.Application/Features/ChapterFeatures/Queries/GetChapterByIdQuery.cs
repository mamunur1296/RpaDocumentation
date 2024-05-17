using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.ChapterFeatures.Queries
{
    public class GetChapterByIdQuery : IRequest<Response<ChapterDTO>>
    {
        public GetChapterByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetChapterByIdHandler : IRequestHandler<GetChapterByIdQuery, Response<ChapterDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetChapterByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<Response<ChapterDTO>> Handle(GetChapterByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                // Retrieve the Chapter asynchronously by ID
                var chapter = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);

                // Check if the Chapter was found
                if (chapter == null)
                {
                    return new Response<ChapterDTO>
                    {
                        Data = null,
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessage = $"Chapter with ID = {request.Id} not found"
                    };
                }

                // Map the Chapter to a DTO
                var chapterDto = _mapper.Map<ChapterDTO>(chapter);

                // Create a successful response
                return new Response<ChapterDTO>
                {
                    Data = chapterDto,
                    Success = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                return new Response<ChapterDTO>
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = $"An error occurred while retrieving the Chapter. Please try again later. Error: {ex.Message}"
                };
            }
        }
    }
}
