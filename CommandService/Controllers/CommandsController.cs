using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/command/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(Guid platformId)
        {
            Console.WriteLine($"CommandService --> Getting Commands from PlatformId: {platformId}");
            if (!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            var commands = _repo.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(Guid platformId, Guid commandId)
        {
            Console.WriteLine($"CommandService --> Getting Command from PlatformId & CommandId: {platformId} # {commandId}");
            if (!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            var command = _repo.GetCommand(platformId, commandId);
            // if (commandId == null) return NotFound();
            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(Guid platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"CommandService --> Creating Command for  PlatformId: {platformId}");
            if (!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            var command = _mapper.Map<Command>(commandCreateDto);
            _repo.CreateCommand(platformId, command);
            _repo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return Ok(commandReadDto);
            //! return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }
}
