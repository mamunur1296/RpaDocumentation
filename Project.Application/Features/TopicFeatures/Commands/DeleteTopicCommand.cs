using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.TopicFeatures.Commands
{
    public class DeleteTopicCommand : IRequest<Response<string>>
    {
        public DeleteTopicCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteTopicHandler : IRequestHandler<DeleteTopicCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteTopicHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<Response<string>> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            // Retrieve the Topic by ID
            var Topic = await _unitOfWorkDb.topicQueryRepository.GetByIdAsync(request.Id);

            if (Topic == null)
            {
                response.Success = false;
                response.Data = null;
                response.ErrorMessage = $"Topic with ID = {request.Id} not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            try
            {
                // Delete the Topic
                await _unitOfWorkDb.topicCommandRepository.DeleteAsync(Topic);
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"Topic with ID = {Topic.Id} deleted successfully";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while deleting the Topic. Please try again later. Error: {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
