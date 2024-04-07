using AutoMapper;
using MediatR;
using Project.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.QuestionsFeatures.Commands
{
    public class UpdateQuestionsCommand : IRequest<string>
    {
        public string title { get; set; }
        public string answers { get; set; }
        public UpdateQuestionsCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
    public class UpdateQuestionsHandler : IRequestHandler<UpdateQuestionsCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateQuestionsHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }


        public async Task<string> Handle(UpdateQuestionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _unitOfWorkDb.questionQueryRepository.GetByIdAsync(request.Id);
                if (data == null) return default;
                else
                {
                    data.title = request.title;
                    data.answers = request.answers;
                }
                await _unitOfWorkDb.questionCommandRepository.UpdateAsync(data);
                await _unitOfWorkDb.SaveAsync();
                return "Successfully upddate";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
