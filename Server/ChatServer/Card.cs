using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer
{
    class Card
    {
        private string color;
        private int cardNumber;
        private string type;
        //private string location;
       // private Bitmap cardImage;


        private string location;//אצל מי זה נמצא פלייר1 פלייר2 או פקט
        public Card(string color, int cardNumber, string location)
        {
            this.color = color;
            this.cardNumber = cardNumber;
            // this.type = type;
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
