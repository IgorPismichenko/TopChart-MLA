using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;

namespace TopChart.Controllers
{
    public class HomeVideoController : Controller
    {
        IVideoService repo;
        IGenresService repoGen;
        ISingersService repoSing;
        IUsersService repoUsers;
        ICommentsVideoService repoComm;
        IWebHostEnvironment _appEnvironment;
        public HomeVideoController(IVideoService v, IGenresService g, ISingersService s, IUsersService u, ICommentsVideoService c, IWebHostEnvironment appEnvironment)
        {
            repo = v;
            repoGen = g;
            repoSing = s;
            repoUsers = u;
            repoComm = c;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Video()
        {
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetVideoList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        VideoDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            UsersDTO users = new UsersDTO();
            ViewData["Users"] = users;
            var model = await repo.GetVideoList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["SingerId"] = new SelectList(repoSing.GetValues(), "Id", "Name");
            ViewData["GenreId"] = new SelectList(repoGen.GetValues(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SingerId,Album,GenreId,Path")] VideoDTO track, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (!uploadedFile.FileName.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Path", "Only mp4 files are allowed.");
                    return View(track);
                }
                if (uploadedFile != null)
                {
                    string path = "/Video/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    track.Path = path;
                    track.Size = uploadedFile.Length.ToString();
                }
                track.Date = DateTime.Now.ToString();
                track.Like = 0;
                await repo.Create(track);
                await repo.Save();
                return RedirectToAction(nameof(Video));
            }
            return View(track);
        }

        public IActionResult CreateGenre()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,Name")] GenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                await repoGen.Create(genre);
                await repoGen.Save();
                return RedirectToAction(nameof(Video));
            }
            return View(genre);
        }

        public IActionResult CreateSinger()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSinger([Bind("Id,Name")] SingerDTO singer, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/Posters/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    singer.Path = path;
                }
                await repoSing.Create(singer);
                await repoSing.Save();
                return RedirectToAction(nameof(Video));
            }
            return View(singer);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SearchVideo(string search)
        {
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetVideoList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        VideoDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            var clips = repo.GetSearchList(search);
            if (clips.Count() == 0)
            {
                return View("Video", clips);
            }
            return View("Video", clips);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("Id", (int)id);
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetVideoList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        VideoDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            ViewData["CommentsVideo"] = await repoComm.GetCommentList();
            ViewData["Users"] = await repoUsers.GetUsersList();
            var clip = await repo.GetTrack(id);
            if (clip == null)
            {
                return NotFound();
            }

            return View(clip);
        }

        [HttpPost]
        public async Task<IActionResult> CommentVideo([Bind("Id")] CommentVideoDTO comm, string comment)
        {
            comm.Message = comment;
            comm.Date = DateTime.Now.ToString();
            string? login = HttpContext.Session.GetString("Login");
            var users = await repoUsers.GetUsersList();
            foreach (var user in users)
            {
                if (user.Login == login)
                {
                    comm.UserId = user.Id;
                }
            }
            var id = HttpContext.Session.GetInt32("Id");
            var clip = await repo.GetTrack(id);
            comm.VideoId = clip.Id;
            await repoComm.Create(comm);
            await repoComm.Save();
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetVideoList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        VideoDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            ViewData["CommentsVideo"] = await repoComm.GetCommentList();
            ViewData["Users"] = await repoUsers.GetUsersList();
            HttpContext.Session.SetInt32("Id", (int)id);
            return View("Details", clip);
        }

        public async Task<IActionResult> LikeVideo(int? id)
        {
            VideoDTO track = await repo.GetTrack(id);
            track.Like += 1;
            await repo.Update(track);
            await repo.Save();
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetVideoList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        VideoDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            UsersDTO users = new UsersDTO();
            ViewData["Users"] = users;
            var model = await repo.GetVideoList();
            return View("Video", model);
        }

        public async Task<IActionResult> TopVideo()
        {
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetVideoList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        VideoDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            var model = await repo.GetVideoList();
            for (int i = 0; i < model.Count; i++)
            {
                for (int j = 0; j < model.Count - 1 - i; j++)
                {
                    if (model[j].Like < model[j + 1].Like)
                    {
                        VideoDTO temp = model[j];
                        model[j] = model[j + 1];
                        model[j + 1] = temp;
                    }
                }
            }
            return View("Video", model);
        }
    }
}
