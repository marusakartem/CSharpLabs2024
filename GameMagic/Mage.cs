using System;
using GameMagic.Spells;

namespace GameMagic
{
    public class Mage : Character
    {
        private List<Spell> _spells;


        public Mage(string name, int hp, int coins) : base(name)
        {
            Hp = hp;
            Coins = coins;
            _spells = new List<Spell>();
        }

        public override int SpellsAmount()
        {
            return _spells.Count;
        }

        public override void ShowSpellsInfo()
        {
            Console.WriteLine("Ваші спели:");
            if(_spells.Count > 0)
            {
                for (int i = 0; i < _spells.Count; i++)
                {
                    Console.Write($"{i + 1}. {_spells[i].GetType().Name}: ");
                    if (_spells[i].Damage < 0)
                    {
                        Console.Write($"heal = {-_spells[i].Damage} hp, ");
                    }
                    else
                    {
                        Console.Write($"damage = {_spells[i].Damage} hp, ");
                    }
                    Console.WriteLine($"price = {_spells[i].Price} coins.");
                }
            } else
            {
                Console.WriteLine("Спели відсутні!");
            }

        }

        public override void BuySpell(int spellIndex)
        {
            if(Coins >= Shop.SpellsToBuy[spellIndex].Price)
            {
                _spells.Add(Shop.SpellsToBuy[spellIndex]);
                Coins -= Shop.SpellsToBuy[spellIndex].Price;
            } else
            {
                Console.WriteLine($"Вам недостатньо монет на цей спел. Ви маєте [{Coins}] монет.");
            }
        }

        public override void Attack(IDamagebl damagebl, int spellIndex)
        {
            if (_spells[spellIndex].Damage < 0)
                damagebl = this;

            _spells[spellIndex].CauseDamage(damagebl);
            Mage mage = (Mage)damagebl;
            if (_spells[spellIndex].Damage < 0)
            {
                Console.WriteLine($"[{Name} підлікувався.]");
            }
            else
            {
                Console.WriteLine($"[{Name} вдарив по магу: {mage.Name}]");
            }
            _spells.Remove(_spells[spellIndex]);
        }
    }
}
