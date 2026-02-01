
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class FoodResource : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public struct BerryBush
    {
        public int health;
        public int hunger;
        public int level;

        public string name;

        public BerryBush(int newHealth, int newHunger, int newLevel)
        {
            this.health = newHealth;
            this.hunger = newHunger;
            this.level = newLevel;
            this.name = "Berries";
        }

    }
    public struct Carrot
    {
        public int health;
        public int hunger;
        public int level;

        public string name;

        public Carrot(int newHealth, int newHunger, int newLevel)
        {
            this.health = newHealth;
            this.hunger = newHunger;
            this.level = newLevel;
            this.name = "Carrot";
        }

    }
    [SerializeField]
    GameObject berryBushPrefab;
    [SerializeField]
    GameObject carrotPrefab;

    public void Start()
    {
        
        
        for(int i=0; i<10; i++) {
            int bobbert = Random.Range(0, 2);
            if(bobbert==0){
                Vector3 position = new Vector3(Random.Range(0, 50), 1, Random.Range(0, 50));
                GameObject berry = Instantiate(berryBushPrefab, this.transform);
                berry.transform.position = position;
                berry.layer=LayerMask.NameToLayer("Food");
                berry.AddComponent<BoxCollider>();

            }
            if(bobbert==1){
                Vector3 carrotPOS = new Vector3(Random.Range(0, 50), 1, Random.Range(0, 50));
                GameObject carrot = Instantiate(carrotPrefab, this.transform);
                carrot.transform.position = carrotPOS;
                carrot.layer=LayerMask.NameToLayer("Food");
                carrot.AddComponent<BoxCollider>();
            }
        }
        
        

    }
//     Steps:
// Have the resources spawn as children to the FoodResource or MatsResource game object
// They already have the script that will hold the spawning code to spawn  the resources, so just parent the preface with this.transform in the instantiate method
// Use random.range to spawn the resource at a random location on the ground
// In order to spawn the resource you need to have access to it with a field that will hold the prefab
}
