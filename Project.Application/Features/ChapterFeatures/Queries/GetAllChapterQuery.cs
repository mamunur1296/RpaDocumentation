using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using Project.Domain.Entities;
using System.Net;



namespace Project.Application.Features.ChapterFeatures.Queries
{
    public class GetAllChapterQuery : IRequest<Response<IEnumerable<ChapterDTO>>>
    {
    }
    public class GetAllChapterHandler : IRequestHandler<GetAllChapterQuery, Response<IEnumerable<ChapterDTO>>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllChapterHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ChapterDTO>>> Handle(GetAllChapterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve all chapters asynchronously
                var chapterList = await _unitOfWorkDb.chapterQueryRepository.GetAllAsync();

                // Check if the chapter list is empty
                if (chapterList == null || !chapterList.Any())
                {
                    return new Response<IEnumerable<ChapterDTO>>
                    {
                        Data = null,
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessage = "No chapters found."
                    };
                }

                // Map chapters to DTOs
                var chapterDtos = chapterList.Select(chapter => _mapper.Map<ChapterDTO>(chapter)).ToList();

                // Create a successful response
                return new Response<IEnumerable<ChapterDTO>>
                {
                    Data = chapterDtos,
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "chapters retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                return new Response<IEnumerable<ChapterDTO>>
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = $"An error occurred while retrieving the chapters. Please try again later. Error: {ex.Message}"
                };
            }
        }
    }
}
