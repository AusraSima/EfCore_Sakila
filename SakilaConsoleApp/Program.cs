namespace SakilaConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ApplicationUI app = new();
            await app.RunAsync();



            //var dbcf = new SakilaDbContextFactory();
            //var context = dbcf.CreateDbContext([]);
            //var actors = await context.Actors.Include(a => a.Films).ToListAsync();
            //foreach (var actor in actors)
            //{
            //    Console.Out.WriteLine(actor.Films.FirstOrDefault().Actors.FirstOrDefault().Films.Count);
            //}

            //var films = await context.Films.Include(film => film.Actors).ToListAsync();
            //foreach (var film in films)
            //{
            //    Console.Out.WriteLine(film);
            //    foreach (var actor in film.Actors)
            //    {
            //        Console.Out.WriteLine("--" + actor);
            //    }
            //}
        }
    }
}
