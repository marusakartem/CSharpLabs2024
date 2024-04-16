namespace GameMagic.Spells
{
    public class Heal : Spell
    {
        public Heal(int heal, int price)
        {
            Damage = heal;
            Price = price;
        }
    }
}
