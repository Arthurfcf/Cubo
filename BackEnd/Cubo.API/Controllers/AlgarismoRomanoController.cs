using AutoMapper;
using Cubo.API.ViewModels;
using Cubo.Domain.Interfaces;
using Cubo.Domain.Interfaces.Repository;
using Cubo.Domain.Interfaces.Service;
using Cubo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlgarismoRomanoController : MainController
    {

        private readonly IAlgarismoRomanoRepository _algarismoRomanoRepository;
        private readonly IAlgarismoRomanoService _algarismoRomanoService;
        private readonly IMapper _mapper;

        public AlgarismoRomanoController(INotificator notificator,
                                 IAlgarismoRomanoRepository algarismoRomanoRepository,
                                 IAlgarismoRomanoService algarismoRomanoService,
                                 IMapper mapper,
                               IUser user) : base(notificator, user)
        {
            _algarismoRomanoRepository = algarismoRomanoRepository;
            _algarismoRomanoService = algarismoRomanoService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<AlgarismoRomanoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<AlgarismoRomanoViewModel>>(await _algarismoRomanoRepository.GetAll());
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AlgarismoRomanoViewModel>> Add(AlgarismoRomanoViewModel algarismoRomanoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _algarismoRomanoService.Add(_mapper.Map<AlgarismoRomano>(algarismoRomanoViewModel));

            return CustomResponse(algarismoRomanoViewModel);
        }
    }
}
