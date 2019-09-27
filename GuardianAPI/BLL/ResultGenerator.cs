using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.BLL
{
    public class ResultGenerator : IResultGenerator
    {
        private readonly AppDbContext _context;

        public ResultGenerator(AppDbContext context)
        {
            _context = context;
        }        


        /// <summary>
        /// Runs after midnight and sends the previous days Result IDs and participant data
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public GuardianResultsDailyResponseDTO ResultResponse()
        {
            var results = _context.Results
                .Select(x => new { 
                    x.Id,
                    x.PID_2_1,
                    x.PID_5_1,
                    x.PID_5_2,
                    x.DateCreated
                })
                .Where(r => r.DateCreated == DateTime.Now.AddDays(-1));

            var resultsDTO = results.Adapt<GuardianResultsDailyResponseDTO>();

            return resultsDTO;


        }
    }
}
