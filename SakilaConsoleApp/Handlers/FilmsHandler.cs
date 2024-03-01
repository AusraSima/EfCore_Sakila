using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakilaConsoleApp.Handlers
{
    public class FilmsHandler : RepositoriesControl
    {
        private FilmRepository _filmRepository;
        public FilmsHandler() : base()
        {
            init();
        }
        private async Task init()
        {
            _filmRepository = await filmsRepositoryInit();
        }
        public async Task HandleAsync()
        {
            await Console.Out.WriteLineAsync("Welcome to Films page!");
            bool continueOrNot = true;
            while (continueOrNot)
            {
                FilmsPageMenu();
                Console.Out.WriteLine("Select from menu: ");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        PrintFilms(await GetFilms());
                        break;
                    case 2:
                        PrintOneFilm(await GetOneFilm());
                        break;
                    case 3:
                        await CreateFilm();
                        break;
                    case 4:
                        await UpdateFilm();
                        break;
                    case 5:
                        await DeleteFilm();
                        break;
                    case 6:
                        goto Exit;
                        break;
                }
                continueOrNot = continueInFilmsPage();
            }
        Exit:;
        }
        public static void FilmsPageMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Get list of films");
            Console.WriteLine("2 - Get info about the film");
            Console.WriteLine("3 - Enter a new film");
            Console.WriteLine("4 - Update film's info");
            Console.WriteLine("5 - Delete film");
            Console.WriteLine("6 - Exit Films page");
            Console.WriteLine();
        }

        public async Task<List<Film>> GetFilms()
        {
            return await _filmRepository.ReadAllAsync();
        }
        public void PrintFilms(List<Film> films)
        {

            foreach (var film in films)
            {
                Console.WriteLine($"{film.Id,-4}   {film.Title,-10}");
            }
            Console.WriteLine();
        }
        public async Task<Film> GetOneFilm()
        {
            await Console.Out.WriteLineAsync("Enter ID of the film you want to review: ");
            short id = Convert.ToInt16(Console.ReadLine());

            Film film = await _filmRepository.ReadAsync(id);
            if (film == null)
            {
                throw new KeyNotFoundException($"A film with ID {id} not found.");
            }

            return await _filmRepository.ReadAsync(film.Id);
        }
        public void PrintOneFilm(Film film)
        {
            Console.WriteLine($"In the film ID {film.Id}, {film.Title}, following actors are starring:");
            foreach (var actor in film.Actors)
            {
                Console.WriteLine($"***{actor.FirstName} {actor.LastName}");
            }
            Console.WriteLine();
        }
        public async Task CreateFilm()
        {
            await Console.Out.WriteLineAsync("Enter film's title: ");
            string title = Console.ReadLine();
            //await Console.Out.WriteLineAsync("Enter actor's last name: ");
            //string lastName = Console.ReadLine();

            var film = new Film()
            {
                Title = title
                //LastName = lastName
            };
            await _filmRepository.CreateAsync(film);
            Console.Out.WriteLine($"New film {film.Title} created successfully.");
            Console.WriteLine();
        }
        private async Task UpdateFilm()
        {
            await Console.Out.WriteLineAsync("Enter ID of the film you want to update: ");
            short id = Convert.ToInt16(Console.ReadLine());

            Film film = await _filmRepository.ReadAsync(id);
            if (film == null)
            {
                Console.WriteLine($"A film with ID {id} not found.");
                return;
            }
            bool isFilmEdited = true;
            while (isFilmEdited)
            {
                await Console.Out.WriteLineAsync("What do you want to update? Select from the list: ");
                UpdateFilmMenu();
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Console.Out.WriteLineAsync("Enter updated film's title: ");
                        string title = Console.ReadLine();
                        film.Title = title;
                        break;
                    case 2:
                        //await Console.Out.WriteLineAsync("Enter updated actor's last name: ");
                        //string lastName = Console.ReadLine();
                        //actor.LastName = lastName;
                        break;
                    case 3:
                        isFilmEdited = false;
                        break;
                }
            }
            await _filmRepository.UpdateAsync(film);
            await Console.Out.WriteLineAsync($"Data of the film {film.Id}, {film.Title} updated successfully.");
            Console.WriteLine();
        }
        public static void UpdateFilmMenu()
        {
            Console.WriteLine("Select what should be updated: ");
            Console.WriteLine(" 1 - Update film's title");
            //Console.WriteLine(" 2 - Update actor's last surname");
            Console.WriteLine(" 3 - Exit update");
            Console.WriteLine();
        }
        private async Task DeleteFilm()
        {
            await Console.Out.WriteLineAsync("Enter ID of the film you want to delete: ");
            short id = Convert.ToInt16(Console.ReadLine());
            Film film = await _filmRepository.ReadAsync(id);
            if (film == null)
            {
                Console.WriteLine($"A film with ID {id} not found.");
                return;
            }
            await _filmRepository.DeleteAsync(film);
            await Console.Out.WriteLineAsync($"The film {film.Id}, {film.Title} deleted successfully.");
            Console.WriteLine();
        }

        public bool continueInFilmsPage()
        {
            string continueKey = "y";
            while (true)
            {
                Console.Out.WriteLine("Do you want to continue work in Films page?(y/n)");
                continueKey = Console.ReadLine().ToLower();
                if (continueKey.Equals("y"))
                {
                    return true;
                }

                if (continueKey.Equals("n"))
                {
                    return false;
                }
            }
        }
    }
}
