using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool playerInRange = false;
    public Animator animator;
    private bool isOpen = false;
    public Transform dropPoint;
    public List<LootData> lootPrefabs;
    public int minLoot = 1;
    public int maxLoot = 3;
    [System.Serializable]
    public class LootData
    {
        public GameObject prefab;
        public int maxAmount;
        [Range(0, 100)] public int dropChance;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && InputManager.InteractPressed && isOpen == false)
        {
            OpenChest();
        }
    }
    private void OpenChest()
    {
        if (isOpen == false)
        {
            Dictionary<GameObject, int> currentLootCount = new Dictionary<GameObject, int>();
            animator.SetBool("isOpen", true);

            int lootCount = Random.Range(minLoot, maxLoot + 1);

            int attempts = 0;
            int maxAttempts = 100;

            int successfulLoots = 0;
            while (successfulLoots < lootCount && attempts < maxAttempts)
            {
                attempts++;

                int index = Random.Range(0, lootPrefabs.Count);
                LootData data = lootPrefabs[index];

                int roll = Random.Range(0, 101);
                if (roll >= data.dropChance)
                    continue;

                if (currentLootCount.ContainsKey(data.prefab) && currentLootCount[data.prefab] >= data.maxAmount)
                    continue;

                GameObject loot = Instantiate(data.prefab, dropPoint.position, Quaternion.identity);
                Vector2 offset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                loot.transform.position += (Vector3)offset;

                loot.GetComponent<Animator>().Play("LootDrop");

                if (!currentLootCount.ContainsKey(data.prefab))
                    currentLootCount[data.prefab] = 0;

                currentLootCount[data.prefab]++;
                successfulLoots++;
            }
            isOpen = true;
        }
    }

}
