using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class PaternityRelationRepository : IPaternityRelationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILoggerManager _logger;

        public PaternityRelationRepository(AppDbContext context,ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaternityRelation> GetById(int id)
        {
            return await _context.PaternityRelations.FindAsync(id);
        }

        public async Task<IEnumerable<PaternityRelation>> GetAll()
        {
            return await _context.PaternityRelations.ToListAsync();
        }

        public async Task<IEnumerable<PaternityRelation>> GetRelatedPaternityByCaseId(string caseId)
        {
            return await _context.PaternityRelations.Where(x => x.PaternityCaseId == caseId).ToListAsync();
        }

        public async Task<PaternityRelation> GetPaternityByParticipantId(int participantId)
        {
            return await _context.PaternityRelations.Where(x => x.ParticipantId == participantId).FirstOrDefaultAsync();
        }

        public async Task<PaternityRelation> Create(GuardianPaternityRelationDTO paternityDTO)
        {
            var paternity = paternityDTO.Adapt<PaternityRelation>();

            paternity.Active = 1;
            paternity.DateCreated = DateTime.Now;
            paternity.DateUpdated = DateTime.Now;

            await _context.PaternityRelations.AddAsync(paternity);
            await _context.SaveChangesAsync();
            _logger.LogInfo($"Paternity {paternity.Id} successfully saved from Guardian");

            return paternity;
        }
    }
}
