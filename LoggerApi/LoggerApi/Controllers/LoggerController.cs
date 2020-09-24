namespace LoggerApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Logger.Models.Entities;
    using Logger.Service.LoggerServices;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The logger controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        /// <summary>
        /// Holds an instance of logger service.
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerService"></param>
        public LoggerController(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost("log")]
        public async Task<IActionResult> AddLogInfo([FromBody] Log log)
        {
            if (log == null)
            {
                return this.BadRequest();
            }

            await this.loggerService.AddLogInfo(log);

            return this.Ok("OK");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] string userId, DateTime from, DateTime to)
        {
            var result = await this.loggerService.GetLoggerStatistics(userId, from, to);
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="columnNameToFilter"></param>
        /// <param name="columnValue"></param>
        /// <param name="columnNameToSort"></param>
        /// <returns></returns>
        [HttpGet("logs")]
        public async Task<IActionResult> GetLogs([FromQuery] DateTime from, DateTime to, string columnNameToFilter = null , string columnValue  = null, string columnNameToSort  = null)
        {
            var result = await this.loggerService.GetLogDataList(from, to, columnNameToFilter, columnValue, columnNameToSort);
            return this.Ok(result);
        }

    }
}