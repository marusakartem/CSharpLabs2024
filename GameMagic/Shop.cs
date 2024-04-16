using GameMagic.Spells;
using System;


namespace GameMagic
{
    public class Shop
    {
        private static List<Spell> _spellsToBuy;

        private static int _minimalPrice;

        static Shop()
        {
            _spellsToBuy = new List<Spell>
            {
            new Poison(25, 30),
            new Fireball(40, 60),
            new Heal(-30, 40)
            };

            SpellsToBuy = _spellsToBuy;
            _minimalPrice = 30;
        }
        public static IReadOnlyList<Spell> SpellsToBuy;

        public static int MinimalPrice
        {
            get => _minimalPrice;
        }


        public static void ShowSpellsToBuy()
        {
            Console.WriteLine("\nСпели в магазині:");
            for (int i = 0; i < SpellsToBuy.Count; i++)
            {
                Console.Write($"{i + 1}. {SpellsToBuy[i].GetType().Name}: ");
                if(SpellsToBuy[i].Damage < 0)
                {
                    Console.Write($"heal = {-SpellsToBuy[i].Damage} hp, ");
                } else
                {
                    Console.Write($"damage = {SpellsToBuy[i].Damage} hp, ");
                }
                Console.WriteLine($"price = {SpellsToBuy[i].Price} coins.");
            }
        }
    }
}
