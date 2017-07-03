using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Drawing;
using System.Media;


namespace ChatServer
{
    // Holds the arguments for the StatusChanged event
    public class StatusChangedEventArgs : EventArgs
    {
        // The argument we're interested in is a message describing the event
        private string EventMsg;

        // Property for retrieving and setting the event message
        public string EventMessage
        {
            get
            {
                return EventMsg;
            }
            set
            {
                EventMsg = value;
            }
        }

        // Constructor for setting the event message
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    // This delegate is needed to specify the parameters we're passing with our event
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    class ChatServer
    {
        // This hash table stores users and connections (browsable by user)
        public static Hashtable htUsers = new Hashtable(30); // 30 users at one time limit
        // This hash table stores connections and users (browsable by connection)
        public static Hashtable htConnections = new Hashtable(30); // 30 users at one time limit
        // Will store the IP address passed to it
        private IPAddress ipAddress;
        private TcpClient tcpClient;
        // The event and its argument will notify the form when a user has connected, disconnected, send message, etc.
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;
        int[] arCardImagesName;
        static Random rnd;
        Card newCard;
        public static Card[] arrTaki;
        private Image cardCurrent;
    
       
        // The constructor sets the IP address to the one retrieved by the instantiating object
        public ChatServer(IPAddress address)
        {
            ipAddress = address;
        }

        // The thread that will hold the connection listener
        private Thread thrListener;

        // The TCP object that listens for connections
        private TcpListener tlsClient;

        // Will tell the while loop to keep monitoring for connections
        bool ServRunning = false;

        // Add the user to the hash tables
        public static void AddUser(TcpClient tcpUser, string strUsername)
        {
            // First add the username and associated connection to both hash tables
            ChatServer.htUsers.Add(strUsername, tcpUser);
            ChatServer.htConnections.Add(tcpUser, strUsername);
            //TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count];
            StreamWriter swSenderSender;

            // Tell of the new connection to all other users and to the server form
            //  SendAdminMessage(htConnections[tcpUser] + " has joined us");
            swSenderSender = new StreamWriter(tcpUser.GetStream());


        }

        // Remove the user from the hash tables
        public static void RemoveUser(TcpClient tcpUser)
        {
            // If the user is there
            if (htConnections[tcpUser] != null)
            {
                // First show the information and tell the other users about the disconnection
                SendAdminMessage(htConnections[tcpUser] + " has left us");

                // Remove the user from the hash table
                ChatServer.htUsers.Remove(ChatServer.htConnections[tcpUser]);
                ChatServer.htConnections.Remove(tcpUser);
            }
        }

        // This is called when we want to raise the StatusChanged event
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                // Invoke the delegate
                statusHandler(null, e);
            }
        }

