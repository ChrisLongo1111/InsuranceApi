using Microsoft.AspNetCore.Mvc;

using Repository;

namespace InsuranceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly ILogger<InsuranceController> _logger;
        private readonly IQuoteRepository quoteRepository;

        public InsuranceController(ILogger<InsuranceController> logger, IQuoteRepository quoteRepository)
        {
            _logger = logger;
            this.quoteRepository = quoteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var quotes = await quoteRepository.GetAllAsync();
            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var quote = await quoteRepository.GetByIdAsync(id);
            return Ok(quote);
        }
    }
}