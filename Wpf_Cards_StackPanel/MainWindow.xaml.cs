using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Animation;
using System.Net.Sockets;
using System.Threading;
using System.Net;
namespace Wpf_Cards_StackPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string UserName = "Unknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
        // Needed to update the form with messages from another thread
        private delegate void UpdateLogCallback(string strMessage);
        // Needed to set the form to a "disconnected" state from another thread
        private delegate void CloseConnectionCallback(string strReason);
        private Thread thrMessaging;
        private IPAddress ipAddr;
        private bool Connected;



        List<Image> player1Cards = new List<Image>();
        List<Image> player2Cards = new List<Image>();
        Image[] arCardImages;
        static Random rnd;
        Card newCard;
        private Image cardCurrent;
        private int num;
        private int num1;
        private int num2;
        bool turn = true;
        bool checki = false;
        Node<Image> p1;
        Node<Image> p2;
        int randturn;
        int CountOfCard;
        double widthCard;
        Image img;
        Image img2;
        int index;
        public  string TurnOfPlayer;
        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();

            Application.Current.Exit += new ExitEventHandler(OnApplicationExit);
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                //close all the open connections
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
            }
        }

        private void CreateStackPanel(string strMessage)
        {

            
                if (TurnOfPlayer=="One")
                {
                    widthCard = stackPanel1.ActualWidth / 8;
                    if (strMessage[0].Equals("o"))
                    {
                        string str = strMessage.Remove(0, 1);
                        int open = Convert.ToInt16(str);
                        OpeningCard.Source = arCardImages[open].Source;
                    }
                    int num = Convert.ToInt16(strMessage);
                    img = new Image();
                    img.Source = arCardImages[num].Source;
                  //  img.Margin = new Thickness(-20, 0, 0, 0);
                    img.Stretch = Stretch.Fill;
                    img.Tag = arCardImages[num].Tag;

                    img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                    img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                    img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                    stackPanel1.Children.Add(img);
                }
                else
                {
                    if (TurnOfPlayer=="Two")
                    {
                        widthCard = stackPanel1.ActualWidth / 8;
                        if (strMessage[0].Equals("o"))
                        {
                            string str = strMessage.Remove(0, 1);
                            int open = Convert.ToInt16(str);
                            OpeningCard.Source = arCardImages[open].Source;
                        }
                        int num = Convert.ToInt16(strMessage);
                        img = new Image();
                        img.Source = arCardImages[num].Source;
                        // img.Margin = new Thickness(-20, 0, 0, 0);
                        img.Stretch = Stretch.Fill;
                        img.Tag = arCardImages[num].Tag;

                        img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                        img.MouseDown += new MouseButtonEventHandler(img2_MouseDown);
                        img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                        stackPanel2.Children.Add(img);
                    }
                }
            
            p1 = player1Cards.GetFirst();
            p2 = player2Cards.GetFirst();
           
            
        }



        //שחקן 1
        private void stackPanel1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            stackPanel1.MouseDown -= stackPanel1_MouseDown;
            stackPanel2.MouseDown += stackPanel2_MouseDown;
        }

        //שחקן 2
        private void stackPanel2_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
            stackPanel2.MouseDown -= stackPanel2_MouseDown;
            stackPanel1.MouseDown += stackPanel1_MouseDown;
        }



        void img_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            DoubleAnimation da = new DoubleAnimation();
            da.From = img.ActualWidth;
            da.To = img.ActualWidth / 1.5;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            da.FillBehavior = FillBehavior.Stop;
            img.BeginAnimation(Image.WidthProperty, da);
        }

        void imgAddCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int times = 1;
            Image imgTaki = (Image)sender;
            int index;
            Card card;
            for (index = 0; index < arCardImages.Length; index++)
            {
                if (imgTaki.Source == arCardImages[index].Source)
                    num1 = index;

            }
            card = CheckInArr(num1);
            if (card.GetCardNumber() == 2)
            {
                times = 2;
                turn = !turn;
            }
            if (TurnOfPlayer.Equals("One"))
            {
                for (int j = 0; j < times; j++)
                {
                    stackPanel1.MouseDown += stackPanel1_MouseDown;
                    lbTurn.Content = "התור של שחקן 2";
                    Image img = new Image();
                    player1Cards.Insert(p1, img);
                    turn = false;
                    img.Source = arCardImages[rnd.Next(arCardImages.Length)].Source;
                    CountOfCard = stackPanel1.Children.Count + 1;
                    widthCard = stackPanel1.ActualWidth / CountOfCard;
                    img.Stretch = Stretch.Fill;
                    stackPanel1.Children.Add(img);
                    for (int i = stackPanel1.Children.Count - 1; i > 0; i--)
                    {
                        img = (Image)stackPanel1.Children[i];
                        img.Width = widthCard;
                        img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                        img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                        img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                        stackPanel1.Children[i] = img;
                        player1Cards.Insert(p1, img);

                    }
                }
            }
            else
            {
               
                    for (int j = 0; j < times; j++)
                    {
                        stackPanel2.MouseDown += new MouseButtonEventHandler(stackPanel2_MouseDown);
                        lbTurn.Content = "התור של שחקן 1";
                        turn = true;
                        Image img = new Image();
                        int numb3 = rnd.Next(arCardImages.Length);
                        img.Source = arCardImages[numb3].Source;
                        CountOfCard = stackPanel2.Children.Count + 1;
                        widthCard = stackPanel2.ActualWidth / CountOfCard;
                        img.Stretch = Stretch.Fill;
                        stackPanel2.Children.Add(img);
                        player2Cards.Insert(p2, img);
                        for (int i = stackPanel2.Children.Count - 1; i > 0; i--)
                        {
                            img = (Image)stackPanel2.Children[i];
                            img.Width = widthCard;
                            img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                            img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                            img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                            stackPanel2.Children[i] = img;
                            player2Cards.Insert(p2, img);
                        }
                    }
                }
            

        }



        //שחקן 1
        void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stackPanel1.MouseDown += new MouseButtonEventHandler(stackPanel1_MouseDown);
            cardCurrent = (Image)sender;
            cardCurrent.Name = "Card1";
            CheckCard(cardCurrent);
            if (checki)
                checki = false;
            else
            {
                NameScope.SetNameScope(this, new NameScope());
                RegisterName(cardCurrent.Name, cardCurrent);
                Int32Animation row = new Int32Animation();
                row.Duration = TimeSpan.FromSeconds(1.0);
                row.From = Grid.GetRow(cardCurrent);
                row.To = Grid.GetRow(cardCurrent) - 1;
                DoubleAnimation angle = new DoubleAnimation();
                angle.Duration = TimeSpan.FromSeconds(1.5);
                angle.From = 0.0;
                angle.To = 360.0;
                //-----------------------------------------------------------
                int numb = 0;
                for (int i = 0; i < arCardImages.Length; i++)
                {
                    if (arCardImages[i].Source == cardCurrent.Source)
                        numb = i;
                }
                string str;
                str =  numb.ToString();
                if (TurnOfPlayer == "One")
                {
                    swSender.WriteLine("-"+str);
                    swSender.Flush();
                }
                if (TurnOfPlayer == "Two")
                {
                    swSender.WriteLine("-" + str);
                    swSender.Flush();
                }
                angle.Completed += new EventHandler(Angle_Completed);
                Storyboard sb = new Storyboard();
                Storyboard.SetTargetName(row, cardCurrent.Name);
                Storyboard.SetTargetProperty(row, new PropertyPath(Grid.RowProperty));
                RotateTransform rt = new RotateTransform(30.0, 0.0, 0.0);
                cardCurrent.RenderTransform = rt;
                cardCurrent.RenderTransformOrigin = new Point(0.5, 0.5);

                Double angel1 = 1.0;
                this.RegisterName("angle1", angel1);
                Storyboard.SetTargetName(angle, "angel1");
                Storyboard.SetTargetProperty(angle, new PropertyPath(RotateTransform.AngleProperty));
                rt.BeginAnimation(RotateTransform.AngleProperty, angle);


                row.BeginAnimation(Grid.RowProperty, row);


            }

            int index;
            Card temp;
            for (index = 0; index < arCardImages.Length; index++)
            {
                if (cardCurrent.Source == arCardImages[index].Source)
                    num1 = index;

            }
            temp = CheckInArr(num1);
            if ((temp.GetCardNumber() == 10) || (temp.GetCardNumber() == 11))
            {
                lbTurn.Content = "התור של שחקן 1";
            }
            else
            {
                turn = !turn;
                lbTurn.Content = "התור של שחקן 2";
            }

        }
            

        void CheckWin()
        {
            if (stackPanel1.Children.Count == 0&&TurnOfPlayer=="One")
            {
                stackPanel1.IsEnabled = false;
                stackPanel2.IsEnabled = false;
                lbMessage.Content = "שחקן 1 ניצח";
                stackPanel1.Visibility = Visibility.Hidden;
                stackPanel2.Visibility = Visibility.Hidden;
                imgAddCard.Visibility = Visibility.Hidden;
                OpeningCard.Visibility = Visibility.Hidden;
                imgWin1.Visibility = Visibility.Visible;
            }
            if (stackPanel2.Children.Count == 0&&TurnOfPlayer=="Two")
            {
                stackPanel2.IsEnabled = false;
                stackPanel1.IsEnabled = false;
                lbMessage.Content = "שחקן 2 ניצח";
                stackPanel1.Visibility = Visibility.Hidden;
                stackPanel2.Visibility = Visibility.Hidden;
                imgAddCard.Visibility = Visibility.Hidden;
                OpeningCard.Visibility = Visibility.Hidden;
                imgWin2.Visibility = Visibility.Visible;
               
            }
        }




        void Angle_Completed(object sender, EventArgs e)
        {
            OpeningCard.Source = cardCurrent.Source;
            stackPanel1.Children.Remove(cardCurrent);
            CheckWin();
        }


        //שחקן 2
        void img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TurnOfPlayer = "Two";
            stackPanel2.MouseDown += new MouseButtonEventHandler(stackPanel2_MouseDown);

            cardCurrent = (Image)sender;
            cardCurrent.Name = "Card2";
            CheckCard(cardCurrent);
            if (checki)
                checki = false;
            else
            {
                NameScope.SetNameScope(this, new NameScope());
                RegisterName(cardCurrent.Name, cardCurrent);
                Int32Animation row = new Int32Animation();
                row.Duration = TimeSpan.FromSeconds(1.0);
                row.From = Grid.GetRow(cardCurrent);
                row.To = Grid.GetRow(cardCurrent) - 1;
                DoubleAnimation angle = new DoubleAnimation();
                angle.Duration = TimeSpan.FromSeconds(1.5);
                angle.From = 0.0;
                angle.To = 360.0;
                //-------------------------------------------------------------
                int numb = 0;
                for (int i = 0; i < arCardImages.Length; i++)
                {
                    if (arCardImages[i].Source == cardCurrent.Source)
                        numb = i;
                }
                string str;
                str =  numb.ToString();
           
                if (TurnOfPlayer == "One")
                {
                    swSender.WriteLine("-" + str);
                    swSender.Flush();
                }
                if (TurnOfPlayer == "Two")
                {
                    swSender.WriteLine("-" + str);
                    swSender.Flush();
                }
                angle.Completed += new EventHandler(Angle_Completed2);
                Storyboard sb = new Storyboard();
                Storyboard.SetTargetName(row, cardCurrent.Name);
                Storyboard.SetTargetProperty(row, new PropertyPath(Grid.RowProperty));
                RotateTransform rt = new RotateTransform(30.0, 0.0, 0.0);
                cardCurrent.RenderTransform = rt;
                cardCurrent.RenderTransformOrigin = new Point(0.5, 0.5);
                Double angel1 = 1.0;
                this.RegisterName("angle1", angel1);
                Storyboard.SetTargetName(angle, "angel1");
                Storyboard.SetTargetProperty(angle, new PropertyPath(RotateTransform.AngleProperty));
                rt.BeginAnimation(RotateTransform.AngleProperty, angle);
                row.BeginAnimation(Grid.RowProperty, row);
            }

            int index;
            Card temp;
            for (index = 0; index < arCardImages.Length; index++)
            {
                if (cardCurrent.Source == arCardImages[index].Source)
                    num1 = index;

            }
            temp = CheckInArr(num1);
            if ((temp.GetCardNumber() == 10) || (temp.GetCardNumber() == 11))
            {
                lbTurn.Content = "התור של שחקן 2";
            }
            else
            {
                turn = !turn;
                lbTurn.Content = "התור של שחקן 1";
            }
        }


        void Angle_Completed2(object sender, EventArgs e)
        {
            OpeningCard.Source = cardCurrent.Source;
            stackPanel2.Children.Remove(cardCurrent);
            CheckWin();
        }
        void CheckCard(Image card)
        {
            Card cardT;
            Card cardPocket;
            int i;
            for (i = 0; i < arCardImages.Length; i++)
            {
                if (arCardImages[i].Source == card.Source)
                    num = i;
            }
            cardT = CheckInArr(num);
            for (i = 0; i < arCardImages.Length; i++)
            {
                if (arCardImages[i].Source == OpeningCard.Source)
                    num = i;
            }
            cardPocket = CheckInArr(num);

            if ((string.Equals(cardT.GetColor(), cardPocket.GetColor())) || (cardT.GetCardNumber() == cardPocket.GetCardNumber()) || (string.Equals(cardT.GetColor(), "Colorful")))
            {
                card.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                lbMessage.Content = " ";
            }
            else
            {
                lbMessage.Content = " הקלף לא מתאים נסה קלף אחר";
                checki = true;
            }

            if (string.Equals(cardT.GetColor(), "Colorful"))
            {
                lbMessage.Visibility = Visibility.Hidden;
                lbColTaki.Content = "בחר צבע";

                if (cardT.GetCardNumber() == 12)
                {
                    picYellow.Visibility = Visibility.Visible;
                    picBlue.Visibility = Visibility.Visible;
                    picRed.Visibility = Visibility.Visible;
                    picGreen.Visibility = Visibility.Visible;

                    picYellow.Source = arCardImages[12].Source;
                    picBlue.Source = arCardImages[28].Source;
                    picRed.Source = arCardImages[42].Source;
                    picGreen.Source = arCardImages[56].Source;
                }

                if (cardT.GetCardNumber() == 13)
                {
                    picYellow.Visibility = Visibility.Visible;
                    picBlue.Visibility = Visibility.Visible;
                    picRed.Visibility = Visibility.Visible;
                    picGreen.Visibility = Visibility.Visible;

                    picYellow.Source = arCardImages[0].Source;
                    picBlue.Source = arCardImages[21].Source;
                    picRed.Source = arCardImages[37].Source;
                    picGreen.Source = arCardImages[45].Source;

                }

                if (!cardT.GetColor().Equals("Colorful") && cardT.GetCardNumber() == 12)
                {
                    int num2 = 0;
                    if (TurnOfPlayer.Equals("One"))
                    {
                        lbMessage.Content = "ישנם עוד קלפים מתאימים";
                        while (p1.GetNext() != null)
                        {
                            for (int j = 0; j < arCardImages.Length; j++)
                            {
                                if (arCardImages[j].Source == p1.GetInfo().Source)
                                    num2 = j;
                            }
                            Card c1 = CheckInArr(num2);
                            for (int j = 0; j < arCardImages.Length; j++)
                            {
                                if (arCardImages[j].Source == OpeningCard.Source)
                                    num2 = j;
                            }
                            Card c2 = CheckInArr(num2);
                            if (c1.GetColor().Equals(c2.GetColor()))
                            {
                                Image img;
                                img = p1.GetInfo();
                                img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                            }
                            else
                            {
                                if (TurnOfPlayer.Equals("Two"))
                                {
                                    lbMessage.Content = "ישנם עוד קלפים מתאימים";
                                    while (p2.GetNext() != null)
                                    {
                                        for (int j = 0; j < arCardImages.Length; j++)
                                        {
                                            if (arCardImages[j].Source == p2.GetInfo().Source)
                                                num2 = j;
                                        }
                                        Card c3 = CheckInArr(num2);
                                        for (int j = 0; j < arCardImages.Length; j++)
                                        {
                                            if (arCardImages[j].Source == OpeningCard.Source)
                                                num2 = j;
                                        }
                                        Card c4 = CheckInArr(num2);
                                        if (c1.GetColor().Equals(c2.GetColor()))
                                        {
                                            Image img;
                                            img = p2.GetInfo();
                                            img.MouseDown += new MouseButtonEventHandler(img2_MouseDown);
                                        }

                                    }

                                }
                            }
                        }
                    }
                }

                if (cardT.GetCardNumber() == 2)
                {
                    lbTurn.Visibility = Visibility.Visible;
                    lbTurn.Content = "קח קלף מהקופה";
                    SoundPlayer SecondPlus = new SoundPlayer("SecondPlus.wav");
                    SecondPlus.Play();

                }

                if (cardT.GetCardNumber() == 11)
                {
                    SoundPlayer Stop = new SoundPlayer("Stop.wav");
                    Stop.Play();

                }



            }
        }

     

         Card CheckInArr(int num)
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





        void img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            DoubleAnimation da = new DoubleAnimation();
            da.From = img.ActualWidth;
            da.To = img.ActualWidth * 1.5;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            img.BeginAnimation(Image.WidthProperty, da);
        }



        private BitmapImage GetBitmapImage(string fileName)
        {
            BitmapImage bi = new BitmapImage();
            try
            {
                bi.BeginInit();
                bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + fileName, UriKind.Absolute);
                bi.EndInit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return bi;
        }
        private void CreateArrayOfCardsFiles()
        {

            string[] arOfCardsFiles = Directory.GetFiles(@"..\..\Images", "*.png", SearchOption.AllDirectories);
            arCardImages = new Image[arOfCardsFiles.Length];
            string FileName;

            for (int i = 0; i < arOfCardsFiles.Length; i++)
            {
                arCardImages[i] = new Image();
                arCardImages[i].Source = GetBitmapImage(arOfCardsFiles[i]);
                FileName = System.IO.Path.GetFileName(arOfCardsFiles[i]);
                arCardImages[i].Tag = FileName;
            }
        }


        //כחול
        //ירוק
        //אדום
        //צהוב
        private void picYellow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OpeningCard.Source == arCardImages[14].Source)
            {
                OpeningCard.Source = arCardImages[56].Source;
                picYellow.Visibility = Visibility.Hidden;
                picBlue.Visibility = Visibility.Hidden;
                picRed.Visibility = Visibility.Hidden;
                picGreen.Visibility = Visibility.Hidden;
                lbColTaki.Visibility = Visibility.Hidden;
            }
            else
            {
                if (OpeningCard.Source == arCardImages[15].Source)
                {
                    OpeningCard.Source = arCardImages[45].Source;
                    picYellow.Visibility = Visibility.Hidden;
                    picBlue.Visibility = Visibility.Hidden;
                    picRed.Visibility = Visibility.Hidden;
                    picGreen.Visibility = Visibility.Hidden;
                    lbColTaki.Visibility = Visibility.Hidden;
                }
                
                for (int i = 0; i < arCardImages.Length; i++)
                {
                    if (OpeningCard.Source == arCardImages[i].Source)
                        num = i;
                }
                Card cardi = CheckInArr(num2);
                string str;
                str = cardi.GetCardNumber().ToString();
                if (TurnOfPlayer == "One")
                {
                    swSender.WriteLine("-" + str);
                    swSender.Flush();
                }
                if (TurnOfPlayer == "Two")
                {
                    swSender.WriteLine("-" + str);
                    swSender.Flush();
                }
            }
        }

        private void picBlue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OpeningCard.Source == arCardImages[14].Source)
            {
                OpeningCard.Source = arCardImages[12].Source;
                picYellow.Visibility = Visibility.Hidden;
                picBlue.Visibility = Visibility.Hidden;
                picRed.Visibility = Visibility.Hidden;
                picGreen.Visibility = Visibility.Hidden;
                lbColTaki.Visibility = Visibility.Hidden;
            }
            else
            {
                if (OpeningCard.Source == arCardImages[15].Source)
                {
                    OpeningCard.Source = arCardImages[0].Source;
                    picYellow.Visibility = Visibility.Hidden;
                    picBlue.Visibility = Visibility.Hidden;
                    picRed.Visibility = Visibility.Hidden;
                    picGreen.Visibility = Visibility.Hidden;
                    lbColTaki.Visibility = Visibility.Hidden;

                }
            }
            for (int i = 0; i < arCardImages.Length; i++)
            {
                if (OpeningCard.Source == arCardImages[i].Source)
                    num = i;
            }
            Card cardi = CheckInArr(num2);
            string str;
            str = cardi.GetCardNumber().ToString();
            if (TurnOfPlayer == "One")
            {
                swSender.WriteLine("-" + str);
                swSender.Flush();
            }
            if (TurnOfPlayer == "Two")
            {
                swSender.WriteLine("-" + str);
                swSender.Flush();
            }
        }

        private void picRed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OpeningCard.Source == arCardImages[14].Source)
            {
                OpeningCard.Source = arCardImages[42].Source;
                picYellow.Visibility = Visibility.Hidden;
                picBlue.Visibility = Visibility.Hidden;
                picRed.Visibility = Visibility.Hidden;
                picGreen.Visibility = Visibility.Hidden;
                lbColTaki.Visibility = Visibility.Hidden;
            }
            else
            {
                if (OpeningCard.Source == arCardImages[15].Source)
                {
                    OpeningCard.Source = arCardImages[37].Source;
                    picYellow.Visibility = Visibility.Hidden;
                    picBlue.Visibility = Visibility.Hidden;
                    picRed.Visibility = Visibility.Hidden;
                    picGreen.Visibility = Visibility.Hidden;
                    lbColTaki.Visibility = Visibility.Hidden;
                }
            }
            for (int i = 0; i < arCardImages.Length; i++)
            {
                if (OpeningCard.Source == arCardImages[i].Source)
                    num = i;
            }
            Card cardi = CheckInArr(num2);
            string str;
            str = cardi.GetCardNumber().ToString();
            if (TurnOfPlayer == "One")
            {
                swSender.WriteLine("-" + str);
                swSender.Flush();
            }
            if (TurnOfPlayer == "Two")
            {
                swSender.WriteLine("-" + str);
                swSender.Flush();
            }
        }

        private void picGreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OpeningCard.Source == arCardImages[14].Source)
            {
                OpeningCard.Source = arCardImages[28].Source;
                picYellow.Visibility = Visibility.Hidden;
                picBlue.Visibility = Visibility.Hidden;
                picRed.Visibility = Visibility.Hidden;
                picGreen.Visibility = Visibility.Hidden;
                lbColTaki.Visibility = Visibility.Hidden;
            }
            else
            {
                if (OpeningCard.Source == arCardImages[15].Source)
                {
                    OpeningCard.Source = arCardImages[21].Source;
                    picYellow.Visibility = Visibility.Hidden;
                    picBlue.Visibility = Visibility.Hidden;
                    picRed.Visibility = Visibility.Hidden;
                    picGreen.Visibility = Visibility.Hidden;
                    lbColTaki.Visibility = Visibility.Hidden;
                }
            }
            for (int i = 0; i < arCardImages.Length; i++)
            {
                if (OpeningCard.Source == arCardImages[i].Source)
                    num = i;
            }
            Card cardi = CheckInArr(num2);
            string str;
            str = cardi.GetCardNumber().ToString();
            if (TurnOfPlayer == "One")
            {
                swSender.WriteLine("-" + str);
                swSender.Flush();
            }
            if (TurnOfPlayer == "Two")
            {
                swSender.WriteLine("-" + str);
                swSender.Flush();
            }
        }
