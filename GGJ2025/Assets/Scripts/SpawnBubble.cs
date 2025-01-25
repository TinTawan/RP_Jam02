using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubble : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject bubblePrefab;

    KeyPressMinigame kpMinigame;

    GameObject spawnedBubble;

    private void Start()
    {
        kpMinigame = FindObjectOfType<KeyPressMinigame>();

    }

    private void Update()
    {
        if (kpMinigame.GetSpawnBubble())
        {
            spawnedBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity, transform);
            //Debug.Log("Spawned bubble");
            kpMinigame.SetSpawnBubble(false);
        }

        if (kpMinigame.GetPopBubble())
        {
            Destroy(spawnedBubble);
        }
    }

    

}
