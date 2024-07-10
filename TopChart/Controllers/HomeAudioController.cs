using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopChart.Filters;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;
using TopChart_DLL.Entities;

namespace TopChart.Controllers
{
    [Culture]
    public class HomeAudioController : Controller
    {
        ITracksService repo;
        IGenresService repoGen;
        ISingersService repoSing;
        IUsersService repoUsers;
        ICommentsService repoComm;
        IMessagesService repoMess;
        IWebHostEnvironment _appEnvironment;
        public HomeAudioController(ITracksService r, IGenresService g, ISingersService s, IUsersService u, ICommentsService c, IMessagesService m, IWebHostEnvironment appEnvironment)
        {
            repo = r;
            repoGen = g;
            repoSing = s;
            repoUsers = u;
            repoComm = c;
            repoMess = m;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Audio()
        {
            HttpContext.Session.SetString("path", Request.Path);
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetTracksList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        TracksDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            UsersDTO users = new UsersDTO();
            ViewData["Users"] = users;
            var model = await repo.GetTracksList();
            return View(model);
        }

        public async Task<IActionResult> EditUser()
        {
            HttpContext.Session.SetString("path", Request.Path);
            var model = await repoUsers.GetUsersList();
            return View(model);
        }

        public IActionResult Create()
        {
            HttpContext.Session.SetString("path", Request.Path);
            ViewData["SingerId"] = new SelectList(repoSing.GetValues(), "Id", "Name");
            ViewData["GenreId"] = new SelectList(repoGen.GetValues(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SingerId,Album,GenreId,Path")] TracksDTO track, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (!uploadedFile.FileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Path", "Only mp3 files are allowed.");
                    return View(track);
                }
                if (uploadedFile != null)
                {
                    string path = "/Tracks/" + uploadedFile.FileName;
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
                return RedirectToAction(nameof(Audio));
            }
            return View(track);
        }

        public IActionResult CreateGenre()
        {
            HttpContext.Session.SetString("path", Request.Path);
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
                return RedirectToAction(nameof(Audio));
            }
            return View(genre);
        }

        public IActionResult CreateSinger()
        {
            HttpContext.Session.SetString("path", Request.Path);
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
                return RedirectToAction(nameof(Audio));
            }
            return View(singer);
        }

        public IActionResult Delete(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }

            var user = repoUsers.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             await repoUsers.Delete(id);

            await repoUsers.Save();
            return RedirectToAction(nameof(EditUser));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }

            var user = repoUsers.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password,Salt,Status")] UsersDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repoUsers.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!repoUsers.UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EditUser));
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.SetString("path", Request.Path);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            HttpContext.Session.SetString("path", Request.Path);
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetTracksList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        TracksDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            var tracks = repo.GetSearchList(search);
            if (tracks.Count() == 0)
            {
                return View("Audio", tracks);
            }
            return View("Audio", tracks);
        }

        public async Task<IActionResult> Details(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("Id", (int)id);
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetTracksList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        TracksDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            ViewData["Comments"] = await repoComm.GetCommentList();
            ViewData["Users"] = await repoUsers.GetUsersList();
            var track =await repo.GetTrack(id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        [HttpPost]
        public async Task<IActionResult> Comment([Bind("Id")] CommentDTO comm, string comment)
        {
            HttpContext.Session.SetString("path", Request.Path);
            comm.Message = comment;
            comm.Date = DateTime.Now.ToString();
            string? login = HttpContext.Session.GetString("Login");
            var users = await repoUsers.GetUsersList();
            foreach (var user in users)
            {
                if(user.Login == login)
                {
                    comm.UserId = user.Id;
                }
            }
            var id = HttpContext.Session.GetInt32("Id");
            var track = await repo.GetTrack(id);
            comm.TrackId = track.Id;
            await repoComm.Create(comm);
            await repoComm.Save();
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetTracksList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        TracksDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            ViewData["Comments"] = await repoComm.GetCommentList();
            ViewData["Users"] = await repoUsers.GetUsersList();
            HttpContext.Session.SetInt32("Id", (int)id);
            return View("Details", track);
        }

        public async Task<IActionResult> Like(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }
            var track = await repo.GetTrack(id);
            if (track == null)
            {
                return NotFound();
            }
            track.Like += 1;
            await repo.Update(track);
            await repo.Save();
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetTracksList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        TracksDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            UsersDTO users = new UsersDTO();
            ViewData["Users"] = users;
            var model = await repo.GetTracksList();
            return View("Audio", model);
        }

        public async Task<IActionResult> Top()
        {
            HttpContext.Session.SetString("path", Request.Path);
            ViewData["Genre"] = await repoGen.GetGenresList();
            var tmp = await repo.GetTracksList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < tmp.Count - 1 - i; j++)
                {
                    if (tmp[j].Like < tmp[j + 1].Like)
                    {
                        TracksDTO temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            }
            ViewData["TopSingers"] = tmp;
            var model = await repo.GetTracksList();
            for (int i = 0; i < model.Count; i++)
            {
                for (int j = 0; j < model.Count - 1 - i; j++)
                {
                    if (model[j].Like < model[j + 1].Like)
                    {
                        TracksDTO temp = model[j];
                        model[j] = model[j + 1];
                        model[j + 1] = temp;
                    }
                }
            }
            return View("Audio", model);
        }
        public async Task<IActionResult> Chatting()
        {
            HttpContext.Session.SetString("path", Request.Path);
            var model = await repoMess.GetMessagesList();
            return View("Chat", model);
        }

        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/HomeAudio/Index";
            List<string> cultures = new List<string>() { "en", "uk", "it" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Append("lang", lang, option);
            return Redirect(returnUrl);
        }
    }
}
