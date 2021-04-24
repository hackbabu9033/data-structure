using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ch4_link_list
{
    public class Node<T>
        where T : IComparable<T>
    {
        public static Node<T> CreateHead(T value,Order order)
        {
            var head = new Node<T>(default(T));
            head.Value = default(T);
            head.Order = order;
            head.Next = new Node<T>(value);
            head.Next.Value = value;
            return head;
        }    

        public Node(T value)
        {
            Value = value;
            Next = null;
        }

        public Order Order { get; set; }

        public T Value { set; get; }

        public Node<T> Next { set; get; }

        public Node<T> AddToFront(T item)
        {
            var frontier = new Node<T>(item);
            frontier.Next = this;
            return frontier;
        }
        public Node<T> AppendToEnd(T item)
        {
            if (this.Next != null)
            {
                return this.Next.AppendToEnd(item);
            }
            else
            {
                this.Next = new Node<T>(item);
                return this.Next;
            }
        }

        public void DeleteNode(T value)
        {
            var pre = this;
            var cur = this.Next;
            while (cur != null && cur.Value.CompareTo(value) != 0)
            {
                pre = cur;
                cur = cur.Next;
            }
            if (cur == null)
            {
                throw new Exception($"unable find {value} in LinkList");
            }
            pre.Next = cur.Next;
            cur = null;
        }

        public void Insertnode(T item)
        {
            var pre = this;
            var cur = this.Next;
            // todo：依照head的order判斷要怎麼insert
            while (cur != null && cur.Value.CompareTo(item) > 0)
            {
                pre = cur;
                cur = cur.Next;
            }
            var node = new Node<T>(item);
            node.Next = cur;
            pre.Next = node;
        }

        public void ReverseLinkList()
        {
            var head = this;
            var pre = head;
            var listNode = head.Next;
            var cur = head.Next;
            head.Order = (head.Order == Order.Asc) ? Order.Desc : Order.Asc;
            while (listNode != null)
            {
                pre = cur;
                cur = listNode;
                listNode = listNode.Next;
                cur.Next = pre;
            }
            head.Next = cur;
        }

    }
}
