using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ST10340607_CyberSecurity_ChatBot
{
    public class Cybersecuritychatbot : Chatbot   // Inheritence from the Chatbot class to override the cybersecurityQuestions() method. 
    {
        private bool chatbotRun = true;        // variable for the while loop to assign the variable to true so loop runs. 
        public override void cybersecurityQuestions()
        {

            while (chatbotRun)
            {
                Console.ForegroundColor = ConsoleColor.Green;            // changing the colour of the font. 
                TypeWriterEffect("I am here to help with your Cybersecurity needs, Please ask me a question.");  // prompting the user to ask a question 
                Console.ResetColor();  // method called to change back to the original colour 
                Console.WriteLine();

                String userQuestion = Console.ReadLine().ToLower(); // String maniplualtion changing the question the user asks to lower case.

                if (string.IsNullOrWhiteSpace(userQuestion))// checking if user has inputted a question otherwise reprompts the user 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please ask me a cybersecurity question, Im here to help");
                    Console.ResetColor();
                    continue;
                }
                ProcessInput(userQuestion);
            }
        }
        private void ProcessInput(String userInput)
        {
            if (userInput.Contains("how are you"))// checking if the string has how are you, it then makes it true if it does if it dosunt then 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect("I am very well thanks.");
                Console.WriteLine();

            }
            else if (userInput.Contains("whats your purpose"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect("My purpose is to answer your questions that regard to cybersecurity saftey and to keep you safe online ");
                Console.WriteLine();

            }
            else if (userInput.Contains("phishing"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect("Phishing is a scam usually over emails or messages where an attacker manipluates " +
                    "their target to gain senstive information");
                Console.WriteLine();
            }
            else if (userInput.Contains("victim of ") || userInput.Contains("phished"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect("Change your passwords\n"
                    + "Report the attack\n"
                    + "Scan your device for malware");
                Console.WriteLine();

            }
            else if (userInput.Contains("password"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect(" Its is important to always follow safe password practices like\n " +
                    "1. Always ensuring you use strong unique passwords that includes upper and lower cases,numbers and special symbols  \n" +
                    "2. You dont reuse any of your passwords\n" +
                    "3.Always enable two factor authenticationn to ensure there is no unauthorised access");
                Console.WriteLine();
            }
            else if (userInput.Contains("safe browsing"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect("Safe browsing  is the act of being safe while using the internet to avoid any potential scams or malware");
                Console.WriteLine();
            }
            else if (userInput.Contains("suspicious link"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWriterEffect(" Great Question,\n" +
                    "1.Have a look at the url, there will be misspellings\n" +
                    "2.There will be very strange domain names for example :  www.go0gle.com \n" +
                    "3.There is no HTTPS and is only hhtp://, these sites arent secure\n" +
                    "4.There could be a sense of urgency for example ACT NOW! ");
                Console.WriteLine();

            }
            else if (userInput.Contains("quit"))// looking for the keyword in the sentence the user types 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                chatbotRun = false;
                TypeWriterEffect("Goodbye, Have a good day");
                Console.WriteLine();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // this else guides the user what to input if they provide the wrong input. 
                TypeWriterEffect("I dont undertand what you mean, Try asking about: Password safety, Phishing,Safe browsing and suspicious links ");
                Console.WriteLine();

            }
            Console.ResetColor();// this line is used to change the colour back to white. 
        }
        private void TypeWriterEffect(string message)// this method is used to make the chatbot feel like its actually typing, by pauing at certain intervals. 
        {
            Random random = new Random();
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(random.Next(20, 70));
                if (c == ' ')
                {
                    Thread.Sleep(random.Next(60, 120));
                }
            }
            Console.WriteLine();
        }
    }

}
