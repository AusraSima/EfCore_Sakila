using DataAccess.Entities;
using DataAccess.Repositories;

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
                    default:
                        await Console.Out.WriteLineAsync("Invalid option, please select a number from the menu.");
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
            string title = IfTextLengthLessThan256(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film description: ");
            string desctription = IfTextLengthLessThan256(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film release year: ");
            short releaseYear = VerifyShortInput(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film language ID: ");
            int languageID = Convert.ToInt16(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film original language ID: ");
            int origLanguageID = Convert.ToInt16(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film rental duration: ");
            byte rentalDuration = (byte)Convert.ToInt16(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film rental rate: ");
            decimal rentalRate = VerifyDecimalInput(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film length: ");
            short length = VerifyShortInput(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film replacement cost: ");
            decimal replacementCost = VerifyDecimalInput(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film rating, select from the following: G, PG, PG-13, R, NC-17");
            string rating = ValidateRating(Console.ReadLine());
            await Console.Out.WriteLineAsync("Enter film special features: ");
            string specialFeatures = IfTextLengthLessThan256(Console.ReadLine());

            var film = new Film()
            {
                Title = title,
                Description = desctription,
                ReleaseYear = releaseYear,
                LanguageId = languageID,
                OriginalLanguageId = origLanguageID,
                RentalDuration = rentalDuration,
                RentalRate = rentalRate,
                Length = length,
                ReplacementCost = replacementCost,
                Rating = rating,
                SpecialFeatures = specialFeatures,
                LastUpdate = DateTime.Now
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
                        string title = IfTextLengthLessThan256(Console.ReadLine());
                        film.Title = title;
                        break;
                    case 2:
                        await Console.Out.WriteLineAsync("Enter updated film description: ");
                        string desctription = IfTextLengthLessThan256(Console.ReadLine());
                        film.Description = desctription;
                        break;
                    case 3:
                        await Console.Out.WriteLineAsync("Enter updated film release year: ");
                        short releaseYear = VerifyShortInput(Console.ReadLine());
                        film.ReleaseYear = releaseYear;
                        break;
                    case 4:
                        await Console.Out.WriteLineAsync("Enter updated film language ID: ");
                        int languageID = Convert.ToInt16(Console.ReadLine());
                        film.LanguageId = languageID;
                        break;
                    case 5:
                        await Console.Out.WriteLineAsync("Enter updated film original language ID: ");
                        int origLanguageID = Convert.ToInt16(Console.ReadLine());
                        film.OriginalLanguageId = origLanguageID;
                        break;
                    case 6:
                        await Console.Out.WriteLineAsync("Enter film rental duration: ");
                        byte rentalDuration = (byte)Convert.ToInt16(Console.ReadLine());
                        film.RentalDuration = rentalDuration;
                        break;
                    case 7:
                        await Console.Out.WriteLineAsync("Enter film rental rate: ");
                        decimal rentalRate = VerifyDecimalInput(Console.ReadLine());
                        film.RentalRate = rentalRate;
                        break;
                    case 8:
                        await Console.Out.WriteLineAsync("Enter film length: ");
                        short length = VerifyShortInput(Console.ReadLine());
                        film.Length = length;
                        break;
                    case 9:
                        await Console.Out.WriteLineAsync("Enter film replacement cost: ");
                        decimal replacementCost = VerifyDecimalInput(Console.ReadLine());
                        film.ReplacementCost = replacementCost;
                        break;
                    case 10:
                        await Console.Out.WriteLineAsync("Enter film rating, select from the following: G, PG, PG-13, R, NC-17");
                        string rating = ValidateRating(Console.ReadLine());
                        film.Rating = rating;
                        break;
                    case 11:
                        await Console.Out.WriteLineAsync("Enter film special features: ");
                        string specialFeatures = IfTextLengthLessThan256(Console.ReadLine());
                        film.SpecialFeatures = specialFeatures;
                        break;
                    case 12:
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
            Console.WriteLine(" 2 - Update film's description");
            Console.WriteLine(" 3 - Update film's release year");
            Console.WriteLine(" 4 - Update film's language ID");
            Console.WriteLine(" 5 - Update film's original language ID");
            Console.WriteLine(" 6 - Update film's rental duration");
            Console.WriteLine(" 7 - Update film's rental rate");
            Console.WriteLine(" 8 - Update film's length");
            Console.WriteLine(" 9 - Update film's replacement cost");
            Console.WriteLine("10 - Update film's rating");
            Console.WriteLine("11 - Update film's special features");
            Console.WriteLine("12 - Exit update");
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
        private string IfTextLengthLessThan256(string input)
        {
            if (input.Length <= 255)
            {
                return input;
            }
            else
            {
                Console.WriteLine("Your text length exceeds 255 characters. Please enter a shorter text.");
                return IfTextLengthLessThan256(Console.ReadLine());
            }
        }
        private string ValidateRating(string input)
        {
            string[] validRatings = { "G", "PG", "PG-13", "R", "NC-17" };
            if (validRatings.Contains(input.ToUpper()))
            {
                return input.ToUpper();
            }
            else
            {
                Console.WriteLine("Invalid rating. Please select from the following options: G, PG, PG-13, R, NC-17");
                return ValidateRating(Console.ReadLine());
            }
        }
        private decimal VerifyDecimalInput(string input)
        {
            decimal rentalRate;
            if (decimal.TryParse(input, out rentalRate))
            {
                return rentalRate;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                return VerifyDecimalInput(Console.ReadLine());
            }
        }
        private short VerifyShortInput(string input)
        {
            short releaseYear;
            if (short.TryParse(input, out releaseYear))
            {
                return releaseYear;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid short integer.");
                return VerifyShortInput(Console.ReadLine());
            }
        }
    }
}
