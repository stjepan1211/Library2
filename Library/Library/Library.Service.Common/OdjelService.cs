﻿using Library.DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IOdjelService
    {
        Task<int> Add(Odjel entity);
        Task<Odjel> Get(Guid id);
        Task<int> Update(Odjel entity);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Odjel>> GetAll();
    }
}
