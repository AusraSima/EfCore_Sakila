using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using SakilaConsoleApp.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakilaConsoleApp
{
    public class ApplicationUI
    {
        private ActorsHandler _actorsHandler;
        private FilmsHandler _filmsHandler;
        public ApplicationUI()
        {
            init();
        }
        public async Task init()
        {
            _actorsHandler = new ActorsHandler();
            _filmsHandler = new FilmsHandler();
        }
        public async Task RunAsync()
        {
            await Console.Out.WriteLineAsync("Main menu");
            while (true)
            {
                await Console.Out.WriteLineAsync("Select from the menu: ");
                MainMenu();
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await _actorsHandler.HandleAsync();
                        break;
                    case 2:
                        await _filmsHandler.HandleAsync();
                        break;
                    case 3:
                        Environment.Exit(1);
                        break;
                }
            }

        }
        public void MainMenu()
        {
            Console.WriteLine("1 - Go to Actors page");
            Console.WriteLine("2 - Go to Films page");
            Console.WriteLine("3 - Exit program");
            Console.WriteLine();
        }


    }
}
