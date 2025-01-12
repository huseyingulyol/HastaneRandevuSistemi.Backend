﻿using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;

namespace Application.Features.Blocks.Commands.Update
{
    public class UpdateBlockCommand : IRequest<UpdateBlockResponse>, ISecuredRequest
    {
        public int Id { get; set; }

        public string No { get; set; }

        public string[] RequiredRoles => ["Admin"];

        public class UpdateBlockCommandHandler : IRequestHandler<UpdateBlockCommand, UpdateBlockResponse>
        {
            private readonly IBlockRepository _blockRepository;
            private readonly IMapper _mapper;

            public UpdateBlockCommandHandler(IBlockRepository blockRepository, IMapper mapper)
            {
                _blockRepository = blockRepository;
                _mapper = mapper;
            }

            public async Task<UpdateBlockResponse> Handle(UpdateBlockCommand request, CancellationToken cancellationToken)
            {
                Block? block = await _blockRepository.GetAsync(p => p.Id == request.Id);
                if (block is null)
                    throw new Exception("Böyle bir veri bulunamadı.");

                Block? blockWithSameNo = await _blockRepository.GetAsync(p => p.No == request.No);
                if (blockWithSameNo is not null)
                {
                    throw new BusinessException("Blok No daha önceden sisteme kaydedilmiş");
                }

                Block mappedBlock = _mapper.Map<Block>(request);

                await _blockRepository.UpdateAsync(mappedBlock);

                UpdateBlockResponse response = _mapper.Map<UpdateBlockResponse>(mappedBlock);

                return response;
            }
        }
    }
}
