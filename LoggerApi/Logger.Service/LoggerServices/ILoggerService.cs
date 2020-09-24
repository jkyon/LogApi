namespace Logger.Service.LoggerServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Entities;

    public interface ILoggerService
    {
        Task AddLogInfo(Log log);

        Task<LogStatistics> GetLoggerStatistics(string userId, DateTime initialDate, DateTime finalDate);


        Task<IEnumerable<Log>> GetLogDataList(DateTime initialDate, DateTime finalDate, string columnNameToFilter = null , string columnValue  = null, string columnNameToSort  = null);
    }
}