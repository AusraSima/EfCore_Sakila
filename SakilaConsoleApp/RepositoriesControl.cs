using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakilaConsoleApp
{
    public class RepositoriesControl
    {
        private readonly SakilaDbContext _context;
        public RepositoriesControl()
        {
            SakilaDbContextFactory dbcf = new SakilaDbContextFactory();
            _context = dbcf.CreateDbContext([]);
        }
        public async Task<ActorRepository> actorsRepositoryInit()
        {
            return new ActorRepository(_context);
        }
        public async Task<FilmRepository> filmsRepositoryInit()
        {
            return new FilmRepository(_context);
        }
    }
}
