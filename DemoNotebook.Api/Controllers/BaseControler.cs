using DemoNotebook.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace DemoNotebook.Api.Controllers
{
    public class BaseControler : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger<BaseControler> _logger;
        public BaseControler(ILogger<BaseControler> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<User> GetValues()
        {
            var service = _unitOfWork.GetRepository<User>();
            var result = service.GetAll();
            return result;
        }
    }
}
