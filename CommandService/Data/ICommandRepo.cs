using System;
using System.Collections.Generic;
using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        //! Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(Guid platformId);

        //! Commands
        IEnumerable<Command> GetCommandsForPlatform(Guid platformId);
        Command GetCommand(Guid platformId, Guid commandId);
        void CreateCommand(Guid platformId, Command command);
    }
}
