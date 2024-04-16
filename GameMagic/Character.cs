﻿
using GameMagic.Spells;

namespace GameMagic
{
    public abstract class Character : IDamagebl
    {
        private int _hp;
        private string _name;
        private int _coins;

        protected Character(string name)
        {
            _name = name;
        }

        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                Console.WriteLine($"[ Гравець {Name} має [{Coins}] монет ]");
            }
        }

        public int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                Console.WriteLine($"[ Гравець {Name} має [{Hp}] hp ]");
            }
        }
        public string Name
        {
            get => _name;
            protected set => _name = value;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if ( damage < 0)
            {
                Console.WriteLine($"Живем, живем! Кричить {Name}");
            } else
            {
                Console.WriteLine($"Ай, больно в нозі! Кричить {Name}");
            }
        }

        public abstract void Attack(IDamagebl damagebl, int spellIndex);
        public abstract void BuySpell(int spellIndex);
        public abstract void ShowSpellsInfo();
        public abstract int SpellsAmount();
    }
}