using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.Write("-->Getting all platforms ...");
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repo.GetAllPlatforms()));
        }

        [HttpGet("{id}")]
        public ActionResult<PlatformReadDto> GetPlatformById(Guid id)
        {
            var platformItem = _repo.GetPlatFormById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto toBeCreatedPlatform)
        {
            var platformModel = _mapper.Map<Platform>(toBeCreatedPlatform);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            return Ok(_mapper.Map<PlatformReadDto>(platformModel));
        }
    }
}
