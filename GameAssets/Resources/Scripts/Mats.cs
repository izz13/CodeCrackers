using UnityEngine;

public class Mats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public struct Stone
    {
        public int health;
        public int level;

        public string name;

        public Stone(int newHealth, int newLevel)
        {
            this.health = newHealth;
            this.level = newLevel;
            this.name = "Stone";
        }

    }
    public struct Dirt
    {
        public int health;
        public int level;

        public string name;

        public Dirt(int newHealth, int newLevel)
        {
            this.health = newHealth;
            this.level = newLevel;
            this.name = "Dirt";
        }

    }
    public struct Plants
    {
        public int health;
        public int level;

        public string name;

        public Plants(int newHealth, int newLevel)
        {
            this.health = newHealth;
            this.level = newLevel;
            this.name = "Plant";
        }

    }
    public struct Crystal
    {
        public int health;
        public int level;

        public string name;

        public Crystal(int newHealth, int newLevel)
        {
            this.health = newHealth;
            this.level = newLevel;
            this.name = "Crystal";
        }

    }
    [SerializeField]
    GameObject stonePrefab;
    [SerializeField]
    GameObject dirtPrefab;
    [SerializeField]
    GameObject plantPrefab;
    [SerializeField]
    GameObject crystalPrefab;

}