        public void BuiltArrayOfCards()
        {

            for (int i = 0; i < arCardImagesName.Length; i++)
            {
                arCardImagesName[i] = i;
                arrTaki[i] = CheckInArr(i);
            }
            // string[] arOfCardsFiles = Directory.GetFiles(@"..\..\Images", "*.png", SearchOption.AllDirectories);


        }
        private Card CheckInArr(int num)
        {

            //קוביות בצבע מסוים-13
            //קלף משנה כיוון-0
            //קלף +-10
            //קלף עצור-11
            //טאקי בצבע מסוים-12
            if (num == 0)
                newCard = new Card("Blue", 13, "Pocket");
            if (num == 1)
                newCard = new Card("Blue", 0, "Pocket");
            if (num == 2)
                newCard = new Card("Blue", 8, "Pocket");
            if (num == 3)
                newCard = new Card("Blue", 5, "Pocket");
            if (num == 4)
                newCard = new Card("Blue", 1, "Pocket");
            if (num == 5)
                newCard = new Card("Blue", 4, "Pocket");
            if (num == 6)
                newCard = new Card("Blue", 9, "Pocket");
            if (num == 7)
                newCard = new Card("Blue", 10, "Pocket");
            if (num == 8)
                newCard = new Card("Blue", 2, "Pocket");
            if (num == 9)
                newCard = new Card("Blue", 7, "Pocket");
            if (num == 10)
                newCard = new Card("Blue", 6, "Pocket");
            if (num == 11)
                newCard = new Card("Blue", 11, "Pocket");
            if (num == 12)
                newCard = new Card("Blue", 12, "Pocket");
            if (num == 13)
                newCard = new Card("Blue", 3, "Pocket");
            if (num == 14)
                newCard = new Card("Colorful", 12, "Pocket");
            if (num == 15)
                newCard = new Card("Colorful", 13, "Pocket");
            if (num == 16)
                newCard = new Card("Green", 0, "Pocket");
            if (num == 17)
                newCard = new Card("Green", 8, "Pocket");
            if (num == 18)
                newCard = new Card("Green", 5, "Pocket");
            if (num == 19)
                newCard = new Card("Green", 1, "Pocket");
            if (num == 20)
                newCard = new Card("Green", 4, "Pocket");
            if (num == 21)
                newCard = new Card("Green", 13, "Pocket");
            if (num == 22)
                newCard = new Card("Green", 9, "Pocket");
            if (num == 23)
                newCard = new Card("Green", 10, "Pocket");
            if (num == 24)
                newCard = new Card("Green", 2, "Pocket");
            if (num == 25)
                newCard = new Card("Green", 7, "Pocket");
            if (num == 26)
                newCard = new Card("Green", 6, "Pocket");
            if (num == 27)
                newCard = new Card("Green", 11, "Pocket");
            if (num == 28)
                newCard = new Card("Green", 12, "Pocket");
            if (num == 29)
                newCard = new Card("Green", 3, "Pocket");
            if (num == 30)
                newCard = new Card("Red", 0, "Pocket");
            if (num == 31)
                newCard = new Card("Red", 8, "Pocket");
            if (num == 32)
                newCard = new Card("Red", 5, "Pocket");
            if (num == 33)
                newCard = new Card("Red", 1, "Pocket");
            if (num == 34)
                newCard = new Card("Red", 4, "Pocket");
            if (num == 35)
                newCard = new Card("Red", 9, "Pocket");
            if (num == 36)
                newCard = new Card("Red", 10, "Pocket");
            if (num == 37)
                newCard = new Card("Red", 13, "Pocket");
            if (num == 38)
                newCard = new Card("Red", 2, "Pocket");
            if (num == 39)
                newCard = new Card("Red", 7, "Pocket");
            if (num == 40)
                newCard = new Card("Red", 6, "Pocket");
            if (num == 41)
                newCard = new Card("Red", 11, "Pocket");
            if (num == 42)
                newCard = new Card("Red", 12, "Pocket");
            if (num == 43)
                newCard = new Card("Red", 3, "Pocket");
            if (num == 44)
                newCard = new Card("Yellow", 0, "Pocket");
            if (num == 45)
                newCard = new Card("Yellow", 13, "Pocket");
            if (num == 46)
                newCard = new Card("Yellow", 8, "Pocket");
            if (num == 47)
                newCard = new Card("Yellow", 5, "Pocket");
            if (num == 48)
                newCard = new Card("Yellow", 1, "Pocket");
            if (num == 49)
                newCard = new Card("Yellow", 4, "Pocket");
            if (num == 50)
                newCard = new Card("Yellow", 9, "Pocket");
            if (num == 51)
                newCard = new Card("Yellow", 10, "Pocket");
            if (num == 52)
                newCard = new Card("Yellow", 2, "Pocket");
            if (num == 53)
                newCard = new Card("Yellow", 7, "Pocket");
            if (num == 54)
                newCard = new Card("Yellow", 6, "Pocket");
            if (num == 55)
                newCard = new Card("Yellow", 11, "Pocket");
            if (num == 56)
                newCard = new Card("Yellow", 12, "Pocket");
            if (num == 57)
                newCard = new Card("Yellow", 3, "Pocket");
            return newCard;
        }


