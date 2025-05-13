using System;
using System.Media;


namespace ST10340607_CyberSecurity_ChatBot
{
    
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.Title = "Cybersecurity Awareness Chatbot";
                try
                {
                    if (OperatingSystem.IsWindows())
                    {
                        String audioFilePath = @"C:\Users\lab_services_student\Desktop\PROG6221-POE\1744782720344369660p9a3uycs-voicemaker.in-speech.wav";
                        SoundPlayer player = new SoundPlayer(audioFilePath);//creating a soundplayer object
                        player.Play(); // playing the audio file
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("There was an error with the audio and it did not play");
                }
                Console.WriteLine(@" 
                                 .--------------------------------------------------------------------.
                                 |  ____      _               ____                       _ _          |
                                 | / ___|   _| |__   ___ _ __/ ___|  ___  ___ _   _ _ __(_) |_ _   _  |
                                 || |  | | | | '_ \ / _ \ '__\___ \ / _ \/ __| | | | '__| | __| | | | |
                                 || |__| |_| | |_) |  __/ |   ___) |  __/ (__| |_| | |  | | |_| |_| | | 
                                 | \____\__, |_.__/ \___|_|  |____/ \___|\___|\__,_|_|  |_|\__|\__, | |
                                 |      |___/   ____ _           _   ____        _             |___/  |
                                 |             / ___| |__   __ _| |_| __ )  ___ | |_                  |
                                 |            | |   | '_ \ / _` | __|  _ \ / _ \| __|                 |
                                 |            | |___| | | | (_| | |_| |_) | (_) | |_                  |
                                 |             \____|_| |_|\__,_|\__|____/ \___/ \__|                 |
                                 '--------------------------------------------------------------------'");// ascii art to display the logo of the chatbot.// refrencve the art! 
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("What is you name:"); //asking the user for their name
                Console.ResetColor();
                String UsersName = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(UsersName))// loop to see if the users name is valid and is not null and dosunt have white space.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I did not quite catch that please can you re-enter you name again");
                    UsersName = Console.ReadLine();
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome " + UsersName + " its so nice to meet you.");
                Console.ResetColor();
                Console.WriteLine();

                Cybersecuritychatbot chatbot = new Cybersecuritychatbot();

                chatbot.cybersecurityQuestions();



            }
        }

    }


