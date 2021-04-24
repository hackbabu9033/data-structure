using System;
using System.Collections.Generic;
using System.Text;

namespace ch3_queue_and_stack.tree
{
    public static class TreeNodeHelper
    {
        public static int CaculateAllGroupWithChildsMissionContentCount(GroupHierarchy node)
        {
            node.CurAndChildrenMissionContentsCount = node.MissionContents.Count;
            if (node.Children != null && node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    node.CurAndChildrenMissionContentsCount += CaculateAllGroupWithChildsMissionContentCount(child);
                }
            }
            else
            {
                return node.CurAndChildrenMissionContentsCount;
            }
            return node.CurAndChildrenMissionContentsCount;
        }
    }
}
