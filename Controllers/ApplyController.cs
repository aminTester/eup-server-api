
using EUP.Data;
using EUP.Data.Enums;
using EUP.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EUP.Controllers
{
    [ApiController]
    [Route("api/apply")]
    public class ApplyController : ControllerBase
    {
        private readonly IApplyService _applyService;
        public ApplyController(IApplyService  applyService)
        {
            _applyService = applyService;    
        }
        [HttpGet("proffesors")]
        public async Task<ActionResult<IEnumerable<ProffesorModel>>> GetAllProffesors()
        {
            try
            {
                var data = await _applyService.GetAllProffesors();
                return Ok(data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("proffesor/{id}")]
        public async Task<ActionResult<ProffesorModel>> GetProfessor(int id)
        {
            var professor = await _applyService.GetProffesor(id);
            if (professor == null)
                return NotFound();
            return Ok(professor);
        }

        [HttpPut("professors/{profId}")]
        public async Task<ActionResult<ProffesorEditModel>> UpdateProf(int profId, [FromBody] ProffesorEditModel profModel)
        {
            if (profModel == null || profId != profModel.Id)
            {
                return BadRequest("Invalid professor data.");
            }

            var updatedProf = await _applyService.UpdateProf(profId, profModel);
            if (updatedProf == null)
            {
                return NotFound("Professor not found or update failed.");
            }

            return Ok(updatedProf);
        }
    }
}
