using System.Diagnostics;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using TaskMaster.Data;
using TaskMaster.Models;


namespace TaskMaster.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }



    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> StajyerList()
    {
        //var stajyers = await _context.Stajyers.ToListAsync();
        return View(await _context.Stajyers.ToListAsync());
    }

    private readonly DataContext _context;
    public HomeController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Stajyer model) //veritabanı bağlantısı
    {
        _context.Stajyers.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> EditStajyer(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var stj = await _context
        .Stajyers
        .Include(s => s.TaskSaves)
        .ThenInclude(s => s.Task)
        .FirstOrDefaultAsync(s => s.StajyerId == id); // find ıncludle çalışmaz (findasync(id))

        // var stj = await _context.Stajyers.FirstOrDefaultAsync(s => s.StajyerId == id);

        if (stj == null)
        {
            return NotFound();
        }
        return View(stj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // get edenle post edenin aynı olduğunu sorgular
    public async Task<IActionResult> EditStajyer(int id, Stajyer model)
    {
        if (id != model.StajyerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Stajyers.Any(s => s.StajyerId == model.StajyerId)) // !koyarak true değer dönerse serverda böyle bir kayıt var mı any bunu sorgular varsa true döner ünlem koyarak falseye çevirip notfound gönderiyoruz. 
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("StajyerList");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteStajyer(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stajyer = await _context.Stajyers.FindAsync(id);

        if (stajyer == null)
        {
            return NotFound();
        }


        return View(stajyer);
    }


    [HttpPost]
    public async Task<IActionResult> DeleteStajyer([FromForm] int id)
    {
        var stajyer = await _context.Stajyers.FindAsync(id);

        if (stajyer == null)
        {
            return NotFound();
        }

        _context.Stajyers.Remove(stajyer);
        await _context.SaveChangesAsync();
        return RedirectToAction("StajyerList");
    }

    public async Task<IActionResult> TaskCreate()
    {

        ViewBag.Engineers = new SelectList(await _context.Engineers.ToListAsync(), "EngineerId", "NameUsername");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> TaskCreate(Data.Task model)
    {
        _context.Tasks.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("TaskList");
    }

    [HttpGet]
    public async Task<IActionResult> TaskList()
    {
        var tasks = await _context
        .Tasks
        .Include(t => t.Engineer)
        .ToListAsync();
        return View(tasks);
    }

    [HttpGet]
    public async Task<IActionResult> TaskEdit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var task = await _context
        .Tasks
        .Include(t => t.TaskSaves)
        .ThenInclude(t => t.Stajyer)
        .FirstOrDefaultAsync(t => t.TaskId == id);

        if (task == null)
        {
            return NotFound();
        }
        ViewBag.Engineers = new SelectList(await _context.Engineers.ToListAsync(), "EngineerId", "NameUsername");
        return View(task);



    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TaskEdit(int id, TaskViewModel model)
    {
        if (id != model.TaskId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(new Data.Task() { TaskId = model.TaskId, Title = model.Title, EngineerId = model.EngineerId });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(t => t.TaskId == model.TaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("TaskList");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> TaskDelete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> TaskDelete([FromForm] int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return RedirectToAction("TaskList");

    }

    [HttpGet]
    public async Task<IActionResult> TaskSaveCreate()
    {
        ViewBag.Stajyers = new SelectList(await _context.Stajyers.ToListAsync(), "StajyerId", "NameUsername");
        ViewBag.Tasks = new SelectList(await _context.Tasks.ToListAsync(), "TaskId", "Title");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TaskSaveCreate(TaskSave model)
    {
        model.TaskSaveDate = DateTime.Now;
        _context.TaskSaves.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction("TaskSave");
    }

    public async Task<IActionResult> TaskSave()
    {
        var taskSaves = await _context
        .TaskSaves
        .Include(x => x.Stajyer)  // ınclude metodu joinleri çalıştırır ve tasksaveden stajyer modeline geçer ve ordaki entityleri de getirir. (navigation Properties)
        .Include(x => x.Task)
        .ToListAsync();
        return View(taskSaves);
    }

    public async Task<IActionResult> EngineerList()
    {
        return View(await _context.Engineers.ToListAsync());
    }

    public IActionResult EngineerCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EngineerCreate(Engineer model)
    {
        _context.Engineers.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("EngineerList");
    }
    [HttpGet]
    public async Task<IActionResult> EditEngineer(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = await _context
        .Engineers
        .FirstOrDefaultAsync(s => s.EngineerId == id); // find ıncludle çalışmaz (findasync(id))

        // var stj = await _context.Stajyers.FirstOrDefaultAsync(s => s.StajyerId == id);

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // get edenle post edenin aynı olduğunu sorgular
    public async Task<IActionResult> EditEngineer(int id, Engineer model)
    {
        if (id != model.EngineerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Engineers.Any(s => s.EngineerId == model.EngineerId)) // !koyarak true değer dönerse serverda böyle bir kayıt var mı any bunu sorgular varsa true döner ünlem koyarak falseye çevirip notfound gönderiyoruz. 
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("EngineerList");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteEngineer(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _context.Engineers.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }


        return View(entity);
    }


    [HttpPost]
    public async Task<IActionResult> DeleteEngineer([FromForm] int id)
    {
        var enginner = await _context.Engineers.FindAsync(id);

        if (enginner == null)
        {
            return NotFound();
        }

        _context.Engineers.Remove(enginner);
        await _context.SaveChangesAsync();
        return RedirectToAction("EngineerList");
    }


}
// Veritabanı işlerinde async kullanmak daha mantıklı yoksa süre uzar.

