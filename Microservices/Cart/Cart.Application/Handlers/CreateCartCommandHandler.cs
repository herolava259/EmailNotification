using AutoMapper;
using Cart.Application.Commands;
using Cart.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Handlers
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Guid>
    {
        
        private readonly ICartRepository _cartRepository;

        public CreateCartCommandHandler(ICartRepository cartRepository)
        {
            
            this._cartRepository = cartRepository;
        }
        public async Task<Guid> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.CreateAsync(new());

            return result;
        }
    }
}
