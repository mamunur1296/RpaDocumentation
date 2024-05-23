using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.QuestionsFeatures.Commands
{
    public class DeleteQuestionsCommand : IRequest<Response<string>>
    {
        public DeleteQuestionsCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteQuestionsHandler : IRequestHandler<DeleteQuestionsCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteQuestionsHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<Response<string>> Handle(DeleteQuestionsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            // Retrieve the Questions by ID
            var Questions = await _unitOfWorkDb.questionQueryRepository.GetByIdAsync(request.Id);

            if (Questions == null)
            {
                response.Success = false;
                response.Data = null;
                response.ErrorMessage = $"Questions with ID = {request.Id} not found";
                response.Status = HttpStatusCode.NotFound;
                return response;
            }

            try
            {
                // Delete the Questions
                await _unitOfWorkDb.questionCommandRepository.DeleteAsync(Questions);
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"Questions with ID = {Questions.Id} deleted successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while deleting the Questions. Please try again later. Error: {ex.Message}";
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
