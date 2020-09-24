namespace Logger.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DbContext;
    using Microsoft.EntityFrameworkCore;
    using Models.Entities;
    using Models.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class LoggerRepository : ILoggerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public LoggerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task AddLog(Log log)
        {
            this.dbContext.Log.Add(log);
            await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns></returns>
        public async Task<LogStatistics> GetLogStatistics(string userId, DateTime initialDate, DateTime finalDate)
        {

            var filteredValues = this.dbContext.Set<Log>().Where(x =>
                x.UserId.Equals(userId) && x.DateTimeCreation > initialDate &&
                x.DateTimeCreation < finalDate.AddDays(1));

            var logStatistics = new LogStatistics
            {
                LogCount = await filteredValues.CountAsync(),
                Warinigs = await filteredValues.Where(x => x.Level == MessageLevel.Warning).CountAsync(),
                Errors = await filteredValues.Where(x => x.Level == MessageLevel.Error).CountAsync(),
                Informations = await filteredValues.Where(x => x.Level == MessageLevel.Information).CountAsync(),
            };
            return logStatistics;
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
        public async Task<IEnumerable<Log>> GetLogs(DateTime initialDate, DateTime finalDate, string columnNameToFilter,
            string columnValue, string columnNameToSort)
        {

            var query = this.dbContext.Set<Log>().Where(x => x.DateTimeCreation > initialDate && x.DateTimeCreation < finalDate.AddDays(1));

            if (!string.IsNullOrEmpty(columnNameToFilter))
            {
                query = FilterByTableField(query, columnNameToFilter, columnValue);
            }

            if (!string.IsNullOrEmpty(columnNameToSort))
            {
                query.OrderBy(x => EF.Property<object>(query, columnNameToSort));
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnNameToFilter"></param>
        /// <param name="columnValue"></param>
        private IQueryable<Log> FilterByTableField(IQueryable<Log> query, string columnNameToFilter, string columnValue)
        {
            switch (columnNameToFilter)
            {
                case "Id":
                    query.Where(x => x.Id == Convert.ToInt32(columnValue));
                    break;
                case "Title":
                    query.Where(x => x.Title.Equals(columnValue));
                    break;
                case "Level":
                    query.Where(x => x.Level == MessageLevel.Error);
                    break;
                case "UserId":
                    query.Where(x => x.UserId.Equals(columnValue));
                    break;
                case "DateTimeCreation":
                    query.Where(x => x.DateTimeCreation == Convert.ToDateTime(columnValue));
                    break;
            }

            return query;
        }
    }
}