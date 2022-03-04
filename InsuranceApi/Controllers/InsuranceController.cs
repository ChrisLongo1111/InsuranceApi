using Microsoft.AspNetCore.Mvc;

using Repository;
using Repository.Models;

namespace InsuranceApi.Controllers
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly ILogger<InsuranceController> logger;
        private readonly IQuoteRepository quoteRepository;

        public InsuranceController(ILogger<InsuranceController> logger, IQuoteRepository quoteRepository)
        {
            this.logger = logger;
            this.quoteRepository = quoteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quotes = await quoteRepository.GetAllAsync();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var quote = await quoteRepository.GetByIdAsync(id);
                return Ok(quote);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var results = await quoteRepository.DeleteAsync(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Quote quote)
        {
            try
            {
                var results = await quoteRepository.UpdateAsync(quote);
                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Quote quote)
        {
            try
            {
                var results = await quoteRepository.AddAsync(quote);
                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
                return BadRequest(ex.Message);
            }
        }
    }
}