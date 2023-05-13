using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //in memory
        private static List<SuperHero> heroes = new List<SuperHero>
        {
 

        };
        private readonly DataContext _context;
        public SuperHeroController(DataContext context) { 
        _context = context;

        }

        // with IActionResult it's just work, but if I want informaton I'm need ActionResult<List<Object>>>
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            //return Ok(heroes);
            return Ok(await _context.SuperHeroes.ToListAsync()); // from DB
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            // I dont know why but we cant just write Find(id)
            //var hero = heroes.Find(h => h.Id == id);
            var hero = await _context.SuperHeroes.FindAsync(id); //from DB. without await respose body will be next
                                                                 //{"isCompleted": false,"isCompletedSuccessfully": false,"isFaulted": false,"isCanceled": false,
                                                                 //"result": {"id": 1,"name": "Spider-man","firstName": "Peter","lastName": "Parket","place": "New York City"}
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        // if I will use integer or string (not complex type) I'm need for this AddHero(SuperHero hero) add AddHero([FromBody]SuperHero hero)
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heroes.Add(hero);
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync(); //new for save in db
            //return Ok(heroes);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            //var hero = heroes.Find(h => h.Id == request.Id);
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync(); //new for save in db

            // return Ok(heroes);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {

            //var hero = heroes.Find(h => h.Id == id); 
            // we change heroes to dbHero
            var dbHero = await _context.SuperHeroes.FindAsync(id); 
            if (dbHero == null)
                return BadRequest("Hero not found.");
            
            _context.SuperHeroes.Remove(dbHero); //
            await _context.SaveChangesAsync();//
            //return Ok(heroes);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }

}
