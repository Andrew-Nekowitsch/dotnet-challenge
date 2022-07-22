using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Task<List<Compensation>> GetById(String id);
        Compensation Add(Compensation comp);
        Task SaveAsync();
    }
}