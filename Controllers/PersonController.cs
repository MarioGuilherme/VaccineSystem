using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VaccineSystem.Data;
using VaccineSystem.Models;

namespace VaccineSystem.Controllers {
    [Authorize]
    public class PersonController : Controller {
        private readonly ApplicationDbContext _context;

        // Construtor da classe que seta o atributo ApplicationDbContext
        public PersonController(ApplicationDbContext context) {
            _context = context;
        }

        // Método responsável por carregar a View da página principal com a listagem de pessoas
        public async Task<IActionResult> Index() {
            return View(await _context.Person.ToListAsync());
        }

        // Método responsável por carregar a View dos detalhes sobre uma pessoa pelo seu id
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
                return NotFound();

            Person person = await _context.Person.FirstOrDefaultAsync(m => m.id_person == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        // Método responsável por carregar a View de cadastro de pessoa
        public IActionResult Create() {
            return View();
        }

        // Método responsável por carregar a View do formulário para edição com os dados da pessoa de acordo com o seu id 
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
                return NotFound();

            Person person = await _context.Person.FindAsync(id);
            if (person == null)
                return NotFound();
            return View(person);
        }

        // Método responsável por carregar a View com os dados de uma pessoa pelo id para confirmar a exclusão
        public async Task<IActionResult> Delete(int? id) {
            if (id == null)
                return NotFound();

            Person person = await _context.Person.FirstOrDefaultAsync(m => m.id_person == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        // Método responsável por cadastrar uma pessoa e retornar um objeto json
        [HttpPost]
        public JsonResult CreateAjax(string full_name, string birth_date, char sex) {
            Response response = new();
            try {
                DateTime dateValue;
                if (DateTime.TryParse(birth_date, out dateValue)) {
                    Person person = new() {
                        full_name = full_name,
                        birth_date = Convert.ToDateTime(birth_date),
                        sex = sex
                    };
                    _context.Add(person);
                    _context.SaveChanges();
                    response.icon = "success";
                    response.msg = "Pessoa cadastrada com sucesso!";
                } else {
                    response.icon = "error";
                    response.msg = "Data e hora inválida!";
                }
            } catch (Exception ex) {
                response.icon = "error";
                response.msg = ex.Message;
            }
            return Json(response);
        }

        // Método responsável por fazer o update nos dados de uma pessoa de acordo com seu ID e retornar um objeto json
        [HttpPost]
        public JsonResult EditAjax(int id, string full_name, string birth_date, char sex) {
            Response response = new();
            try {
                Person person = new() {
                    id_person = id,
                    full_name = full_name,
                    birth_date = Convert.ToDateTime(birth_date),
                    sex = sex
                };
                if (PersonExists(person.id_person)) {
                    _context.Update(person);
                    _context.SaveChanges();
                    response.icon = "success";
                    response.msg = "Pessoa editada com sucesso!!";
                } else {
                    response.icon = "error";
                    response.msg = "Pessoa não encontrada!";
                }
            } catch (Exception ex) {
                response.icon = "error";
                response.msg = ex.Message;
            }
            return Json(response);
        }

        // Método responsável por excluir uma pessoa pelo seu ID e retornar um objeto json
        [HttpPost]
        public JsonResult DeleteAjax(int id) {
            Response response = new();
            try {
                Person person = _context.Person.Find(id);
                _context.Person.Remove(person);
                _context.SaveChanges();
                response.icon = "success";
                response.msg = "Pessoa deletada com sucesso!";
            } catch (Exception ex) {
                response.icon = "error";
                response.msg = ex.Message;
            }
            return Json(response);
        }

        // Método responsável por verificar se existe uma pessoa pelo seu id
        private bool PersonExists(int id) {
            return _context.Person.Any(e => e.id_person == id);
        }
    }
}