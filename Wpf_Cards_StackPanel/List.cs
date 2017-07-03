using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_Cards_StackPanel
{
    class List<T>
    {
        Node<T> first;

        public List()
        {
            this.first = null;
        }
        public bool IsEmpty()
        {
            if (this.first == null)
                return true;
            else
                return false;
        }
        public Node<T> GetFirst()
        {
            return this.first;
        }
        public Node<T> Insert(Node<T> pos, T x)
        {
            Node<T> temp = new Node<T>(x);
            if (pos == null)
            {
                temp.SetNext(this.first);
                this.first = temp;
            }
            else
            {
                temp.SetNext(pos.GetNext());
                pos.SetNext(temp);
            }
            return temp;
        }
        public Node<T> Remove(Node<T> pos)
        {
            if (this.first == pos)
                this.first = pos.GetNext();
            else
            {
                Node<T> prevPos = this.GetFirst();
                while (prevPos.GetNext() != pos)
                    prevPos = prevPos.GetNext();
                prevPos.SetNext(pos.GetNext());
            }
            Node<T> nextPos = pos.GetNext();
            pos.SetNext(null);

            return nextPos;
        }
        public override string ToString()
        {
            string str = "[";
            Node<T> pos = this.first;
            while (pos != null)
            {
                str = str + pos.GetInfo().ToString();
                if (pos.GetNext() != null)
                    str = str + ",";
                pos = pos.GetNext();
            }
            str = str + "]";
            return str;
        }
    }
}