        // Send administrative messages
        public static void SendAdminMessage(string Message)
        {
            StreamWriter swSenderSender;

            // First of all, show in our application who says what
            e = new StatusChangedEventArgs("Administrator: " + Message);
            OnStatusChanged(e);

            // Create an array of TCP clients, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count];
            // Copy the TcpClient objects into the array
            ChatServer.htUsers.Values.CopyTo(tcpClients, 0);
            // Loop through the list of TCP clients
            for (int i = 0; i < tcpClients.Length; i++)
            {
                // Try sending a message to each
                try
                {
                    // If the message is blank or the connection is null, break out
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    // Send the message to the current user in the loop
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine(Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // If there was a problem, the user is not there anymore, remove him
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        // Send messages from one user to all the others
        public static void SendMessage1(string From, string Message)
        {
            StreamWriter swSenderSender;

            // First of all, show in our application who says what
            e = new StatusChangedEventArgs(From + " says: " + Message);
            OnStatusChanged(e);

            // Create an array of TCP clients, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count];
            // Copy the TcpClient objects into the array
            ChatServer.htUsers.Values.CopyTo(tcpClients, 0);
            // Loop through the list of TCP clients

            // Try sending a message to each
              for(int i=0;i<=htConnections.Count-1;i++)
                { 
                  try
            {
             
                // If the message is blank or the connection is null, break out
                if (Message.Trim() != "" || tcpClients[i] != null)
                {

                    // Send the message to the current user in the loop
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());//למי אני שולחת
                     rnd = new Random();
                        int num = rnd.Next(0, 58);
                        swSenderSender.WriteLine(Message);//מה שאני שולחת
                        swSenderSender.Flush();
                        swSenderSender = null;
                        //swSenderSender.WriteLine(num);//מה שאני שולחת
                        //  swSenderSender.Flush();
                        //  swSenderSender = null;
                    



                }
                
            }
            catch // If there was a problem, the user is not there anymore, remove him
            {
                RemoveUser(tcpClients[i]);
            }
        }
        }
        
        



        public void StartListening()
        {

            // Get the IP of the first network device, however this can prove unreliable on certain configurations
            IPAddress ipaLocal = ipAddress;

            // Create the TCP listener object using the IP of the server and the specified port
            tlsClient = new TcpListener(1986);

            // Start the TCP listener and listen for connections
            tlsClient.Start();

            // The while loop will check for true in this before checking for connections
            ServRunning = true;

            // Start the new tread that hosts the listener
            thrListener = new Thread(KeepListening);
            thrListener.Start();
        }

        private void KeepListening()
        {
            // While the server is running
            while (ServRunning == true)
            {
                // Accept a pending connection
                tcpClient = tlsClient.AcceptTcpClient();
                // Create a new instance of Connection
                Connection newConnection = new Connection(tcpClient);
            }
        }
    }

    // This class handels connections; there will be as many instances of it as there will be connected users
    class Connection
    {
        TcpClient tcpClient;
        // The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string strResponse;
        static Random rnd;
        public int count1 = 0;
        // The constructor of the class takes in a TCP connection
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            // The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            // The thread calls the AcceptClient() method
            thrSender.Start();
        }

        private void CloseConnection()
        {
            // Close the currently open objects
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        // Occures when a new client is accepted
        private void AcceptClient()
        {
           
           
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());

            // Read the account information from the client
            currUser = srReceiver.ReadLine();

            // We got a response from the client
            if (currUser != "")
            {
                // Store the user name in the hash table
                if (ChatServer.htUsers.Contains(currUser) == true)
                {
                    // 0 means not connected
                    swSender.WriteLine("0|This username already exists.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                    if (currUser == "Administrator")
                    {
                        // 0 means not connected
                        swSender.WriteLine("0|This username is reserved.");
                        swSender.Flush();
                        CloseConnection();
                        return;
                    }
                    else
                    {
                        // 1 means connected successfully
                        swSender.WriteLine("1");
                        swSender.Flush();

                        // Add the user to the hash tables and start listening for messages from him
                        ChatServer.AddUser(tcpClient, currUser);
                        // Otherwise send the message to all the other users
                      
                           
                           // StreamWriter swSenderSender;
                           // rnd = new Random();
                          //  int num = rnd.Next(0, 58);
                            //ChatServer.SendAdminMessage(num.ToString());
                            if (ChatServer.htConnections.Count == 1)
                            {
                              
                                rnd = new Random();
                                for (int pocket = 0; pocket < 8; pocket++)
                                {
                                    // swSenderSender = new StreamWriter(tcpClient.GetStream());//למי אני שולחת

                                    int num = rnd.Next(0, 58);
                                    ChatServer.SendMessage1(currUser, "1"+num.ToString());
                                    count1++;
                                }
                            }
                            if (ChatServer.htConnections.Count == 2)
                            {
                                
                               
                                
                                //StreamWriter swSenderSender;
                                rnd = new Random();
                                for (int pocket = 0; pocket < 8; pocket++)
                                {
                                    //swSenderSender = new StreamWriter(tcpClient.GetStream());//למי אני שולחת
                                  
                                    int num = rnd.Next(0, 58);
                                    ChatServer.SendMessage1( currUser ,"2"+num.ToString());
                                    count1++;
                                }
                                if (ChatServer.htConnections.Count == 2)
                                {
                                    if (count1 == 8)
                                    {
                                        int opening = rnd.Next(0, 58);
                                        if (opening == 0)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 1)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 7)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 8)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 11)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 12)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 14)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 15)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 16)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 22)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 23)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 24)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 27)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 28)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 30)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 36)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 37)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 38)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 41)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 42)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 44)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 45)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 51)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 52)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 55)
                                            opening = rnd.Next(0, 58);
                                        if (opening == 56)
                                            opening = rnd.Next(0, 58);
                                        string openCard = "-" + opening.ToString();
                                        ChatServer.SendAdminMessage(openCard);

                                    }
                                }
                            }
                    }
                
             
             
                

                try
                {
                    // Keep waiting for a message from the user
                    while ((strResponse = srReceiver.ReadLine()) != "")
                    {
                        // If it's invalid, remove the user
                        if (strResponse == null)
                        {
                            ChatServer.RemoveUser(tcpClient);
                        }
                        else
                        {
                           
                            
                            // if (leftDigit == 9)
                            // Otherwise send the message to all the other users
                            ChatServer.SendMessage1(currUser, strResponse);
                        }
                    }
                }
                catch
                {
                    // If anything went wrong with this user, disconnect him
                    ChatServer.RemoveUser(tcpClient);
                }
               // string str = srReceiver.ReadLine();
               // ChatServer.SendAdminMessage("-"+str);

                
                

               
            }

        }
    }
}