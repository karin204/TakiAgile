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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Animation;

namespace Wpf_Cards_StackPanel
{
    class Card
    {
       
            private string color;
            private int cardNumber;
            private string type;
            private BitmapImage cardImage;
            

            private string location;//whom turn it is
            public Card(string color, int cardNumber, string location)
            {
                this.color = color;
                this.cardNumber = cardNumber;
                this.location = location;
            }


            public string GetColor()
            {
                return this.color;
            }

            public int GetCardNumber()
            {
                return this.cardNumber;
            }

            public void SetLocation(string location)
            {
                this.location = location;
            }
            public string GetLocation()
            {
                return this.location;
            }


           
          

          
        


    }
}
