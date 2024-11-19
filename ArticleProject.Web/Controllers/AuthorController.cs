using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.Specifications.AuthorSpecifications;
using ArticleProject.Web.Helper;
using ArticleProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Web.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _repo;
        private readonly IAuthorizationService _auth;

        public AuthorController(IUnitOfWork repo, IAuthorizationService auth)
        {
            _repo = repo;
            _auth = auth;
        }
        // GET: AuthorController
        [Authorize("Admin")]
        public async Task<IActionResult> Index(string searchItem)
        {
            List<Author> authors;
            if (searchItem is null)
            {
                authors = await _repo.CreateRepo<Author, int>().GetAllAsync();
            }
            else
            {
                authors = await _repo.CreateRepo<Author, int>().GetAllAsyncWithSpecification(new AuthorSearchingItem(searchItem));
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
                InstegramLink = a.InstegramLink,
                XSiteLink = a.XSiteLink
            });
            return View(mappedAuthors);
        }

        // GET: AuthorController/Edit/5
        public IActionResult Edit(int id)
        {
            var author = _repo.CreateRepo<Author, int>().GetById(id);
            var mappedAuthor = new AuthorViewModel()
            {
                Id = author.Id,
                UserId = author.UserId,
                UserName = author.UserName,
                FullName = author.FullName,
                PictureUrl = author.PictureUrl,
                Bio = author.Bio,
                FaceBookLink = author.FaceBookLink,
                InstegramLink = author.InstegramLink,
                XSiteLink = author.XSiteLink
            };
            return View(mappedAuthor);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AuthorViewModel input)
        {
            try
            {
                var author = _repo.CreateRepo<Author, int>().GetById(id.Value);
                if (input.PictureFromUser is not null)
                {
                    if (author.PictureUrl is not null)
                        FileSettings.DeleteFile(author.PictureUrl, "UsersImages");
                    author.PictureUrl = FileSettings.UploadFile(input.PictureFromUser, "UsersImages");
                }
                author.FullName = input.FullName;
                author.Bio = input.Bio;
                author.XSiteLink = input.XSiteLink;
                author.FaceBookLink = input.FaceBookLink;
                author.InstegramLink = input.InstegramLink;

                var updateResult = _repo.CreateRepo<Author, int>().Update(id.Value, author);
                if (updateResult == 1)
                {
                    await _repo.CompleteAsync();
                    var result = await _auth.AuthorizeAsync(User, "Admin");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else return RedirectToAction("Index", "Admin");
                }
                return View(input);
            }
            catch
            {
                return View(input);
            }
        }
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = _repo.CreateRepo<Author, int>().GetById(id);
            FileSettings.DeleteFile(author.PictureUrl, "UsersImages");
            _repo.CreateRepo<Author, int>().Delete(id);
            await _repo.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
