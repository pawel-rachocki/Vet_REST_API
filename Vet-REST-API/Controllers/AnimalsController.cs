using Microsoft.AspNetCore.Mvc;
using Vet_REST_API.Models;

namespace Vet_REST_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    //Get all Animals
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        var animals = Database.GetAnimals();
        return Ok(animals);
    }

    //Get Animal by id
    [HttpGet("{id:int}")]
    public ActionResult<Animal> GetAnimal(int id)
    {
        var animal = Database.GetAnimals().FirstOrDefault(x => x.Id == id);
        if (animal == null)
        {
            return NotFound("Animal with given id does not exist");
        }
        return Ok(animal);
    }

}