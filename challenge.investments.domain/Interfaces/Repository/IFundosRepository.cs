﻿using System.Threading.Tasks;
using challenge.investments.domain.Entities;

namespace challenge.investments.domain.Interfaces.Repository
{
    public interface IFundosRepository
    {
        Task<FundosModel> Get();
    }
}
