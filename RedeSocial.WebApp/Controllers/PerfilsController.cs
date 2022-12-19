using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;
using System.Security.Claims;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class PerfilsController : Controller
    {
        private readonly PerfilService _service;

        public PerfilsController(PerfilService perfilService) {
            _service = perfilService;
        }

        // GET: Perfils
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var perfil = _service.ConsultarPerfil(userId);
            if (perfil != null) {
                return RedirectToAction("Details");
            }
            return View(_service.ConsultarPerfils());
        }

        // GET: Perfils/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                id = GetUserId();
            }
            var perfil = _service.ConsultarPerfil(id.Value);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // GET: Perfils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Foto")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                perfil.Id = GetUserId();
                _service.CriarPerfil(perfil);
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: Perfils/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfil = _service.ConsultarPerfil(id.Value);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // POST: Perfils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Foto")] Perfil perfil)
        {
            if (id != perfil.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = _service.AlterarPerfil(perfil);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: Perfils/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfil = _service.ConsultarPerfil(id.Value);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // POST: Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _service.ExcluirPerfil(id);
            return RedirectToAction(nameof(Index));
        }

        private Guid GetUserId() {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
