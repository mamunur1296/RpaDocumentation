using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;

namespace Project.Application.Features.ChapterFeatures.Commands
{
    public class DeleteChapterCommand : IRequest<Response<string>>
    {
        public DeleteChapterCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteChapterHandler : IRequestHandler<DeleteChapterCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteChapterHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<Response<string>> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            // Retrieve the chapter by ID
            var chapter = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);

            if (chapter == null)
            {
                response.Success = false;
                response.Data = null;
                response.ErrorMessage = $"chapter with ID = {request.Id} not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            try
            {
                // Delete the chapter
                await _unitOfWorkDb.chapterCommandRepository.DeleteAsync(chapter);
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"chapter with ID = {chapter.Id} deleted successfully";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while deleting the chapter. Please try again later. Error: {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
    
}
