using System;
using System.Collections.Generic;

namespace Text_eventyr
{
    public class Room
    {
        public string RoomName { get; set; }
        public int RoomID { get; set; }
        public string Description { get; set; }
        public Dictionary<string, Room> Exits { get; init; }
        public List<Item> Items { get; set; }

        public int dmg { get; set; }

        public Room(string roomName, int roomID, string description, int Dmg)
        {
            RoomName = roomName;
            RoomID = roomID;
            Description = description;
            Exits = new Dictionary<string, Room>();
            Items = new List<Item>();
            dmg = Dmg;
        }

        public void AddExit(string direction, Room room)
        {
            Exits[direction] = room;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }
    }
}
