using MediatR;
using Project.Application.Features.QuestionsFeatures.Commands;
using Project.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.TopicFeatures.Commands
{
    public class DeleteTopicCommand : IRequest<string>
    {
        public DeleteTopicCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteTopicHandler : IRequestHandler<DeleteTopicCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteTopicHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<string> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var date = await _unitOfWorkDb.topicQueryRepository.GetByIdAsync(request.Id);
                if (date == null)
                {
                    return "Data not found";
                }
                await _unitOfWorkDb.topicCommandRepository.DeleteAsync(date);
                await _unitOfWorkDb.SaveAsync();
                return "Completed";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