//----------------------------------------------------------------------------------------------
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            stackPanel1.Visibility = Visibility.Visible;
            stackPanel2.Visibility = Visibility.Visible;
            imgAddCard.Visibility = Visibility.Visible;
            lbIpAddr.Visibility = Visibility.Hidden;
            txtIp.Visibility = Visibility.Hidden;
            txtUser.Visibility = Visibility.Hidden;
            btnConnect.Visibility = Visibility.Hidden;
            txtLog.Visibility = Visibility.Hidden;
            txtMessage.Visibility = Visibility.Hidden;
            btnSend.Visibility = Visibility.Hidden;
            lbNick.Visibility = Visibility.Hidden;
            lbPlayer1.Visibility = Visibility.Visible;
            lbPlayer2.Visibility = Visibility.Visible;
            lbTurn.Visibility = Visibility.Visible;
            lbTurn.Content = "שחקן 2 מתחיל";
            // If we are not currently connected but awaiting to connect
            if (Connected == false)
            {
                // Initialize the connection
                InitializeConnection();
            }
            else // We are connected, thus disconnect
            {
                CloseConnection("Disconnected at user's request.");
            }

           
            
        }


        private void InitializeConnection()
        {
            // Parse the IP address from the TextBox into an IPAddress object
            ipAddr = IPAddress.Parse(txtIp.Text);
            // Start a new TCP connections to the chat server
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddr, 1986);

            // Helps us track whether we're connected or not
            Connected = true;
            // Prepare the form
            UserName = txtUser.Text;

            // Disable and enable the appropriate fields
            txtIp.IsEnabled = false;
            txtUser.IsEnabled = false;
            txtMessage.IsEnabled = true;
            btnSend.IsEnabled = true;
            btnConnect.Content = "Disconnect";

            // Send the desired username to the server
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(txtUser.Text);
            swSender.Flush();

            // Start the thread for receiving messages and further communication
            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();
        }

        private void ReceiveMessages()
        {
            int countCard = 0;
            // Receive the response from the server
            srReceiver = new StreamReader(tcpServer.GetStream());
            // If the first character of the response is 1, connection was successful
            string ConResponse = srReceiver.ReadLine();
            // If the first character is a 1, connection was successful
            if (ConResponse[0] == '1')
            {

                // Update the form to tell it we are now connected
                this.Dispatcher.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else // If the first character is not a 1 (probably a 0), the connection was unsuccessful
            {
                string Reason = "Not Connected: ";
                // Extract the reason out of the response message. The reason starts at the 3rd character
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                // Update the form with the reason why we couldn't connect
                this.Dispatcher.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                // Exit the method
                return;
            }
            // While we are successfully connected, read incoming lines from the server
            while (Connected)
            {
                // Show the messages in the log TextBox
                
                    this.Dispatcher.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
                    
                 
            }
        }

        // This method is called from a different thread in order to update the log TextBox
        private void UpdateLog(string strMessage)
        {
           
            // Append text also scrolls the TextBox to the bottom each time
            //if (TurnOfPlayer.Equals("One"))
            //    lbPlayer2.Content = lbNick.Content;
            //else
            //    lbPlayer2.Content = lbNick.Content;
            char ch=strMessage[0];
            if (strMessage != "Connected Successfully!"&&strMessage!="One"&&strMessage!="Two"&&ch!='-')
            {
                 if (strMessage[0] == '1')
                TurnOfPlayer = "One";
            if (strMessage[0] =='2'&&TurnOfPlayer!="One")
                TurnOfPlayer = "Two";
            if (TurnOfPlayer == "One" && strMessage[0] == '1')
            {
                string str = strMessage.Substring(1);
                CreateStackPanel(str);
            }
            if (TurnOfPlayer == "Two" && strMessage[0] == '2')
            {
                string str = strMessage.Substring(1);
                CreateStackPanel(str);
            }
                
            }
            if (ch=='-')
            {
                txtLog.AppendText(strMessage + "\r\n");
                strMessage= strMessage.Remove(0,1);
                int open=Convert.ToInt16(strMessage);
                OpeningCard.Source = arCardImages[open].Source;
            }
        }

        // Closes a current connection
        private void CloseConnection(string Reason)
        {
            // Show the reason why the connection is ending
            txtLog.AppendText(Reason + "\r\n");
            // Enable and disable the appropriate controls on the form
            txtIp.IsEnabled = true;
            txtUser.IsEnabled = true;
            txtMessage.IsEnabled = false;
            btnSend.IsEnabled = false;
            btnConnect.Content = "Connect";

            // Close the objects
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }

        // Sends the message typed in to the server
        private void SendMessage()
        {
            int open = 0;
            for (int i = 0; i < arCardImages.Length; i++)
            {
                if (arCardImages[i].Source == OpeningCard.Source)
                    open = i;
            }

            string openName;
            openName = open.ToString();
            if (txtMessage.Text != "")
            {
              
                swSender.WriteLine(openName);//send the current string
                swSender.Flush();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            btnStartGame.Visibility = Visibility.Hidden;
            btnHelp.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
            imgHelp.Visibility = Visibility.Visible;
            btnReturn.Visibility = Visibility.Visible;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            btnHelp.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
            btnStartGame.Visibility = Visibility.Hidden;
            CreateArrayOfCardsFiles();
            lbIpAddr.Visibility = Visibility.Visible;
            txtUser.Visibility = Visibility.Visible;
            btnConnect.Visibility = Visibility.Visible;        
            txtIp.Visibility = Visibility.Visible;
            lbNick.Visibility = Visibility.Visible;
           

            
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            imgHelp.Visibility = Visibility.Hidden;
            btnReturn.Visibility = Visibility.Hidden;
            btnStartGame.Visibility = Visibility.Visible;
            btnHelp.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

      

    
    }
}
