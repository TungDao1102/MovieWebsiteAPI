using APIWebMovie.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BillController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllBill")]
        public async Task<IActionResult> GetAllBill()
        {
            var bills = await _unitOfWork.billRepository.GetAll<BillView>();
            if(bills == null)
            {
                return NotFound();
            }
            return Ok(bills);
        }
    }
}
