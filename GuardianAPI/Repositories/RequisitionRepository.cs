using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class RequisitionRepository : IRequisitionRepository
    {
        private readonly AppDbContext _context;

        public RequisitionRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<RequisitionDTO> Add(RequisitionDTO requisition)
        {
            var req = requisition.Adapt<Requisition>();
             _context.Requisitions.Add(req);
            await _context.SaveChangesAsync();
            return requisition;
        }

        public async Task<IEnumerable<RequisitionDTO>> GetAllRequisitions()
        {
            var reqs = await _context.Requisitions.ToListAsync();
            return reqs.Adapt<IEnumerable<RequisitionDTO>>();
        }

        public async Task<RequisitionDTO> GetRequisition(int Id)
        {
            var req = await _context.Requisitions.FindAsync(Id);

            return req.Adapt<RequisitionDTO>();
        }       
    }
}
