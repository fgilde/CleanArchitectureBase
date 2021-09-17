using System;
using CleanArchitectureBase.Application.Contracts;

namespace CleanArchitectureBase.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
