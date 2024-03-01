using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakilaConsoleApp.Handlers
{
    public class ActorsHandler : RepositoriesControl
    {
        private ActorRepository _actorRepository;
        public ActorsHandler() : base()
        {
            init();
        }
        private async Task init()
        {
            _actorRepository = await actorsRepositoryInit();
        }
        public async Task HandleAsync()
        {
            await Console.Out.WriteLineAsync("Welcome to Actors page!");
            bool continueOrNot = true;
            while (continueOrNot)
            {
                ActorsPageMenu();
                Console.Out.WriteLine("Select from menu: ");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        PrintActors(await GetActors());
                        break;
                    case 2:
                        PrintOneActor(await GetOneActor());
                        break;
                    case 3:
                        await CreateActor();
                        break;
                    case 4:
                        await UpdateActor();
                        break;
                    case 5:
                        await DeleteActor();
                        break;
                    case 6:
                        goto Exit;
                        break;
                }
                continueOrNot = continueInActorsPage();
            }
        Exit:;
        }

        public static void ActorsPageMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Get list of actors");
            Console.WriteLine("2 - Get info about the actor");
            Console.WriteLine("3 - Enter a new actor");
            Console.WriteLine("4 - Update actor's info");
            Console.WriteLine("5 - Delete actor");
            Console.WriteLine("6 - Exit Actors page");
            Console.WriteLine();
        }

        public async Task<List<Actor>> GetActors()
        {
            return await _actorRepository.ReadAllAsync();
        }
        public void PrintActors(List<Actor> actors)
        {

            foreach (var actor in actors)
            {
                Console.WriteLine($"{actor.Id,-4}   {actor.FirstName,-10}   {actor.LastName,-15}   ");
            }
            Console.WriteLine();
        }
        public async Task<Actor> GetOneActor()
        {
            await Console.Out.WriteLineAsync("Enter ID of the actor you want to review: ");
            short id = Convert.ToInt16(Console.ReadLine());

            Actor actor = await _actorRepository.ReadAsync(id);
            if (actor == null)
            {
                throw new KeyNotFoundException($"An actor with ID {id} not found.");
            }

            return await _actorRepository.ReadAsync(actor.Id);
        }
        public void PrintOneActor(Actor actor)
        {
            Console.WriteLine($"The actor ID {actor.Id}, {actor.FirstName} {actor.LastName} starred in {actor.Films.Count} film(s):");
            foreach (var film in actor.Films)
            {
                Console.WriteLine($"***{film.Title}");
            }
            Console.WriteLine();
        }
        public async Task CreateActor()
        {
            await Console.Out.WriteLineAsync("Enter actor's first name: ");
            string firstName = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter actor's last name: ");
            string lastName = Console.ReadLine();

            var actor = new Actor()
            {
                FirstName = firstName,
                LastName = lastName
            };
            await _actorRepository.CreateAsync(actor);
            Console.Out.WriteLine($"New actor {actor.FirstName} {actor.LastName} created successfully.");
            Console.WriteLine();
        }
        private async Task UpdateActor()
        {
            await Console.Out.WriteLineAsync("Enter ID of the actor you want to update: ");
            short id = Convert.ToInt16(Console.ReadLine());

            Actor actor = await _actorRepository.ReadAsync(id);
            if (actor == null)
            {
                Console.WriteLine($"An actor with ID {id} not found.");
                return;
            }
            bool isActorEdited = true;
            while (isActorEdited)
            {
                await Console.Out.WriteLineAsync("What do you want to update? Select from the list: ");
                UpdateActorMenu();
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Console.Out.WriteLineAsync("Enter updated actor's first name: ");
                        string firstName = Console.ReadLine();
                        actor.FirstName = firstName;
                        break;
                    case 2:
                        await Console.Out.WriteLineAsync("Enter updated actor's last name: ");
                        string lastName = Console.ReadLine();
                        actor.LastName = lastName;
                        break;
                    case 3:
                        isActorEdited = false;
                        break;
                }
            }
            await _actorRepository.UpdateAsync(actor);
            await Console.Out.WriteLineAsync($"Data of the actor {actor.Id}, {actor.FirstName} {actor.LastName} updated successfully.");
            Console.WriteLine();
        }
        public static void UpdateActorMenu()
        {
            Console.WriteLine("Select what should be updated: ");
            Console.WriteLine(" 1 - Update actor's first name");
            Console.WriteLine(" 2 - Update actor's last surname");
            Console.WriteLine(" 3 - Exit update");
            Console.WriteLine();
        }
        private async Task DeleteActor()
        {
            await Console.Out.WriteLineAsync("Enter ID of the actor you want to delete: ");
            short id = Convert.ToInt16(Console.ReadLine());
            Actor actor = await _actorRepository.ReadAsync(id);
            if (actor == null)
            {
                Console.WriteLine($"An actor with ID {id} not found.");
                return;
            }
            await _actorRepository.DeleteAsync(actor);
            await Console.Out.WriteLineAsync($"The actor {actor.Id}, {actor.FirstName} {actor.LastName} deleted successfully.");
            Console.WriteLine();
        }

        public bool continueInActorsPage()
        {
            string continueKey = "y";
            while (true)
            {
                Console.Out.WriteLine("Do you want to continue work in Actors page?(y/n)");
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
