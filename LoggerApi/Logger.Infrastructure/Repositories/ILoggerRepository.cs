namespace Logger.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Entities;

    public interface ILoggerRepository
    {
        Task AddLog(Log log);

        Task<LogStatistics> GetLogStatistics(string userId, DateTime initialDate, DateTime finalDate);


        Task<IEnumerable<Log>> GetLogs(DateTime initialDate, DateTime finalDate, string columnNameToFilter, string columnValue, string columnNameToSort);

    }
}