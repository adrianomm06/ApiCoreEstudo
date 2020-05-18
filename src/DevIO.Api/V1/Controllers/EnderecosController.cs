using AutoMapper;
using DevIO.Api.Controllers;
using DevIO.Api.Extensions;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevIO.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EnderecosController : MainController
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecosController(IEnderecoRepository enderecoRepository, IMapper mapper, IEnderecoService enderecoService, INotificador notificador, IUser user) : base(notificador, user)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
            _enderecoService = enderecoService;
        }

        [HttpGet("{id:guid}")]
        public async Task<EnderecoViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
        }

        [ClaimsAuthorize("Endereco", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _enderecoService.Atualizar(_mapper.Map<Endereco>(enderecoViewModel));

            return CustomResponse(enderecoViewModel);
        }

    }
}
