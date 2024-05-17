using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using Project.Domain.Entities;
using System.Net;


namespace Project.Application.Features.TopicFeatures.Commands
{
    public class CreateTopicCommand : IRequest<Response<string>>
    {
        public string title { get; set; }
        public Guid Chapterid { get; set; }

    }
    public class CreateTopicHandler : IRequestHandler<CreateTopicCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateTopicHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;

        }


        public async Task<Response<string>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {
                // Create a new Topic entity
                var Topic = new Topic
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Created_By = "Admin", // Ideally, this should be dynamic
                    title = request.title,
                    Chapterid= request.Chapterid
                };

                // Add the new Topic to the repository
                await _unitOfWorkDb.topicCommandRepository.AddAsync(Topic);

                // Save changes to the database
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"Topic with ID = {Topic.Id} created successfully!";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while creating the Topic. Please try again later. Error: {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
