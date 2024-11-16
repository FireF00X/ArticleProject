using ArticleProject.Core.Entities;
using ArticleProject.Data.IRepository;
using ArticleProject.Data.Specifications.PostsSpecifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;
using ArticleProject.Web.Helper;
using ArticleProject.Data.Specifications.AuthorSpecifications;

namespace ArticleProject.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IUnitOfWork _repo;
        private readonly IAuthorizationService _authorizationService;

        public PostsController(IUnitOfWork repo,
                               IAuthorizationService authorizationService)
        {
            _repo = repo;
            _authorizationService = authorizationService;
        }
        public async Task<IActionResult> Index(string? searchItem, int? pageIndex, int pageSize = 3)
        {
            var result = await _authorizationService.AuthorizeAsync(User, "Admin");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).ToString().Split(":")[2].Trim(); ;
            List<AuthorPost> posts;
            if (result.Succeeded)
            {
                posts = await _repo.CreateRepo<AuthorPost, int>().GetAllAsyncWithSpecification(new PostsSearchingItem(userId, searchItem));
            }
            else
            {
                posts = await _repo.CreateRepo<AuthorPost, int>().GetAllAsyncWithSpecification(new PostsSearchingItem(userId, searchItem));
            }
            var mappedPost = posts.Select(p => new AuthorPostViewModel()
            {
                Id = p.Id,
                UserId = userId,
                UserName = p.UserName,
                FullName = p.FullName,
                AuthorId = p.AuthorId,
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt,
                PostCategory = p.PostCategory,
                PostDescription = p.PostDescription,
                PostImageName = p.PostImageUrl,
                PostTitle = p.PostTitle
            });
            return View(mappedPost.ToPagedList(pageIndex ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorPostViewModel input)
        {
            if (!ModelState.IsValid) return View(input);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).ToString().Split(":")[2].Trim();
            var authors = await _repo.CreateRepo<Author, int>().GetAllAsyncWithSpecification(new AuthorSearchingItem(userId));
            var author = authors.FirstOrDefault();
            var mappedPost = new AuthorPost()
            {
                Id = input.Id,
                UserId = userId,
                UserName = author.UserName,
                FullName = author.FullName,
                AuthorId = author.Id,
                CategoryId = input.CategoryId,
                CreatedAt = input.CreatedAt,
                PostCategory = _repo.CreateRepo<Category, int>().GetById(input.CategoryId).Name,
                PostDescription = input.PostDescription,
                PostImageUrl = FileSettings.UploadFile(input.PostImageUrl, "PostImages"),
                PostTitle = input.PostTitle
            };
            await _repo.CreateRepo<AuthorPost, int>().AddAsync(mappedPost);
            await _repo.CompleteAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id.Value < 0 || id is null) return BadRequest();
            var post = _repo.CreateRepo<AuthorPost, int>().GetById(id.Value);
            var mappedPost = new AuthorPostViewModel()
            {
                Id = post.Id,
                UserId = post.UserId,
                UserName = post.UserName,
                FullName = post.FullName,
                AuthorId = post.AuthorId,
                CategoryId = post.CategoryId,
                CreatedAt = post.CreatedAt,
                PostCategory = post.PostCategory,
                PostDescription = post.PostDescription,
                PostImageName = post.PostImageUrl,
                PostTitle = post.PostTitle
            };
            return View(viewName, mappedPost);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id.Value, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, AuthorPostViewModel input)
        {
            if (ModelState.IsValid)
            {
                if (input.Id == id)
                {
                    var post = _repo.CreateRepo<AuthorPost, int>().GetById(id.Value);
                    if (input.PostImageUrl is not null)
                    {
                        FileSettings.DeleteFile(post.PostImageUrl, "PostImages");
                        post.PostImageUrl = FileSettings.UploadFile(input.PostImageUrl, "PostImages");
                    }
                    post.PostTitle = input.PostTitle;
                    post.PostDescription = input.PostDescription;
                    post.CategoryId = input.CategoryId;
                    post.PostCategory = _repo.CreateRepo<Category, int>().GetById(input.CategoryId).Name;
                    var result = _repo.CreateRepo<AuthorPost, int>().Update(id.Value, post);
                    if (result == 1)
                    {
                        await _repo.CompleteAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(input);
        }
        public IActionResult Delete(int? id)
        {
            var image = _repo.CreateRepo<AuthorPost, int>().GetById(id.Value).PostImageUrl;
            if (image != null)
            {
                FileSettings.DeleteFile(image, "PostImages");
            }
            _repo.CreateRepo<AuthorPost, int>().Delete(id.Value);
            return RedirectToAction("Index");
        }
    }
}
