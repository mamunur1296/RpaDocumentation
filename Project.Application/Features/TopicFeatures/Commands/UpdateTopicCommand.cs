using AutoMapper;
using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.TopicFeatures.Commands
{
    public class UpdateTopicCommand : IRequest<Response<string>>
    {
        public Guid Id { get;  set; }
        public string title { get; set; }
        public Guid Chapterid { get; set; }

    }
    public class UpdateTopicHandler : IRequestHandler<UpdateTopicCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateTopicHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {
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

                // Update Topic properties
                Topic.Modified_By = "Admin"; // Ideally, this should be dynamic
                Topic.ModifiedDate = DateTime.Now;
                Topic.title = request.title;
                Topic.Chapterid = request.Chapterid;

                // Perform update operation
                await _unitOfWorkDb.topicCommandRepository.UpdateAsync(Topic);
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"Topic with ID = {Topic.Id} updated successfully";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while updating the Topic. Please try again later. Error: {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
