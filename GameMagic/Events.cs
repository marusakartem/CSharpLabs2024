using System;

namespace GameMagic
{
    public static class Events
    {
        public delegate void HpChangedEventHandler(string playerName, int newHp);
        public static event HpChangedEventHandler HpChanged;

        public delegate void CoinsChangedEventHandler(string playerName, int newCoins);
        public static event CoinsChangedEventHandler CoinsChanged;

        public delegate void SpellAppliedEventHandler(string playerName, string spellName);
        public static event SpellAppliedEventHandler SpellApplied;

        public delegate void RoundWonEventHandler(string winnerName);
        public static event RoundWonEventHandler RoundWon;
        
        public delegate void TurnChangedEventHandler(string playerName);
        public static event TurnChangedEventHandler TurnChanged;

        public static void OnHpChanged(string playerName, int newHp)
        {
            HpChanged?.Invoke(playerName, newHp);
        }

        public static void OnCoinsChanged(string playerName, int newCoins)
        {
            CoinsChanged?.Invoke(playerName, newCoins);
        }

        public static void OnSpellApplied(string playerName, string spellName)
        {
            SpellApplied?.Invoke(playerName, spellName);
        }

        public static void OnRoundWon(string winnerName)
        {
            RoundWon?.Invoke(winnerName);
        }

        public static void OnTurnChanged(string playerName)
        {
            TurnChanged?.Invoke(playerName);
        }
    }
}
