using AutoMapper;
using MediatR;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.QuestionsFeatures.Commands
{
    public class UpdateQuestionsCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string answers { get; set; }
        public Guid TopicId { get; set; }
        public Guid chapterId { get; set; }


    }
    public class UpdateQuestionsHandler : IRequestHandler<UpdateQuestionsCommand, Response<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateQuestionsHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }


        public async Task<Response<string>> Handle(UpdateQuestionsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            try
            {
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

                // Update Questions properties
                Questions.ModifiedDate = DateTime.Now;
                Questions.Modified_By = "Admin"; // Ideally, this should be dynamic
                Questions.title=request.title; 
                Questions.answers=request.answers;
                Questions.TopicId= request.TopicId;
                Questions.chapterId= request.chapterId;


                // Perform update operation
                await _unitOfWorkDb.questionCommandRepository.UpdateAsync(Questions);
                await _unitOfWorkDb.SaveAsync();

                // Set successful response
                response.Success = true;
                response.Data = $"Questions with ID = {Questions.Id} updated successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Set failure response with detailed error message
                response.Success = false;
                response.Data = null; // Setting Data to null since there's an error
                response.ErrorMessage = $"An error occurred while updating the Questions. Please try again later. Error: {ex.Message}";
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
