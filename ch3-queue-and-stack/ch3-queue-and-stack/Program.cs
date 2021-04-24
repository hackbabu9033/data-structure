using ch3_queue_and_stack.tree;
using System;
using System.Collections.Generic;

namespace ch3_queue_and_stack
{
    class Program
    {
        static void Main(string[] args)
        {
            #region circleQueue exercise
            //var intCircleQueue = new CircleQueue<int>(5);

            //intCircleQueue.Enqueue(1);
            //intCircleQueue.Dequeue();
            //intCircleQueue.Enqueue(2);
            //intCircleQueue.Enqueue(3);
            //intCircleQueue.Enqueue(4);
            //intCircleQueue.Enqueue(5);
            //intCircleQueue.Dequeue();
            //intCircleQueue.Dequeue();
            //intCircleQueue.Dequeue();
            //intCircleQueue.Dequeue();
            //intCircleQueue.Enqueue(10);
            //intCircleQueue.Enqueue(8);
            #endregion

            #region covert collection to complete binaryTree
            //var arrayList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //var trees = BinaryTree<int>.CreateBinaryTree(arrayList);
            ////BinaryTree<int>.PrintTreeNodes(trees);
            //#endregion

            //#region heapify to max-heap
            //var datas = new List<int>() { 1, 5, 8, 3, 4, 5, 6, 7 };
            //var maxHeap = new Heap(datas, HeapType.Max);
            //var minHeap = new Heap(datas, HeapType.Min);
            //BinaryTree<int>.PrintTreeNodes(maxHeap.Tree);
            //maxHeap.Insert(2);
            //maxHeap.Delete(5);
            //var min = minHeap.Peek();
            #endregion

            #region GroupHierarchy test
            #region 3th groups
            var thirdGroup1 = new List<GroupHierarchy>();
            thirdGroup1.Add(new GroupHierarchy()
            {
                GroupName = "OA",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "test1" },
                                                                  new MissionContent() { Name = "test2" } }
            }
            );
            thirdGroup1.Add(new GroupHierarchy()
            {
                GroupName = "QA123",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "test50" },
                                                                      new MissionContent() { Name = "test51" } }
            }
            );
            thirdGroup1.Add(new GroupHierarchy()
            {
                GroupName = "Hank23",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "test99" },
                                                                      new MissionContent() { Name = "test100" } }
            }
            );
            var thirdGroup2 = new List<GroupHierarchy>();
            
            thirdGroup2.Add(new GroupHierarchy()
            {
                GroupName = "asqw",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "test50" },
                                                                      new MissionContent() { Name = "test51" } }
            }
            );
            thirdGroup2.Add(new GroupHierarchy()
            {
                GroupName = "aaaple",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "test99" },
                                                                      new MissionContent() { Name = "test100" } }
            }
            );
            var thirdGroup3 = new List<GroupHierarchy>();
            thirdGroup3.Add(new GroupHierarchy()
            {
                GroupName = "aaaple",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "test99" },
                                                                      new MissionContent() { Name = "test100" } }
            }
            );
            #endregion
            #region 2th groups
            var twiceGroup1 = new GroupHierarchy() 
            {
                GroupName = "poe3.12",
                MissionContents = new List<MissionContent>(){ new MissionContent() { Name = "deadEye" }},
                Children = thirdGroup1
            };
            var twiceGroup2 = new GroupHierarchy()
            {
                GroupName = "poe3.13",
                MissionContents = new List<MissionContent>() { new MissionContent() { Name = "elementist" } },
                Children = thirdGroup2
            };
            #endregion

            var topGroup1 = new GroupHierarchy()
            {
                GroupName = "poe3.14",
                MissionContents = new List<MissionContent>() { new MissionContent() { Name = "sion" } }
            };
            topGroup1.Children.Add(twiceGroup1);
            topGroup1.Children.Add(twiceGroup2);
            topGroup1.Children.Add(twiceGroup2);
            TreeNodeHelper.CaculateAllGroupWithChildsMissionContentCount(topGroup1);
            #endregion
        }
    }
}
