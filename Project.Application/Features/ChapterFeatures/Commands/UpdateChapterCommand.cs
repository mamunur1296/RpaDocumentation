using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.ChapterFeatures.Commands
{
    public class UpdateChapterCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string title { get; set; }

    }
    public class UpdateChapterHandler : IRequestHandler<UpdateChapterCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateChapterHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<Response<string>> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {
                // Retrieve the chapter by ID
                var chapter = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);

                if (chapter == null)
                {
                    response.Success = false;
                    response.Data = null;
                    response.ErrorMessage = $"chapter with ID = {request.Id} not found";
                    response.Status = HttpStatusCode.NotFound;
                    return response;
                }

                // Update chapter properties
                chapter.ModifiedDate = DateTime.Now;
                chapter.Modified_By = "Admin"; // Ideally, this should be dynamic
                chapter.title = request.title;

                // Perform update operation
                await _unitOfWorkDb.chapterCommandRepository.UpdateAsync(chapter);
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"chapter with ID = {chapter.Id} updated successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while updating the chapter. Please try again later. Error: {ex.Message}";
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
