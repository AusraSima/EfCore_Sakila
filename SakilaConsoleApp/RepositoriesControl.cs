﻿using DataAccess;
using DataAccess.Repositories;

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
