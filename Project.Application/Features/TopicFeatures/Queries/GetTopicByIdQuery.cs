﻿using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.TopicFeatures.Queries
{
    public class GetTopicByIdQuery : IRequest<TopicDTO>
    {
        public GetTopicByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetTopicByIdHandler : IRequestHandler<GetTopicByIdQuery, TopicDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetTopicByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<TopicDTO> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the Topic asynchronously by ID
                var Topic = await _unitOfWorkDb.topicQueryRepository.GetByIdAsync(request.Id);

                // Check if the Topic was found
                if (Topic == null)
                {
                    throw new NotFoundException("No Topic found.");
                }

                // Map the Topic to a DTO
                var TopicDto = _mapper.Map<TopicDTO>(Topic);

                // Create a successful response
                return TopicDto;
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                throw new Exception($"An error occurred while retrieving Topics: {ex.Message}", ex);
            }
        }
    }
}
