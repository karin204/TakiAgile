using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_Cards_StackPanel
{
    class Node<T>
    {

        private T info;
        private Node<T> next;

        public Node(T x)
        {
            this.info = x;
            this.next = null;
        }
        public Node(T x, Node<T> next)
        {
            this.info = x;
            this.next = next;
        }
        public Node<T> GetNext()
        {
            return this.next;
        }
        public void SetNext(Node<T> next)
        {
            this.next = next;
        }
        public T GetInfo()
        {
            return this.info;
        }
        public void SetInfo(T x)
        {
            this.info = x;
        }
        public override string ToString()
        {
            return this.info.ToString();
        }
    }
}
