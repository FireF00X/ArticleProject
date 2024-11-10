using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.Specifications.AuthorSpecifications;
using ArticleProject.Web.Helper;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _repo;

        public AuthorController(IUnitOfWork repo)
        {
            _repo = repo;
        }

        // GET: AuthorController
        public async Task<IActionResult> Index(string searchItem)
        {
            List<Author> authors;
            if(searchItem is null)
            {
                authors = await _repo.CreateRepo<Author,int>().GetAllAsync();
            }
            else
            {
                authors = await _repo.CreateRepo<Author,int>().GetAllAsyncWithSpecification(new AuthorSearchingItem(searchItem));
            }
            var mappedAuthors = authors.Select(a => new AuthorViewModel
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.UserName,
                FullName = a.FullName,
                PictureUrl = a.PictureUrl,
                Bio = a.Bio,
                FaceBookLink = a.FaceBookLink,
                InstegramLink= a.InstegramLink,
                XSiteLink = a.XSiteLink
            });
            return View(mappedAuthors);
        }

        // GET: AuthorController/Edit/5
        public IActionResult Edit(int id )
        {
            var author = _repo.CreateRepo<Author,int>().GetById(id);
            var mappedAuthor = new AuthorViewModel()
            {
                Id =author.Id,
                UserId =author.UserId,
                UserName =author.UserName,
                FullName =author.FullName,
                PictureUrl = author.PictureUrl,
                Bio =author.Bio,
                FaceBookLink =author.FaceBookLink,
                InstegramLink =author.InstegramLink,
                XSiteLink =author.XSiteLink
            };
            return View(mappedAuthor);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorViewModel input)
        {
            try
            {                
                var mappedAuthor = new Author()
                {
                    Id = input.Id,
                    UserId = input.UserId,
                    UserName = input.UserName,
                    FullName = input.FullName,
                    PictureUrl = FileSettings.UploadFile(input.PictureFromUser, "UsersImages"),
                    Bio = input.Bio,
                    FaceBookLink = input.FaceBookLink,
                    InstegramLink = input.InstegramLink,
                    XSiteLink = input.XSiteLink
                };
                var author = _repo.CreateRepo<Author, int>().Update(id,mappedAuthor);
                await _repo.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(input);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
           var author  = _repo.CreateRepo<Author, int>().GetById(id);
            FileSettings.DeleteFile(author.PictureUrl, "UsersImages");
            _repo.CreateRepo<Author,int>().Delete(id);
            await _repo.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
