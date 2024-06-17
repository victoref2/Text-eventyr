using System;

namespace Text_eventyr
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display the title screen
            DisplayTitleScreen();

            // Clear the screen after the player presses Enter
            Console.Clear();

            // Initialize rooms
            Room startRoom = new Room("Murky Swamp Entrance", 1, "You stand at the entrance of a murky swamp. The air is thick with fog, and you can barely see through the dense, thorny bushes.", 0);
            Room denseThicket = new Room("Dense Thicket", 2, "You push your way through the thick, thorny bushes. It's hard to see, and the thorns tear at your clothes.", 5);
            Room hiddenClearing = new Room("Hidden Clearing", 3, "You find a hidden clearing. The fog lifts slightly, revealing a more open space.", 0);
            Room abandonedHut = new Room("Abandoned Hut", 4, "You come across an old, abandoned hut. The door creaks as you push it open but sadly you get a splinter. Inside, there's a dusty closet.", 1);
            Room swampHeart = new Room("Swamp Heart", 5, "You've reached the heart of the swamp. The atmosphere is eerie, and you feel a chill down your spine.", 0);
            Room mossyCave = new Room("Mossy Cave", 6, "A cave covered in moss and dripping water. It feels damp and cold.", 1);
            Room foggyPath = new Room("Foggy Path", 7, "A path shrouded in dense fog. You can barely see a few feet ahead.", 0);
            Room oldTree = new Room("Old Tree", 8, "An enormous, ancient tree. Its twisted roots form a maze on the ground.", 0);
            Room darkPond = new Room("Dark Pond", 9, "A dark, still pond. The water looks almost black, and you can't see the bottom. You decide to take a sip.", 4);
            Room fallenLog = new Room("Fallen Log", 10, "A giant fallen log blocks your path. You can crawl under it or climb over. But when you get to the other side, you slip in the mud and hit your head.", 4);
            Room thornyTunnel = new Room("Thorny Tunnel", 11, "A tunnel formed by thorny bushes. It's narrow and difficult to pass through. Take 5 Damage.", 5);
            Room murkyStream = new Room("Murky Stream", 12, "A slow-moving, murky stream. The water is thick and smells unpleasant.", 0);
            Room overgrownRuins = new Room("Overgrown Ruins", 13, "The ruins of an ancient structure, now overgrown with vines and moss.", 0);
            Room whisperingGlade = new Room("Whispering Glade", 14, "A glade where the wind seems to whisper through the leaves.", 0);
            Room twistedRoots = new Room("Twisted Roots", 15, "A dense network of twisted roots. It's easy to trip if you're not careful, and you fell 4 damage.", 4);
            Room exitRoom = new Room("EXIT", 16, "You see a bright light ahead, signaling the end of your journey. You can only leave if you have exactly 1 HP.", 0);

            // Set room exits
            startRoom.AddExit("north", denseThicket);
            denseThicket.AddExit("south", startRoom);
            denseThicket.AddExit("north", hiddenClearing);
            hiddenClearing.AddExit("south", denseThicket);
            hiddenClearing.AddExit("east", abandonedHut);
            hiddenClearing.AddExit("west", swampHeart);
            abandonedHut.AddExit("west", hiddenClearing);
            swampHeart.AddExit("east", hiddenClearing);
            swampHeart.AddExit("north", mossyCave);
            mossyCave.AddExit("south", swampHeart);
            mossyCave.AddExit("east", foggyPath);
            foggyPath.AddExit("west", mossyCave);
            foggyPath.AddExit("north", oldTree);
            oldTree.AddExit("south", foggyPath);
            oldTree.AddExit("east", darkPond);
            darkPond.AddExit("west", oldTree);
            darkPond.AddExit("north", fallenLog);
            fallenLog.AddExit("south", darkPond);
            fallenLog.AddExit("east", thornyTunnel);
            thornyTunnel.AddExit("west", fallenLog);
            thornyTunnel.AddExit("north", murkyStream);
            murkyStream.AddExit("south", thornyTunnel);
            murkyStream.AddExit("east", overgrownRuins);
            overgrownRuins.AddExit("west", murkyStream);
            overgrownRuins.AddExit("north", whisperingGlade);
            whisperingGlade.AddExit("south", overgrownRuins);
            whisperingGlade.AddExit("east", twistedRoots);
            twistedRoots.AddExit("west", whisperingGlade);
            twistedRoots.AddExit("north", exitRoom);
            exitRoom.AddExit("south", twistedRoots);

            // Initialize player
            Player player = new Player(startRoom);

            // File path to the DOOT sound
            string dootSoundFilePath = @"C:\Users\Rikke\Music\doot_sound.mp3";

            // Add items to rooms
            Item skeletonDoot = new Item(1, 0, "A dusty closet containing a Skeleton Doot item.");
            abandonedHut.AddItem(skeletonDoot);

            Item healthPotion = new Item(2, 5, "A potion that heals you.");
            denseThicket.AddItem(healthPotion);

            Item magicAmulet = new Item(3, 1, "A magic amulet that slightly increases your HP.");
            swampHeart.AddItem(magicAmulet);

            Item thornyBranch = new Item(4, 2, "A thorny branch you found on the ground. It might be useful.");
            startRoom.AddItem(thornyBranch);

            Item glowingStone = new Item(5, 3, "A glowing stone that emits a faint light.");
            mossyCave.AddItem(glowingStone);

            Item mysteriousScroll = new Item(6, 2, "A scroll with mysterious writings. It seems valuable.");
            overgrownRuins.AddItem(mysteriousScroll);

            Item enchantedLeaf = new Item(7, 2, "An enchanted leaf that shimmers in the light.");
            whisperingGlade.AddItem(enchantedLeaf);

            // Main game loop
            while (player.HP > 0)
            {
                Console.Clear();
                Console.WriteLine($"You are in {player.CurrentRoom.RoomName}. HP: {player.HP}");
                Console.WriteLine(player.CurrentRoom.Description);

                Console.WriteLine("Available exits:");
                foreach (var exit in player.CurrentRoom.Exits)
                {
                    Console.WriteLine($"- {exit.Key}");
                }

                Console.WriteLine("Items in the room:");
                foreach (var item in player.CurrentRoom.Items)
                {
                    Console.WriteLine($"- {item.Description} (ID: {item.ItemId})");
                }

                player.DisplayInventory();

                Console.WriteLine("Enter command (move [direction], pickup [itemId], use [itemId], or exit):");
                string command = Console.ReadLine();
                if (command.StartsWith("move "))
                {
                    string direction = command.Substring(5);
                    if (player.CurrentRoom.Exits[direction] == exitRoom && player.HP != 1)
                    {
                        Console.WriteLine("You need to have exactly 1 HP to exit. Game over.");
                        break;
                    }

                    player.Move(direction);
                    player.TakeDamage(player.CurrentRoom.dmg);
                }
                else if (command.StartsWith("pickup "))
                {
                    int itemId;
                    if (int.TryParse(command.Substring(7), out itemId))
                    {
                        if (itemId == 1)
                        {
                            player.AcquireSkeletonDoot(dootSoundFilePath);
                        }
                        Item item = player.CurrentRoom.Items.Find(i => i.ItemId == itemId);
                        if (item != null)
                        {
                            player.AddItemToInventory(item);
                            player.CurrentRoom.Items.Remove(item);
                            Console.WriteLine($"Picked up {item.Description}.");
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid item ID.");
                    }
                }
                else if (command.StartsWith("use "))
                {
                    int itemId;
                    if (int.TryParse(command.Substring(4), out itemId))
                    {
                        Item item = player.Inventory.Find(i => i.ItemId == itemId);
                        if (item != null)
                        {
                            item.UseItem(player);
                            player.Inventory.Remove(item);
                            Console.WriteLine($"Used {item.Description}.");
                        }
                        else
                        {
                            Console.WriteLine("Item not found in inventory.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid item ID.");
                    }
                }
                else if (command == "exit")
                {
                    break;
                }
            }

            if (player.HP == 1)
            {
                Console.WriteLine("Congratulations! You have managed to escape the swamp with exactly 1 HP!");
            }
            else if (player.HP > 0)
            {
                Console.WriteLine("Game over. You failed to escape the swamp with exactly 1 HP.");
            }
            else
            {
                Console.WriteLine("You died in the swamp. Game over.");
            }
        }

        static void DisplayTitleScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*    Welcome to The Last EGO:       *");
                Console.WriteLine("*        The Breaking Point         *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*************************************");
                Console.WriteLine("\nPress Enter to start the game...");

                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
    }
}
