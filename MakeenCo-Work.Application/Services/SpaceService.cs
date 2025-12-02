using MakeenCo_Work.Application.Commands;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly IUnitOfWork _unit;

        public SpaceService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<SpaceDto>> GetAllAsync()
        {
            var spaces = await _unit.Spaces.GetAllAsync();
            return spaces.Select(s => new SpaceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Capacity = s.Capacity,
                HourlyRate = s.HourlyRate,
                DailyRate = s.DailyRate,
                MonthlyRate = s.MonthlyRate,
                IsActive = s.IsActive,
                Location = s.Location,
                ImageUrl = s.ImageUrl
            });
        }

        public async Task<SpaceDto?> GetByIdAsync(Guid id)
        {
            var space = await _unit.Spaces.GetByIdAsync(id);
            if (space == null) return null;

            return new SpaceDto
            {
                Id = space.Id,
                Name = space.Name,
                Description = space.Description,
                Capacity = space.Capacity,
                HourlyRate = space.HourlyRate,
                DailyRate = space.DailyRate,
                MonthlyRate = space.MonthlyRate,
                IsActive = space.IsActive,
                Location = space.Location,
                ImageUrl = space.ImageUrl
            };
        }

        public async Task CreateAsync(CreateSpaceCommand command)
        {
            var space = new Space(command.Name,
                command.Capacity,
                command.HourlyRate,
                command.DailyRate,
                command.MonthlyRate,
                command.Description,
                command.Location);
            await _unit.Spaces.AddAsync(space);
            await _unit.CompleteAsync();
        }

        public async Task UpdateAsync(UpdateSpaceCommand command)
        {
            var space = await _unit.Spaces.GetByIdAsync(command.Id);
            if (space == null) throw new Exception("Space not found");

            space.Update(command.Name,
                command.Description,
                command.Capacity,
                command.HourlyRate,
                command.DailyRate,
                command.MonthlyRate,
                command.Location);

            _unit.Spaces.Update(space);
            await _unit.CompleteAsync();
        }

        public async Task SetActive(Guid id, bool isActive)
        {
            var space = await _unit.Spaces.GetByIdAsync(id);
            if (space == null) throw new Exception("Space not found");

            space.SetActive(isActive);
            await _unit.CompleteAsync();
        }
    }
}
