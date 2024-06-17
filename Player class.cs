using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio.Wave; 

namespace Text_eventyr;

public class Player
{
        public int HP { get; private set; } = 10;
        public Room CurrentRoom { get; set; }
        public bool HasSkeletonDoot { get; set; } = false;
        private string dootSoundFilePath = @"C:\Users\Rikke\Music\doot_sound.mp3";
        public List<Item> Inventory { get; private set; }

        public Player(Room startRoom)
        {
            CurrentRoom = startRoom;
            Inventory = new List<Item>();
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }

        public void Heal(int healAmount)
        {
            HP += healAmount;
        }

        public void Move(string direction)
        {
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                CurrentRoom = CurrentRoom.Exits[direction];
                Console.Clear();
                Console.WriteLine($"Moved to {CurrentRoom.RoomName}");
                if (HasSkeletonDoot)
                {
                    PlayDootSound();
                }
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
        }

        public void AcquireSkeletonDoot(string soundFilePath)
        {
            HasSkeletonDoot = true;
            dootSoundFilePath = soundFilePath;
            Console.WriteLine("You have acquired the Skeleton Doot!");
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Your Inventory:");
            foreach (var item in Inventory)
            {
                Console.WriteLine($"- {item.Description} (ID: {item.ItemId})");
            }
        }

        private void PlayDootSound()
        {
            if (string.IsNullOrEmpty(dootSoundFilePath))
                return;

            try
            {
                using (var audioFile = new AudioFileReader(dootSoundFilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
            System.Diagnostics.Process.Start(@"https://www.youtube.com/watch?v=WTWyosdkx44");
        }
        }
    }



    public class Item
    {
        public int ItemId { get; set; }
        public int HpStat { get; set; }
        public string Description { get; set; }
        

        public Item(int itemId, int hpstat, string description)
        {
            ItemId = itemId;
            HpStat = hpstat;
            Description = description;
            
        }

        public void UseItem(Player player)
        {
            player.Heal(HpStat);
        }
    }



public class ItemList
{
    public List<Item> Items { get; private set; }

    public ItemList()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public Item GetItem(int itemId)
    {
        return Items.Find(item => item.ItemId == itemId);
    }
}