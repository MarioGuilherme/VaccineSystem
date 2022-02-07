using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccineSystem.Data;
using VaccineSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace VaccineSystem.Controllers {
    [Authorize]
    public class VaccineController : Controller {
        private readonly ApplicationDbContext _context;

        // Construtor da classe que seta o atributo ApplicationDbContext
        public VaccineController(ApplicationDbContext context) {
            _context = context;
        }

        // Método responsável por carregar a View da página principal com a listagem de vacinas
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Vaccine.Include(v => v.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // Método responsável por carregar a View dos detalhes sobre uma vacina pelo seu id
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
                return NotFound();

            Vaccine vaccine = await _context.Vaccine.Include(v => v.Person).FirstOrDefaultAsync(m => m.id_vaccine == id);

            if (vaccine == null)
                return NotFound();

            return View(vaccine);
        }

        // Método responsável por carregar a View do formulário para cadastro de vacina
        public IActionResult Create() {
            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name");
            return View();
        }

        // Método responsável por carregar a View do formulário para edição de uma vacina
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
                return NotFound();

            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine == null)
                return NotFound();

            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name", vaccine.id_person);
            return View(vaccine);
        }

        // Método responsável por carregar a View com os dados de uma vacina pelo id para confirmar a exclusão
        public async Task<IActionResult> Delete(int? id) {
            if (id == null)
                return NotFound();

            var vaccine = await _context.Vaccine
                .Include(v => v.Person)
                .FirstOrDefaultAsync(m => m.id_vaccine == id);
            if (vaccine == null)
                return NotFound();

            return View(vaccine);
        }

        // Método responsável por cadastrar uma vacina
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_vaccine,id_person,vaccine_name,vaccine_value,vaccine_date")] Vaccine vaccine) {
            if (ModelState.IsValid) {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name", vaccine.id_person);
            return View(vaccine);
        }

        // Método responsável por atualizar os dados de uma vacina de acordo com seu id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_vaccine,id_person,vaccine_name,vaccine_value,vaccine_date")] Vaccine vaccine) {
            if (id != vaccine.id_vaccine)
                return NotFound();

            if (ModelState.IsValid) {
                try {
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!VaccineExists(vaccine.id_vaccine))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name", vaccine.id_person);
            return View(vaccine);
        }

        // Método responsável por excluir uma vacina pelo seu ID e retornar um objeto json
        [HttpPost]
        public JsonResult DeleteAjax(int id) {
            Response response = new();
            try {
                Address address = _context.Address.Find(id);
                _context.Address.Remove(address);
                _context.SaveChanges();
                response.icon = "success";
                response.msg = "Vacina deletada com sucesso!";
            } catch (Exception ex) {
                response.icon = "error";
                response.msg = ex.Message;
            }
            return Json(response);
        }

        // Método responsável por verificar se existe uma vacina pelo seu id
        private bool VaccineExists(int id) {
            return _context.Vaccine.Any(e => e.id_vaccine == id);
        }
    }
}