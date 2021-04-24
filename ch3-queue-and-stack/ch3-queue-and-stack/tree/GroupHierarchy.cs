using System;
using System.Collections.Generic;
using System.Text;

namespace ch3_queue_and_stack.tree
{
    public class GroupHierarchy
    {
        public string GroupName { get; set; }
        public List<GroupHierarchy> Children { get; set; } = new List<GroupHierarchy>();

        public List<MissionContent> MissionContents { get; set; } = new List<MissionContent>();

        public int CurAndChildrenMissionContentsCount { get; set; }
    }

    public class MissionContent
    {
        public string Name { get; set; }
    }

}
