using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FilmRepository
    {
        private readonly SakilaDbContext _context;
        public FilmRepository(SakilaDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Film>> ReadAllAsync()
        {
            return await _context.Films.Include(film => film.Actors).ToListAsync();
        }
        public async Task<Film> ReadAsync(short id)
        {
            return await _context.Films.Include(film => film.Actors).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task UpdateAsync(Film film)
        {
            var newFilm = await ReadAsync(film.Id);
            _context.Films.Entry(newFilm).CurrentValues.SetValues(film);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Film film)
        {
            var deleteFilm = await ReadAsync(film.Id);
            _context.Films.Remove(deleteFilm);
            await _context.SaveChangesAsync();
        }
    }
}
