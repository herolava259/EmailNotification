using AutoMapper;
using Cart.Application.Queries;
using Cart.Application.Responses;
using Cart.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Handlers
{
    public class GetListItemByIdQueryHandler: IRequestHandler<GetListItemByIdQuery, ListItemResponse>
    {
        private readonly IListItemRepository _listItemRepository;
        private readonly IMapper _mapper;

        public GetListItemByIdQueryHandler(IListItemRepository listItemRepository, IMapper mapper)
        {
            this._listItemRepository = listItemRepository;
            this._mapper = mapper;
        }

        public async Task<ListItemResponse> Handle(GetListItemByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _listItemRepository.GetAsync(c => c.Id == request.Id, tracked: false,
                                            includeProperties: "");
            if (entity is null) return default!;
            return _mapper.Map<ListItemResponse>(entity);
        }
    }
}
