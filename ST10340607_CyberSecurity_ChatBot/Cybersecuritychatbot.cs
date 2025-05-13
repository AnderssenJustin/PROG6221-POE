using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ST10340607_CyberSecurity_ChatBot
{
    public class Cybersecuritychatbot : Chatbot
    {
        private bool chatbotRun = true;
        private string usersName;
        private readonly Dictionary<string, string> userMemory = new Dictionary<string, string>();
        private readonly Random _random = new Random();
        private string currentTopic = null;
        

        private readonly Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>() // this is the generic collection for the dictionary for when a tip is asked from the users 
        {
            {"password", new List<string>
            {
                "Use 12+ characters with mixed types and avoid personal info!",
                "Consider a password manager - it's safer than reusing passwords.",
                "Enable two-factor authentication wherever possible.",
                "Regularly update important passwords every 3-6 months.",
                "Use passphrases like 'PurpleTurtle$JumpsHigh' for better security."
            }},
            {"phishing", new List<string>
            {
                "Phishing scams often use urgent language to trick you.",
                "Check sender email addresses carefully - look for misspellings.",
                "Never download attachments from suspicious emails.",
                "Legitimate companies will never ask for passwords via email.",
                "Hover over links to see the actual URL before clicking."
            }},
            {"browsing", new List<string>
            {
                "Always look for HTTPS and padlock icons in your browser.",
                "Use a VPN on public Wi-Fi to protect your data.",
                "Keep your browser updated to patch security vulnerabilities.",
                "Disable auto-fill for sensitive information in browsers.",
                "Clear cookies regularly to prevent tracking."
            }},
            {"malware", new List<string>
            {
                "Keep your operating system and software updated with security patches.",
                "Use a reputable antivirus and anti-malware solution and scan regularly.",
                "Don't download software from untrusted sources.",
                "Be careful with email attachments, even from known senders.",
                "Back up your important data regularly to protect against ransomware."
            }},
            {"wifi", new List<string>
            {
                "Always use WPA3 encryption for your home WiFi if available.",
                "Change default router usernames and passwords immediately.",
                "Hide your network SSID to prevent easy discovery.",
                "Use a guest network for visitors and IoT devices.",
                "Regularly update your router's firmware to patch security vulnerabilities."
            }}
        };

        private readonly List<string> defaultResponses = new List<string>()
        {
            "I'm not sure I understand. Can you try rephrasing?",
            "Could you ask about cybersecurity topics like passwords, phishing, malware, WiFi or safe browsing?",
            "I specialize in cybersecurity - try asking about online safety."
        };

        public delegate void ResponseDelegate(string topic);// delegate to determine the response to the keywords

        private readonly Dictionary<string, ResponseDelegate> keywordResponseHandlers = new Dictionary<string, ResponseDelegate>();

        public Cybersecuritychatbot()
        {
            InitializeKeywordResponseHandlers();
        }

        private void InitializeKeywordResponseHandlers()
        {
            keywordResponseHandlers.Add("password", RespondToPassword);
            keywordResponseHandlers.Add("phishing", RespondToPhishing);
            keywordResponseHandlers.Add("browsing", RespondToBrowsing);
            keywordResponseHandlers.Add("malware", RespondToMalware);
            keywordResponseHandlers.Add("wifi", RespondToWifi);
        }

        private void RespondToPassword(string keyword) // response to when user says they are interested in a ceratin topic 
        {
            string response;

            if (userMemory.ContainsKey("interest") && userMemory["interest"] == "password")
            {
                response = $"{usersName}, since password security matters to you: " +
                          OutputRandomResponses(keyword);

                if (_random.NextDouble() > 0.5)
                {
                    response += "\n" + "Pro tip: Consider using a password manager like Bitwarden or 1Password!";
                }
            }
            else
            {
                response = OutputRandomResponses(keyword);
            }

            TypeWriterEffect(response);
        }

        private void RespondToPhishing(string keyword)// response to when user says they are interested in a ceratin topic 
        {
            string response;

            if (userMemory.ContainsKey("interest") && userMemory["interest"] == "phishing")
            {
                response = $"{usersName}, since phishing concerns you: " +
                          OutputRandomResponses(keyword);

                if (_random.NextDouble() > 0.5)
                {
                    response += "\n" + "Remember: When in doubt, contact the company directly!";
                }
            }
            else
            {
                response = OutputRandomResponses(keyword);
            }

            TypeWriterEffect(response);
        }

        private void RespondToBrowsing(string keyword)// response to when user says they are interested in a ceratin topic 
        {
            string response;

            if (userMemory.ContainsKey("interest") && userMemory["interest"] == "browsing")
            {
                response = $"{usersName}, since safe browsing interests you: " +
                          OutputRandomResponses(keyword);

                if (_random.NextDouble() > 0.5)
                {
                    response += "\n" + "Did you know: Browser extensions like uBlock Origin improve security!";
                }
            }
            else
            {
                response = OutputRandomResponses(keyword);
            }

            TypeWriterEffect(response);
        }

        private void RespondToMalware(string keyword)// response to when user says they are interested in malware 
        {
            string response;

            if (userMemory.ContainsKey("interest") && userMemory["interest"] == "malware")
            {
                response = $"{usersName}, since malware protection concerns you: " +
                          OutputRandomResponses(keyword);

                if (_random.NextDouble() > 0.5)
                {
                    response += "\n" + "Pro tip: Consider using sandboxed environments when testing unknown software!";
                }
            }
            else
            {
                response = OutputRandomResponses(keyword);
            }

            TypeWriterEffect(response);
        }

        private void RespondToWifi(string keyword)// response to when user says they are interested in wifi security
        {
            string response;

            if (userMemory.ContainsKey("interest") && userMemory["interest"] == "wifi")
            {
                response = $"{usersName}, since WiFi security matters to you: " +
                          OutputRandomResponses(keyword);

                if (_random.NextDouble() > 0.5)
                {
                    response += "\n" + "Pro tip: Consider using a VPN even on your home network for extra protection!";
                }
            }
            else
            {
                response = OutputRandomResponses(keyword);
            }

            TypeWriterEffect(response);
        }

        public override void cybersecurityQuestions()// this method is used to handle the propmts from the chatbot for the greeting and question prompts 
        {
            int promptCount = 0;
            var reminderMessages = new List<string>
    {
        "Remember I'm here to help with phishing, passwords, malware, WiFi security, and safe browsing!",
        "Need help with cybersecurity? Ask me about phishing, passwords, malware, WiFi security or safe browsing!",
        "Just a reminder - I can assist with cybersecurity topics like phishing, password safety, malware, WiFi security and safe browsing.",
        "I'm still here to help with your cybersecurity questions!"
    };


            Console.ForegroundColor = ConsoleColor.Green;
            TypeWriterEffect("I am here to help with your Cybersecurity needs. Please ask me a question or type exit/quit/end to quit.");
            Console.ResetColor();

            while (chatbotRun)
            {
                string userQuestion = Console.ReadLine()?.ToLower();

                if (string.IsNullOrWhiteSpace(userQuestion))
                {
                    ShowError("Please ask me a cybersecurity question. I'm here to help!");
                    continue;
                }

                ProcessInput(userQuestion);
                promptCount++;


                if (promptCount % 3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriterEffect(reminderMessages[_random.Next(reminderMessages.Count)]);
                    Console.ResetColor();
                }
            }
        }

        private void ProcessInput(string userInput) // this method is used to handle the input that the user gives the chatbot 
        {
            userInput = userInput.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                ShowError("Please ask a cybersecurity question.");
                return;
            }

            if (IsExitCommand(userInput))
            {
                chatbotRun = false;
                TypeWriterEffect($"Goodbye {usersName}, stay secure!");
                return;
            }

            if (HandleMemoryCommands(userInput))
            {
                return;
            }

            string emotion = detectEmotion(userInput);
            ChangeChatbotTone(emotion);

            if (currentTopic != null && HandleFollowUp(userInput))
            {
                return;
            }

            if (IsInterestDeclaration(userInput, out string interestTopic))
            {
                HandleInterestDeclaration(interestTopic);
                currentTopic = interestTopic;
                return;
            }

            if (HandleSpecificQuestions(userInput))
            {
                return;
            }

            foreach (var handler in keywordResponseHandlers)
            {
                if (userInput.Contains(handler.Key))
                {
                    currentTopic = handler.Key;
                    handler.Value(handler.Key);
                    return;
                }
            }

            TypeWriterEffect(OutputRandomResponses("default"));
            currentTopic = null;
        }

        private bool HandleMemoryCommands(string input) // this method is used to handle the input the user inputs with regards to seeing what the chatbot remebers 
        {
            if (input.Contains("what do you remember") || input.Contains("what you know"))
            {
                ShowRememberedInfo();
                return true;
            }

            if (input.Contains("forget what you know") || input.Contains("clear memory"))
            {
                userMemory.Clear();
                TypeWriterEffect("I've cleared all stored information.");
                return true;
            }

            return false;
        }

        private void ShowRememberedInfo()// this method is used to show the stored memeory that the chatbot has has intrest andusers name 
        {
            if (userMemory.Count == 0)
            {
                TypeWriterEffect("I don't remember any specific preferences yet.");
                return;
            }

            var response = new StringBuilder();
            response.Append("Here's what I remember:");

            if (userMemory.ContainsKey("interest"))
            {
                response.Append($"\n- You're interested in {userMemory["interest"]} security");
            }

            if (!string.IsNullOrEmpty(usersName))
            {
                response.Append($"\n- Your name is {usersName}");
            }

            TypeWriterEffect(response.ToString());
        }

        private bool IsExitCommand(string input) // determining if the user has inputted the exit command to stop app 
        {
            return input.Contains("quit") || input.Contains("exit") || input.Contains("end");
        }

        private bool HandleSpecificQuestions(string input) // this method is used to handle the specific question the user asks 
        {
            if (input.Contains("what is phishing"))
            {
                currentTopic = "phishing";
                TypeWriterEffect("Phishing is a type of cyberattack where attackers impersonate legitimate institutions to steal sensitive data like login credentials or financial information.");
                return true;
            }

            if (input.Contains("what is password safety") || input.Contains("what is password security"))
            {
                currentTopic = "password";
                TypeWriterEffect("Password safety refers to practices like using strong, unique passwords, enabling multi-factor authentication, and avoiding password reuse.");
                return true;
            }
            if (input.Contains("what is safe passwords") || (input.Contains("what are safe passwords")))
            {
                currentTopic = "password";
                TypeWriterEffect("Safe passwords are passwords that contain upper and lower case letters as well as numerical values and special characters");
                return true;

            }

            if (input.Contains("what is safe browsing") || input.Contains("what is secure browsing"))
            {
                currentTopic = "browsing";
                TypeWriterEffect("Safe browsing means using secure connections (HTTPS), avoiding suspicious sites, using updated browsers, and being cautious with downloads and pop-ups.");
                return true;
            }

            if (input.Contains("what is malware") || input.Contains("what are viruses"))
            {
                currentTopic = "malware";
                TypeWriterEffect("Malware refers to malicious software designed to damage or gain unauthorized access to computer systems, including viruses, trojans, spyware, and ransomware.");
                return true;
            }

            if (input.Contains("what is wifi security") || input.Contains("how to secure wifi"))
            {
                currentTopic = "wifi";
                TypeWriterEffect("WiFi security involves protecting your wireless network from unauthorized access through encryption, strong passwords, and proper router configuration.");
                return true;
            }

            if (input.Contains("spot phishing") || input.Contains("identify phishing"))
            {
                currentTopic = "phishing";
                TypeWriterEffect("Look for: 1) Generic greetings, 2) Urgent threats, 3) Mismatched URLs, 4) Poor grammar, 5) Requests for sensitive info.");
                return true;
            }

            if (input.Contains("detect malware") || input.Contains("identify malware") || input.Contains("malware signs"))
            {
                currentTopic = "malware";
                TypeWriterEffect("Watch for: 1) Slow performance, 2) Unexpected pop-ups, 3) Strange browser behavior, 4) Missing files, 5) Unusual network activity.");
                return true;
            }

            if (input.Contains("secure my wifi") || input.Contains("protect wifi network"))
            {
                currentTopic = "wifi";
                TypeWriterEffect("Secure your WiFi by: 1) Using WPA3 encryption, 2) Creating strong passwords, 3) Changing default credentials, 4) Enabling firewall, 5) Disabling WPS.");
                return true;
            }

            if (input.Contains("what if i have been a victim of phishing") ||
                input.Contains("i think i was phished") ||
                input.Contains("i have been a victim of phishing") ||
                input.Contains("i fell for a phishing scam") ||
                input.Contains("i clicked a phishing link") ||
                input.Contains("i gave my info to a scammer") ||
                input.Contains("what should i do after phishing") ||
                input.Contains("phishing attack happened") ||
                input.Contains("compromised due to phishing") ||
                input.Contains("phishing incident"))
            {
                currentTopic = "phishing";
                ShowActionSteps();
                return true;
            }

            if (input.Contains("i think i have malware") ||
                input.Contains("my computer has a virus") ||
                input.Contains("infected with malware") ||
                input.Contains("what to do if i have malware") ||
                input.Contains("malware infection") ||
                input.Contains("virus on my computer"))
            {
                currentTopic = "malware";
                ShowMalwareActionSteps();
                return true;
            }

            if (input.Contains("my wifi was hacked") ||
                input.Contains("someone is on my network") ||
                input.Contains("wifi compromised") ||
                input.Contains("unauthorized wifi access") ||
                input.Contains("wifi breached"))
            {
                currentTopic = "wifi";
                ShowWifiActionSteps();
                return true;
            }

            return false;
        }

        private bool HandleFollowUp(string input)// this method ensures that the follow up questions asked by the user are handled 
        {
            if (input.Contains("more") || input.Contains("explain further") || input.Contains("go on"))
            {
                TypeWriterEffect(GetDeeperExplanation(currentTopic));
                return true;
            }

            if (input.Contains("clarify") || input.Contains("dont understand") || input.Contains("confused"))
            {
                TypeWriterEffect(GetSimplerExplanation(currentTopic));
                return true;
            }

            if (input.Contains("what about") || input.Contains("how about") || input.Contains("also"))
            {
                TypeWriterEffect(GetRelatedTopic(currentTopic));
                return true;
            }

            return false;
        }

        private string GetDeeperExplanation(string topic)// if the user asks for more info or to explain further the chatbot will output this 
        {
            switch (topic)
            {
                case "phishing":
                    return "Advanced phishing attacks can spoof entire websites and even use HTTPS. Always double-check URLs and avoid clicking links in unsolicited messages.";
                case "password":
                    return "Passphrases like 'BlueHorse$RunsFast!' are both secure and easier to remember. Password managers can generate and store them securely.";
                case "browsing":
                    return "Using browser extensions like HTTPS Everywhere or Privacy Badger can help enforce safe browsing and protect your identity online.";
                case "malware":
                    return "Modern malware can use fileless techniques that live in memory to avoid detection. Use behavioral monitoring and keep your security tools updated to the latest versions.";
                case "wifi":
                    return "Consider implementing MAC address filtering and network segmentation for advanced WiFi security. Also, regularly check connected devices to identify unauthorized access.";
                default:
                    return "Let me provide more details on that...";
            }
        }

        private string GetSimplerExplanation(string topic)// if the user dosunt understand the chatbot will output these responces 
        {
            switch (topic)
            {
                case "phishing":
                    return "Phishing is when someone tricks you into clicking fake links to steal your information, like passwords.";
                case "password":
                    return "Password safety means using long, hard-to-guess passwords and not using the same one everywhere.";
                case "browsing":
                    return "Safe browsing means being careful about the websites you visit and only clicking on trusted links.";
                case "malware":
                    return "Malware is bad software that can harm your computer or steal your information. Like viruses or spyware.";
                case "wifi":
                    return "WiFi security is about keeping strangers from using your internet connection or spying on what you're doing online.";
                default:
                    return "Let me put it in simpler terms...";
            }
        }

        private string GetRelatedTopic(string topic)// if the user asks for a topic related to what they asked the chatbot will output this 
        {
            switch (topic)
            {
                case "phishing":
                    return "You might also want to learn about 'smishing', which is phishing via text messages.";
                case "password":
                    return "A related topic is 'two-factor authentication' which adds another layer of security beyond just a password.";
                case "browsing":
                    return "You might also want to explore VPNs, which add privacy when browsing online.";
                case "malware":
                    return "You might also be interested in 'ransomware', a type of malware that locks your files until you pay a ransom.";
                case "wifi":
                    return "You might also want to learn about 'evil twin' attacks, where hackers set up fake WiFi networks with names similar to legitimate ones.";
                default:
                    return "What else are you curious about?";
            }
        }

        private bool IsInterestDeclaration(string input, out string topic)
        {
            input = input.ToLower();
            topic = null;


            if (Regex.IsMatch(input, @"\b(interested in|care about|like|want to learn about)\b")) // allows there to be diffrent way for the user to input they are interested in something 
            {
                if (input.Contains("phishing") || input.Contains("email scam"))
                {
                    topic = "phishing";
                    return true;
                }
                else if (input.Contains("passwords") || input.Contains("login"))
                {
                    topic = "password";
                    return true;
                }
                else if (input.Contains("browsing") || input.Contains("internet") || input.Contains("web"))
                {
                    topic = "browsing";
                    return true;
                }
                else if (input.Contains("malware") || input.Contains("virus") || input.Contains("trojan") || input.Contains("ransomware"))
                {
                    topic = "malware";
                    return true;
                }
                else if (input.Contains("wifi") || input.Contains("wireless") || input.Contains("network security") || input.Contains("router"))
                {
                    topic = "wifi";
                    return true;
                }
            }

            return false;
        }

        private void HandleInterestDeclaration(string topic)// this method hanldles the output of what the user has intrest in 
        {
            RememberUserInterest(topic);

            var responses = new Dictionary<string, List<string>>()
            {
                {"password", new List<string>
                {
                    $"I'll remember password security matters to you, {usersName}.",
                    $"Noted! I'll focus on password tips for you, {usersName}.",

                }},
                {"phishing", new List<string>
                {
                    $"Noted, I'll watch for phishing topics for you, {usersName}.",
                    $"I'll remember this, Phishing awareness is important!",

                }},
                {"browsing", new List<string>
                {
                    $"Noted! Web safety is important to you.",
                    $"I'll remember you are intrested in safe browsing."
                }},
                {"malware", new List<string>
                {
                    $"I'll remember malware protection matters to you, {usersName}.",
                    $"Got it! Malware defense is an important topic."
                }},
                {"wifi", new List<string>
                {
                    $"I'll remember WiFi security is important to you, {usersName}.",
                    $"Noted! Network security is a great focus area."
                }}
            };

            TypeWriterEffect(responses[topic][_random.Next(responses[topic].Count)]);
        }

        public void RememberUserInterest(string topic) // method used to remeber the intrest that the user has mentioned 
        {
            if (userMemory.ContainsKey("interest"))
            {
                userMemory["interest"] = topic;
            }
            else
            {
                userMemory.Add("interest", topic);
            }
        }

        private string OutputRandomResponses(string topic)
        {
            if (topic == "default")
                return defaultResponses[_random.Next(defaultResponses.Count)];

            return keywordResponses[topic][_random.Next(keywordResponses[topic].Count)];
        }

        private void ShowActionSteps()
        {
            TypeWriterEffect("If you've been compromised by phishing:");
            Console.WriteLine("1. Change affected passwords immediately");
            Console.WriteLine("2. Contact your bank if financial info was shared");
            Console.WriteLine("3. Scan devices for malware");
            Console.WriteLine("4. Report to the impersonated organization");
        }

        private void ShowMalwareActionSteps()
        {
            TypeWriterEffect("If you suspect malware infection:");
            Console.WriteLine("1. Disconnect from the internet to prevent data theft");
            Console.WriteLine("2. Run a full system scan with updated antivirus software");
            Console.WriteLine("3. Use a separate malware removal tool as a second opinion");
            Console.WriteLine("4. Back up important data if not already done");
            Console.WriteLine("5. Consider resetting your system if infection persists");
        }

        private void ShowWifiActionSteps()
        {
            TypeWriterEffect("If your WiFi network is compromised:");
            Console.WriteLine("1. Change your router's admin password immediately");
            Console.WriteLine("2. Update router firmware to the latest version");
            Console.WriteLine("3. Change your WiFi network name and password");
            Console.WriteLine("4. Enable WPA3 encryption if available");
            Console.WriteLine("5. Check connected devices and remove unknown ones");
        }

        public override string detectEmotion(string input) // this method is used to detect if there is any emotion in the users input 
        {
            if (input.Contains("worried") || input.Contains("scared"))
                return "worried";

            if (input.Contains("angry") || input.Contains("frustrated") || input.Contains("frustrating"))
                return "frustrated";

            if (input.Contains("confused") || input.Contains("unsure"))
                return "confused";

            return "neutral";
        }

        private void ChangeChatbotTone(string emotion)// this method is used to respond to the users emotion and chnage the emotion of the chat bot 
        {
            switch (emotion)
            {
                case "worried":
                    TypeWriterEffect("I understand this is concerning. Let me help...");
                    break;
                case "frustrated":
                    TypeWriterEffect("Cybersecurity can be frustrating. Let's solve this...");
                    break;
                case "confused":
                    TypeWriterEffect("This can be confusing. Let me explain...");
                    break;
            }
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            TypeWriterEffect(message);
            Console.ResetColor();
        }

        private void TypeWriterEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(_random.Next(5, 20));
                if (c == ' ')
                {
                    Thread.Sleep(_random.Next(20, 40));
                }
            }
            Console.WriteLine();
        }

        public void SetUserName(string name)
        {
            this.usersName = name;
            userMemory["name"] = name;
        }
    }
}


