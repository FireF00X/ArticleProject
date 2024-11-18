using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.Specifications.CategorySpecifications;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace ArticleProject.Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _repo;

        public CategoriesController(IUnitOfWork repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index(string? searchItem, int? pageIndex = 1, int pageSize=3)
        {
            List<Category> cats;
            var specsParam = new CategorySearchingItem(searchItem, pageIndex, pageSize);
                cats = await _repo.CreateRepo<Category, int>().GetAllAsyncWithSpecification(specsParam);
            var mappedCats = cats.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(mappedCats.ToPagedList(pageIndex ?? 1, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel input)
        {
            if (ModelState.IsValid)
            {
                var mappedCagegory = new Category()
                {
                    Name = input.Name
                };
                var result = await _repo.CreateRepo<Category, int>().AddAsync(mappedCagegory);

                if (result == 1)
                {
                    await _repo.CompleteAsync();
                    return RedirectToAction("Index");
                }

                else
                    return View(input);
            }
            return View(input);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id.HasValue)
            {
                var cat = _repo.CreateRepo<Category, int>().GetById(id.Value);
                var mappedCat = new CategoryViewModel()
                {
                    Id = cat.Id,
                    Name = cat.Name
                };
                return View(viewName, mappedCat);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel input)
        {
            if (ModelState.IsValid)
            {
                var cat = _repo.CreateRepo<Category, int>().GetById(input.Id.Value);
                cat.Name = input.Name;
                var result = _repo.CreateRepo<Category, int>().Update(input.Id.Value, cat);
                await _repo.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(input);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var cat = _repo.CreateRepo<Category, int>().Delete(id.Value);
            await _repo.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
