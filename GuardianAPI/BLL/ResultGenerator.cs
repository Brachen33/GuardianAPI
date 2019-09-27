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
        public List<GuardianResultsDailyResponseDTO> ResultResponse()
        {
            // Date for testing on local
            var testingDate = new DateTime(2017, 01, 17, 03, 00, 11);

            var results = _context.Results
                .Select(x => new Result
                {
                    Id = x.Id,
                    PID_2_1 = x.PID_2_1,
                    PID_5_1 = x.PID_5_1,
                    PID_5_2 = x.PID_5_2,
                    DateCreated = x.DateCreated
                })
                .Where(x => x.DateCreated == testingDate).ToList();
            //   .Where(r => r.DateCreated == DateTime.Now.AddDays(-1));

            var resultsDTO = results.Adapt<List<GuardianResultsDailyResponseDTO>>(); 

            return resultsDTO;
        }
    }
}
