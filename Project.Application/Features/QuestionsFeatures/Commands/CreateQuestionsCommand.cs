using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using Project.Domain.Entities;
using System.Net;


namespace Project.Application.Features.QuestionsFeatures.Commands
{
    public class CreateQuestionsCommand : IRequest<Response<string>>
    {
        public string title { get; set; }
        public string answers { get; set; }
        public Guid TopicId { get; set; }
    }
    public class CreateQuestionsHandler : IRequestHandler<CreateQuestionsCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateQuestionsHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
            
        }
        public async Task<Response<string>> Handle(CreateQuestionsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {
                // Create a new Questions entity
                var questions = new Questions
                {
                    Id = Guid.NewGuid(),
                    Created_By = "Admin", // Ideally, this should be dynamic
                    CreatedDate= DateTime.Now,
                    title=request.title,
                    answers=request.answers,
                    TopicId=request.TopicId,
                };

                // Add the new Questions to the repository
                await _unitOfWorkDb.questionCommandRepository.AddAsync(questions);

                // Save changes to the database
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"Questions with ID = {questions.Id} created successfully!";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while creating the Questions. Please try again later. Error: {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
