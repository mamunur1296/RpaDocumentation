using MediatR;
using Project.Domain.Abstractions;
using System.Net;
using Project.Application.Response;
using Project.Domain.Entities;


namespace Project.Application.Features.ChapterFeatures.Commands
{
    public class CreateChapterCommand  : IRequest<Response<string>>
    {
        public string title { get; set; }

    }
    public class CreateChapterHandler : IRequestHandler<CreateChapterCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateChapterHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;

        }


        public async Task<Response<string>> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {
                // Create a new chapter entity
                var chapter = new Chapter
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Created_By= "Admin",// Ideally, this should be dynamic
                    title =request.title,
                };

                // Add the new chapter to the repository
                await _unitOfWorkDb.chapterCommandRepository.AddAsync(chapter);

                // Save changes to the database
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"chapter with ID = {chapter.Id} created successfully!";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while creating the chapter. Please try again later. Error: {ex.Message}";
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
    
}
