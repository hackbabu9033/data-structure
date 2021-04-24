using System;

namespace ch4_link_list
{
    class Program
    {
        static void Main(string[] args)
        {
            #region test int node
            var head = Node<int>.CreateHead(90,Order.Desc);
            head.Insertnode(100);
            head.Insertnode(80);
            head.Insertnode(20);
            head.Insertnode(30);
            head.Insertnode(10);
            head.Insertnode(40);
            head.DeleteNode(80);
            head.ReverseLinkList();
            #endregion
        }
    }
}
