using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_6
{
    class LinkedListVector : AbstractVector
    {
        protected class Node
        {
            public Node next;
            public double value;

            public Node()
            {
                next = null;
                value = 0;
            }

            public Node(double value) : this()
            {
                this.value = value;
            }
        }

        Node first;
        int length;

        public LinkedListVector(int length)
        {
            this.length = 0;

            for (int i = 0; i < length; ++i)
                AddNode(new Node());
        }

        public LinkedListVector() : this(5)
        { }

        public override double this[int index]
        {
            get { return FindNode(index).value; }
            set { FindNode(index).value = value; }
        }

        protected void AddNode(Node node)
        {
            if (first == null)
            {
                first = node;
            }
            else
            {
                FindNode(length - 1).next = node;
            }

            ++length;
        }
        public override int Length
        {
            get { return length; }
        }

        

        protected Node FindNode(int index)
        {
            AssertIndex(index, "find node");

            Node cur = first;
            for (int curIndex = 0; cur.next != null && curIndex < index; cur = cur.next, ++curIndex) ;

            return cur;
        }
        public override object Clone()
        {
            Vector linkedListVector = new LinkedListVector(Length);
            for (int i = 0; i < Length; ++i)
            {
                linkedListVector[i] = this[i];
            }

            return linkedListVector;
        }
    }

}