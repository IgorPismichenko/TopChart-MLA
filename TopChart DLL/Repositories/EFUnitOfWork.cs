using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_DLL.EF;
using TopChart_DLL.Interfaces;

namespace TopChart_DLL.Repositories
{
    public class EFUnitOfWork:IUnitOfWork
    {
        private TopChartDbMLAContext db;
        private CommentsRepository commentsRepository;
        private CommentsVideoRepository commentsVideoRepository;
        private GenreRepository genreRepository;
        private SingersRepository singersRepository;
        private TracksRepository tracksRepository;
        private UsersRepository usersRepository;
        private VideoRepository videoRepository;

        public EFUnitOfWork(TopChartDbMLAContext context)
        {
            db = context;
        }
        public IRepositoryComments Comments
        {
            get
            {
                if (commentsRepository == null)
                    commentsRepository = new CommentsRepository(db);
                return commentsRepository;
            }
        }
        public IRepositoryCommentsVideo CommentsVideo
        {
            get
            {
                if (commentsVideoRepository == null)
                    commentsVideoRepository = new CommentsVideoRepository(db);
                return commentsVideoRepository;
            }
        }
        public IRepositoryGenres Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }
        public IRepositorySingers Singers
        {
            get
            {
                if (singersRepository == null)
                    singersRepository = new SingersRepository(db);
                return singersRepository;
            }
        }
        public IRepositoryTracks Tracks
        {
            get
            {
                if (tracksRepository == null)
                    tracksRepository = new TracksRepository(db);
                return tracksRepository;
            }
        }
        public IRepositoryUsers Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UsersRepository(db);
                return usersRepository;
            }
        }
        public IRepositoryVideo Videos
        {
            get
            {
                if (videoRepository == null)
                    videoRepository = new VideoRepository(db);
                return videoRepository;
            }
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
