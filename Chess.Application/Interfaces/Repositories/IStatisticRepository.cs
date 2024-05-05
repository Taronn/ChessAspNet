using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Application.Interfaces.Repositories;
public interface IStatisticRepository
{
    Task<Statistic> AddAsync(Statistic statistic);
}
