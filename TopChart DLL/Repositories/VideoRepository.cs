using Microsoft.EntityFrameworkCore;
using TopChart_DLL.EF;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_DLL.Repositories
{
    public class VideoRepository: IRepositoryVideo
    {
        private readonly TopChartDbMLAContext _context;

        public VideoRepository(TopChartDbMLAContext context)
        {
            _context = context;
        }
        public async Task<List<Video>> GetVideoList()
        {
            var tracksContext = _context.Video.Include(p => p.Singer).Include(p => p.Genre);
            return await tracksContext.ToListAsync();
        }

        public IQueryable<Video> GetSearchList(string name)
        {
            return _context.Video.Include(p => p.Singer).Include(p => p.Genre).Where(a => a.Name == name);
        }
        public async Task Create(Video t)
        {
            await _context.Video.AddAsync(t);
        }
        public async Task Delete(int id)
        {
            Video? v = await _context.Video.FindAsync(id);
            if (v != null)
                _context.Video.Remove(v);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Video GetTrack(int? id)
        {
            var tracksContext = _context.Video.Include(p => p.Singer).Include(p => p.Genre);
            return tracksContext.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Video v)
        {
            _context.Entry(v).State = EntityState.Modified;
        }

        public async Task<List<Video>> GetSortedTracksList()
        {
            var tracks = _context.Video.Include(p => p.Singer).ToList();
            for (int i = 0; i < tracks.Count; i++)
            {
                for (int j = 0; j < tracks.Count - 1 - i; j++)
                {
                    if (tracks[j].Like < tracks[j + 1].Like)
                    {
                        Video tmp = tracks[j];
                        tracks[j] = tracks[j + 1];
                        tracks[j + 1] = tmp;
                    }
                }
            }
            return tracks;
        }
    }
}
