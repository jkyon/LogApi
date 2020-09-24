namespace Logger.Service.LoggerServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Repositories;
    using Models.Entities;

    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILoggerRepository repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public LoggerService(ILoggerRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task AddLogInfo(Log log)
        {
            await this.repository.AddLog(log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns></returns>
        public async Task<LogStatistics> GetLoggerStatistics(string userId, DateTime initialDate, DateTime finalDate)
        {
            return await this.repository.GetLogStatistics(userId, initialDate, finalDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <param name="columnNameToFilter"></param>
        /// <param name="columnValue"></param>
        /// <param name="columnNameToSort"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Log>> GetLogDataList(DateTime initialDate, DateTime finalDate,
            string columnNameToFilter = null, string columnValue = null, string columnNameToSort = null)
        {
            return await this.repository.GetLogs(initialDate, finalDate, columnNameToFilter, columnValue, columnNameToSort);
        }
    }
}