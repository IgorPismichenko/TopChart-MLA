using Microsoft.AspNetCore.Mvc;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;

namespace TopChart.Controllers
{
    public class HomeController : Controller
    {
        ITracksService repo;
        IVideoService repoV;
        IGenresService repoGen;
        ISingersService repoSing;

        public HomeController(ITracksService r, IVideoService rV, IGenresService rG, ISingersService rS)
        {
            repo = r;
            repoV = rV;
            repoGen = rG;
            repoSing = rS;
        }
        public async Task<IActionResult> Index()
        {
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
            return View(model);
        }
        public async Task<IActionResult> IndexVideo()
        {
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
            var model = await repoV.GetVideoList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
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
                return View("Index", tracks);
            }
            return View("Index", tracks);
        }

        [HttpPost]
        public async Task<IActionResult> SearchVideo(string search)
        {
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
            var clips = repoV.GetSearchList(search);
            if (clips.Count() == 0)
            {
                return View("IndexVideo", clips);
            }
            return View("IndexVideo", clips);
        }

        public async Task<IActionResult> Top()
        {
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
            return View("Index", model);
        }

        public async Task<IActionResult> TopVideo()
        {
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
            var model = await repoV.GetVideoList();
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
            return View("IndexVideo", model);
        }
    }
}
