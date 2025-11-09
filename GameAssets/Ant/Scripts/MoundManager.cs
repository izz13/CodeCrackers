using UnityEngine;

public class MoundManager : MonoBehaviour
{
    public struct QueenAnt
    {
        public int health;
        public int hunger;
        public int level;

        public QueenAnt(int newHealth, int newHunger, int newLevel)
        {
            this.health = newHealth;
            this.hunger = newHunger;
            this.level = newLevel;
        }
    }

    QueenAnt queenAnt;
    void Start()
    {
        queenAnt = new QueenAnt(100, 0, 1);
    }
}
