﻿using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface ICompanyRepository
    {
        Company GetCompany(int id);
      
    }
}
