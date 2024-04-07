using MediatR;
using Project.Application.Features.QuestionsFeatures.Commands;
using Project.Domain.Abstractions;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.TopicFeatures.Commands
{
    public class CreateTopicCommand : IRequest<string>
    {
        public string title { get; set; }
        public Guid Chapterid { get; set; }

    }
    public class CreateTopicHandler : IRequestHandler<CreateTopicCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateTopicHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;

        }


        public async Task<string> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questions = new Topic()
                {
                    title = request.title,
                    Chapterid=request.Chapterid,
                    Id = new Guid(),
                };
                await _unitOfWorkDb.topicCommandRepository.AddAsync(questions);
                await _unitOfWorkDb.SaveAsync();
                return "created successfully";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
