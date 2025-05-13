using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10340607_CyberSecurity_ChatBot
{
    public abstract class Chatbot// creation of the abstract class 
    {
        public abstract void cybersecurityQuestions();// abstract method that is overidden
        public virtual void userInterest(String topic) { } // used for the user interest for the method in the other class Cybersecuritychatbot  
        public virtual string detectEmotion(string input) => "neutral"; // method used in the Cybersecuritychatbot class to handle the emotion
    }
}

