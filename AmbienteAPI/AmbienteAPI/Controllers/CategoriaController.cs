using AmbienteAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAPI.Model;
using WebAPI.Controllers;

namespace AmbienteAPI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApiService _apiService;

        public CategoriaController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var categoria = await _apiService.GetAllCategoriasAsync();
            return View(categoria);
        }


        //create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            _apiService.PostCategoriaAsync(categoria);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(long? id)
        {
            var categoria = await _apiService.GetCategoriaAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        //CORRIGIR O EDIT DA CATEGORIA
        public async Task<IActionResult> Edit(long? id)
        {
            var categoria = await _apiService.GetCategoriaAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Categoria categoria)
        {
            if (id != categoria.CategoriasId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _apiService.PutCategoriaAsync(id, categoria);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o produto.");
            }

            return View(categoria);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            var categoria = await _apiService.GetCategoriaAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {

            var response = await _apiService.DeleteCategoria(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
        }



        


    }
}
