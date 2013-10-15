using System;

namespace RaceGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RaceGame game = new RaceGame())
            {
                game.Run();
            }
        }
    }
#endif
}

