using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccineSystem.Data;
using VaccineSystem.Models;

namespace VaccineSystem.Controllers {
    [Authorize]
    public class AddressController : Controller {
        private readonly ApplicationDbContext _context;

        // Construtor da classe que seta o atributo ApplicationDbContext
        public AddressController(ApplicationDbContext context) {
            _context = context;
        }

        // Método responsável por carregar a View da página principal com a listagem de endereços
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Address.Include(a => a.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // Método responsável por carregar a View dos detalhes sobre um endereço pelo seu id
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
                return NotFound();

            Address address = await _context.Address
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.id_address == id);
            if (address == null)
                return NotFound();

            return View(address);
        }

        // Método responsável por carregar a View do formulário para cadastro de endereço
        public IActionResult Create() {
            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name");
            return View();
        }

        // Método responsável por carregar a View do formulário para edição de um endereço
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
                return NotFound();

            Address address = await _context.Address.FindAsync(id);
            if (address == null)
                return NotFound();

            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name", address.id_person);
            return View(address);
        }

        // Método responsável por carregar a View com os dados de um endereço pelo id para confirmar a exclusão
        public async Task<IActionResult> Delete(int? id) {
            if (id == null)
                return NotFound();

            Address address = await _context.Address
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.id_address == id);
            if (address == null)
                return NotFound();

            return View(address);
        }

        // Método responsável por cadastrar um novo endereço
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_address,id_person,street,complement,neighborhood,city,state")] Address address) {
            if (ModelState.IsValid) {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name", address.id_person);
            return View(address);
        }

        // Método responsável por atualizar um endereço pelo seu id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_address,id_person,street,complement,neighborhood,city,state")] Address address) {
            if (id != address.id_address)
                return NotFound();

            if (ModelState.IsValid) {
                try {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!AddressExists(address.id_address))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_person"] = new SelectList(_context.Person, "id_person", "full_name", address.id_person);
            return View(address);
        }

        // Método responsável por excluir um endereço pelo seu ID e retornar um objeto json
        [HttpPost]
        public JsonResult DeleteAjax(int id) {
            Response response = new();
            try {
                Address address = _context.Address.Find(id);
                _context.Address.Remove(address);
                _context.SaveChanges();
                response.icon = "success";
                response.msg = "Endereço deletado com sucesso!";
            } catch (Exception ex) {
                response.icon = "error";
                response.msg = ex.Message;
            }
            return Json(response);
        }

        // Método responsável por verificar se existe um endereço pelo seu id
        private bool AddressExists(int id) {
            return _context.Address.Any(e => e.id_address == id);
        }
    }
}