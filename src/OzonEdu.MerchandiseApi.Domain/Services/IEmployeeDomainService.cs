﻿using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IEmployeeDomainService
    {
        Task<Employee> FindOrCreateBy(EmployeeParameters parameters, CancellationToken token);
    }
}
