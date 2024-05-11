using System.Drawing;
using System.Text;
using GameMagic.Spells;

namespace GameMagic
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            Console.WriteLine("Правила гри: \n1. Гравець перемагає, якщо у супротивника закінчилось hp.\n" +
                              "2. Якщо немає монет і спелів, то ви в будь-якому випадку пропускаєте хід, отримуючи 30 монет.\n" +
                              "3. За один хід можна: 1р. щось купити в магазині;  1р. використати спел або пропустити хід.\n");
            Character player1 = new Mage("Karl", 100, 100);
            Character player2 = new Mage("Stiv", 100, 100);

            Events.HpChanged += (playerName, newHp) => Console.WriteLine($"Кількість HP гравця {playerName} становить {newHp}");
            Events.CoinsChanged += (playerName, newCoins) => Console.WriteLine($"Кількість монет гравця {playerName} становить {newCoins}");
            Events.SpellApplied += (playerName, spellName) => Console.WriteLine($"Гравець {playerName} застосував спел: {spellName}");
            Events.RoundWon += winnerName => Console.WriteLine($"Гравець {winnerName} переміг у раунді");
            Events.TurnChanged += playerName => Console.WriteLine($"\n***Хід гравця {playerName}***");

            Shop.ShowAvailableSpells();

            while (player1.Hp > 0 && player2.Hp > 0)
            {
                battle(player1, player2);
                battle(player2, player1);
            }

            if (player1.Hp <= 0)
                Events.OnRoundWon(player1.Name);
            else
                Events.OnRoundWon(player2.Name);
        }

        static void battle(Character player1, Character player2)
        {
            if (player1.Hp <= 0 || player2.Hp <= 0) return;

            Events.OnTurnChanged(player1.Name);

            player1.ShowSpellsInfo();

            int shopping = ValidateInput($"\nБажаєте щось придбати в магазині? Ви маєте [{player1.Coins}] монет.\n" +
                "Виберіть варіант відповіді (1 - Так, 2 - Ні)." +
                " Якщо у вас немає спелів, то ви всеодно потрапите в магазин: ",
                       x => x == 1 || x == 2,
                       "Невірний вибір. Будь ласка, виберіть число в діапазоні від 1 до 2.");
            if (shopping == 1 || player1.SpellsAmount() == 0)
            {
                Shop.ShowAvailableSpells();
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
                {
                    int spellIndex = ValidateInput("\nОберіть спел для покупки (Номер): ",
                    x => x >= 1 && x <= 3,
                   "Невірний вибір. Будь ласка, виберіть число в діапазоні від 1 до 3.");
                    player1.BuySpell(spellIndex - 1);
                }

            }

            int choice = ValidateInput("\nВиберіть дію (1 - Атакувати/Підлікуватись, 2 - Пропустити хід): ",
                       x => x == 1 || x == 2,
                       "Невірний вибір. Будь ласка, виберіть число в діапазоні від 1 до 2.");
            if (choice == 1)
            {

                while( player1.SpellsAmount() == 0 )
                {
                    Console.WriteLine("У вас немає спелів! Купіть в магазині.");
                    int _spellIndex = ValidateInput("\nОберіть спел для покупки (Номер): ",
                    x => x >= 1 && x <= 3,
                   "Невірний вибір. Будь ласка, виберіть число в діапазоні від 1 до 3.");
                    player1.BuySpell(_spellIndex - 1);
                }
                player1.ShowSpellsInfo();
                int spellIndex = ValidateInput("\nОберіть спел для атаки чи лікування: ",
                    x => x >= 1 && x <= player1.SpellsAmount(),
                   $"Невірний вибір. Будь ласка, виберіть число в діапазоні від 1 до {player1.SpellsAmount()}.");
                player1.Attack(player2, spellIndex - 1);
            }
            else
            {
                player1.Coins += 30;
                Console.WriteLine($"\nГравець {player1.Name} пропускає хід і отримує 30 монет.");
            }
        }
        static int ValidateInput(string prompt, Func<int, bool> condition, string errorMessage)
        {
            int choice;
            bool isValidInput = false;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    isValidInput = condition(choice);
                }
                else
                {
                    Console.WriteLine("Неправильний ввід. Будь ласка, введіть ціле число.");
                }

                if (!isValidInput)
                {
                    Console.WriteLine(errorMessage);
                }

            } while (!isValidInput);

            return choice;
        }
    }
}