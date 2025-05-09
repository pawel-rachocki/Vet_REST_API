using Microsoft.AspNetCore.Mvc;
using Vet_REST_API.Models;
using Vet_REST_API.Models.DTOs;

namespace Vet_REST_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    // Get all Animals
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        var animals = Database.GetAnimals();
        return Ok(animals);
    }

    // Get Animal by id
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
    
    // Add new Animal
    [HttpPost]
    public ActionResult<Animal> AddAnimal(Animal animal)
    {
        var newId = Database.GetAnimals().Any() ? Database.GetAnimals().Max(x => x.Id) + 1 : 1;
        var newAnimal = new Animal
        {
            Id = newId,
            Name = animal.Name,
            Weight = animal.Weight,
            CoatColor = animal.CoatColor,
            Category = animal.Category,
        };
        Database.AddAnimal(newAnimal);
        
        return Created($"api/animals/{animal.Id}", newAnimal);
    }
    
    // PUT /api/animals/{id}
    [HttpPut("{id:int}")]
    public ActionResult<Animal> UpdateAnimal(int id, Animal animal)
    {
        var animalToUpdate = Database.GetAnimals().FirstOrDefault(x => x.Id == id);

        if (animalToUpdate == null)
        {
            return NotFound("Animal with given id does not exist");
        }
        
        animalToUpdate.Name = animal.Name;
        animalToUpdate.Category = animal.Category;
        animalToUpdate.Weight = animal.Weight;
        animalToUpdate.CoatColor = animal.CoatColor;
        
        return NoContent();
    }
    
    // DELETE /api/animals/{id}
    [HttpDelete("{id:int}")]
    public ActionResult<Animal> DeleteAnimal(int id)
    {
        var animalToDelete = Database.GetAnimals().FirstOrDefault(x => x.Id == id);

        if (animalToDelete == null)
        {
            return NotFound("Animal with given id does not exist");
        }
        
        Database.RemoveAnimal(animalToDelete);
        
        return NoContent();
        
    }

    // GET /api/animals/search?name=imię
    [HttpGet("search")]
    public ActionResult<IEnumerable<Animal>> SearchAnimals([FromQuery] string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return BadRequest("Search string cannot be null or empty");
        }
        var animals = Database.GetAnimals().Where(x => x.Name !=null && x.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        if (animals.Count == 0)
        {
            return NotFound("No animal with that name exists");
        }
        return Ok(animals);
    }
    
    //VISITS
    
    // GET /api/animals/{animalId}/visits
    [HttpGet("{animalId:int}/visits")]
    public ActionResult<IEnumerable<Visit>> GetAnimalsVisits(int animalId)
    {
        var animal = Database.GetAnimals().FirstOrDefault(x => x.Id == animalId);
        if (animal == null)
        {
            return NotFound("Animal with given id does not exist");
        }
        
        var visits = Database.GetVisits()
            .Where(v => v.Animal != null && v.Animal.Id == animalId)
            .ToList();
        return Ok(visits);
    }
    
    //POST /api/animals/{animalId}/visits
    [HttpPost("{animalId:int}/visits")]
    public ActionResult<Visit> AddVisitForAnimal(int animalId, [FromBody] VisitDto visit)
    {
        var animal = Database.GetAnimals().FirstOrDefault(x => x.Id == animalId);

        if (animal == null)
        {
            return NotFound("Animal with given id does not exist");
        }
        
        var newVisitId = Database.GetVisits().Any() ? Database.GetVisits().Max(x => x.Id) + 1 : 1;

        var newVisit = new Visit
        {
            Id = newVisitId,
            Animal = animal,
            Date = visit.Date,
            Description = visit.Description,
            Price = visit.Price,
        };
        Database.AddVisit(newVisit);
        return Created(
            $"api/animals/{animalId}/visits/{newVisit.Id}",
            newVisit
        );
    }

}