using APIWebMovie.Interface;
using APIWebMovie.Models;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIMovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllActor")]
        public async Task<IActionResult> GetAllActor()
        {
            var actors = await _unitOfWork.actorRepository.FindToList<ActorView>(x => !x.IsDelete);
            if(actors == null)
            {
                return NotFound();
            }
            return Ok(actors);
        }

        [HttpGet("GetActorById")]
        public async Task<IActionResult> GetActorById(int actorId)
        {
            var actor = await _unitOfWork.actorRepository.Find<ActorView>(x =>x.ActorId == actorId && !x.IsDelete);
            if(actor == null)
            {
                return NotFound("Actor not found");
            }
            return Ok(actor);
        }

        [HttpPost("AddActor")]
        public async Task<IActionResult> AddActor(ActorView actor)
        {
            var result = await _unitOfWork.actorRepository.Add<ActorView>(actor);
            if (result)
            {
                return Ok("Add success");
            }
            return BadRequest("Add failed");
        }

        [HttpPut("EditActor")]
        public async Task<IActionResult> EditActor(ActorView actorView)
        {
            var actor = await _unitOfWork.actorRepository.FindToEntity(x => x.ActorId == actorView.ActorId);
            if (actor == null)
            {
                return NotFound() ;
            }
            actor.ActorName = actorView.ActorName;
            actor.Avartar = actorView.Avartar;
            actor.IsDelete = actorView.IsDelete;
            var result = await _unitOfWork.actorRepository.Update(actor);
            if (result)
            {
                return Ok("Edit success");
            }
            return BadRequest("Edit failed");
        }
          
        [HttpDelete("DeleteActor")]
        public async Task<IActionResult> DeleteActor(int actorId)
        {
            var actor = await _unitOfWork.actorRepository.FindToEntity(x => x.ActorId == actorId && !x.IsDelete);
            if (actor == null)
            {
                return NotFound("Actor not found");
            }
            actor.IsDelete = true;
            var result = await _unitOfWork.actorRepository.Update(actor);
            if(result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }

        [HttpGet("GetActorByMovieId")]

        public async Task<IActionResult> GetActorByMovieId(int movieId)
        {
            var details = await _unitOfWork.detailActorMovieRepository.FindToList<DetailActorView>(x => x.MovieId == movieId);
            if (details == null)
            {
                return NotFound();
            }
            var listActor = new List<ActorView>();
            foreach (var detail in details)
            {
                var actor = await _unitOfWork.actorRepository.Find<ActorView>(x => x.ActorId == detail.ActorId && !x.IsDelete);
                listActor.Add(actor);
            }
            if (listActor.Count > 0)
            {
                return Ok(listActor);
            }
            return NotFound();
        }
    }
}
