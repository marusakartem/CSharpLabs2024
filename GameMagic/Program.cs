using System.Text;

namespace GameMagic
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            Console.WriteLine("Правила гри: " +
                "\n1. Гравець програє, якщо у нього закінчилось hp." +
                "\n2. За один хід можна 1 раз купити спел в магазині." +
                "\n3. Під час ходу є вибір: використати 1 спел або пропустити хід, отримуючи 30 монет.\n");

            Character player1 = new Mage("Garik", 100, 150);
            Character player2 = new Mage("Vasya", 100, 150);

            Shop.ShowSpellsToBuy();

            while (player1.Hp > 0 && player2.Hp > 0)
            {
                Battle(player1, player2);
                Battle(player2, player1);
            }

            if (player1.Hp <= 0)
                Console.WriteLine($"***** Гравець {player2.Name} переміг! *****");
            else
                Console.WriteLine($"***** Гравець {player1.Name} переміг! *****");
        }

        static void Battle(Character player1, Character player2)
        {
            if (player1.Hp <= 0 || player2.Hp <= 0) return;

            Console.WriteLine($"\n***** Хід гравця {player1.Name} *****");

            player1.ShowSpellsInfo();

            bool wantsToShop = PromptToShop(player1);
            if (wantsToShop || player1.SpellsAmount() == 0)
            {
                Shop.ShowSpellsToBuy();
                if (player1.Coins < Shop.MinimalPrice)
                {
                    Console.WriteLine($"\nМагазин недоступний! У вас недостатньо монет [{player1.Coins}].");
                    if (player1.SpellsAmount() == 0)
                    {
                        Console.WriteLine($"\nУ вас недостатньо монет та немає спелів!\n" +
                            $"Гравець {player1.Name} пропускає хід і отримує 30 монет.");
                        player1.Coins += 30;
                        return;
                    }
                }
                else
                    ShowAndBuySpell(player1);
            }

            int choice = GetValidChoice("\nВиберіть дію (1 - Атакувати/Підлікуватись, 2 - Пропустити хід): ",
                       x => x == 1 || x == 2, 2);

            if (choice == 1)
                PerformAttack(player1, player2);
            else
            {
                player1.Coins += 30;
                Console.WriteLine($"\nГравець {player1.Name} пропускає хід і отримує 30 монет.");
            }
        }

        static bool PromptToShop(Character player)
        {
            int choice = GetValidChoice($"\nБажаєте щось придбати в магазині? Ви маєте [{player.Coins}] монет.\n" +
                "Виберіть варіант відповіді (1 - Так, 2 - Ні)." +
                " При відсутності спелів ви однаково потрапите в магазин: ", x => x == 1 || x == 2, 2);
            return choice == 1;
        }

        static void ShowAndBuySpell(Character player)
        {
            int spellIndex = GetValidChoice("\nОберіть спел для покупки (Номер): ",
                    x => x >= 1 && x <= 3, Shop.SpellsToBuy.Count);
            player.BuySpell(spellIndex - 1);
        }

        static void PerformAttack(Character player1, Character player2)
        {
            while (player1.SpellsAmount() == 0)
            {
                Console.WriteLine("У вас немає спелів! Купіть в магазині.");
                ShowAndBuySpell(player1);
            }

            player1.ShowSpellsInfo();
            int spellIndex = GetValidChoice("\nОберіть спел для атаки чи лікування: ",
                x => x >= 1 && x <= player1.SpellsAmount(), player1.SpellsAmount());
            player1.Attack(player2, spellIndex - 1);
        }

        static int GetValidChoice(string prompt, Func<int, bool> condition, int extrValue)
        {
            string errorMessage = $"Невірний вибір. Будь ласка, виберіть число в діапазоні від 1 до {extrValue}.";
            int choice;
            bool isValidInput = false;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                    isValidInput = condition(choice);
                else
                    Console.WriteLine("Неправильний ввід. Будь ласка, введіть ціле число.");

                if (!isValidInput)
                    Console.WriteLine(errorMessage);

            } while (!isValidInput);

            return choice;
        }
    }
}

