
using System;
using System.Collections.Generic;

internal class Program
{
    private static Character player;
    private static List<Item> inventory;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
        inventory = new List<Item>
        {
            new Item("초급 회복포션", "HP 50회복을 시키는 포션", ItemType.Consumable),
            new Item("녹슨 검", "초보자에게 어울리는 검", ItemType.Weapon)
        };
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상점으로 이동");
        Console.WriteLine("2. 상태 보기");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                EnterStore();
                break;
                 
            case 2:
                DisplayMyInfo();
                break;

            case 3:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"레벨.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력: {player.Atk}");
        Console.WriteLine($"방어력: {player.Def}");
        Console.WriteLine($"체력: {player.Hp}");
        Console.WriteLine($"골드: {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유한 아이템을 표시합니다.");
        Console.WriteLine();
        for (int i = 0; i < inventory.Count; i++)
        {
            string equippedMarker = (inventory[i].Equipped) ? "[E] " : "";
            Console.WriteLine($"{equippedMarker}{i + 1}. {inventory[i].Name} - {inventory[i].Description}");
        }
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 장착 관리");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                ManageEquippedItems();
                break;
        }
    }
    static void EnterStore()
    {
        Console.Clear();

        Console.WriteLine("상점에 오신 것을 환영합니다!");
        Console.WriteLine("구매하실 아이템을 선택해주세요:");
        Console.WriteLine();
        Console.WriteLine("1. 하급 체력포션 - 100G");
        Console.WriteLine("2. 하급 마나포션 - 100G");
        Console.WriteLine("3. 철검 - 200G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                BuyItem(new Item("하급 체력포션", "HP 50회복을 시키는 포션", ItemType.Consumable), 100);
                break;

            case 2:
                BuyItem(new Item("하급 마나포션", "MP 50회복을 시키는 포션", ItemType.Consumable), 100);
                break;
            case 3:
                BuyItem(new Item("철검", "무난한 검", ItemType.Weapon), 200);
                break;

        }
    }
    static void BuyItem(Item item, int price)
    {
        if (player.SpendGold(price))
        {
            inventory.Add(item);
            Console.WriteLine($"{item.Name}을(를) 구매하였습니다.");
        }
        else
        {
            Console.WriteLine("골드가 부족합니다.");
        }

        Console.WriteLine("아무 키나 누르세요...");
        Console.ReadKey();
        EnterStore();
    }


    static void ManageEquippedItems()
    {
        Console.Clear();

        Console.WriteLine("장착 관리");
        Console.WriteLine("아이템을 장착 또는 해제합니다.");
        Console.WriteLine();
        for (int i = 0; i < inventory.Count; i++)
        {
            string equippedMarker = (inventory[i].Equipped) ? "[E] " : "";
            Console.WriteLine($"{equippedMarker}{i + 1}. {inventory[i].Name} - {inventory[i].Description}");
        }
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 장착");
        Console.WriteLine("2. 해제");

        int input = CheckValidInput(0, 2);
        if (input == 0)
        {
            DisplayInventory();
        }
        else
        {
            Console.WriteLine("아이템 번호를 입력해주세요:");
            int itemNumber = CheckValidInput(1, inventory.Count) - 1;

            if (input == 1)
            {
                if (!inventory[itemNumber].Equipped)
                {
                    inventory[itemNumber].Equipped = true;
                    Console.WriteLine($"{inventory[itemNumber].Name}을(를) 장착했습니다.");
                }
                else
                {
                    Console.WriteLine("이미 해당 아이템은 장착되어 있습니다.");
                }
            }
            else if (input == 2)
            {
                if (inventory[itemNumber].Equipped)
                {
                    inventory[itemNumber].Equipped = false;
                    Console.WriteLine($"{inventory[itemNumber].Name}의 장착을 해제했습니다.");
                }
                else
                {
                    Console.WriteLine("해당 아이템은 장착되어 있지 않습니다.");
                }
            }

            Console.WriteLine("아무 키나 누르세요...");
            Console.ReadKey();
            ManageEquippedItems();
        }
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; private set; } 

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }

    public void AddGold(int amount)
    {
        if (amount > 0)
        {
            Gold += amount;
        }
    }

    public bool SpendGold(int amount)
    {
        if (amount > 0 && Gold >= amount)
        {
            Gold -= amount;
            return true;
        }
        return false;
    }
}

public class Item
{
    public string Name { get; }
    public string Description { get; }
    public ItemType Type { get; }
    public bool Equipped { get; set; }

    public Item(string name, string description, ItemType type)
    {
        Name = name;
        Description = description;
        Type = type;
        Equipped = false;
    }
}

public enum ItemType
{
    Consumable,
    Weapon,
    Armor
}