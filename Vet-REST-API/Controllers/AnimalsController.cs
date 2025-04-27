using Microsoft.AspNetCore.Mvc;
using Vet_REST_API.Models;

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

}