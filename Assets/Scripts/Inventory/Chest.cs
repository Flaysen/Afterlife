using System;
using System.Collections.Generic;
using Core;
using InventorySystem;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    enum ChestState
    {
        closed,
        x,
        isOpening,
        opened
    }

    [System.Serializable]
    public struct SpawnPoint
    {
        public Vector3 position;

        public bool Spawned { get; set; }

        public SpawnPoint(Vector3 position, bool spawned)
        {
            this.position = position;
            this.Spawned = spawned;
        }
    }

    public event Action<bool> OnInteractDisplay;
    [SerializeField]  private int dropCount = 3;  
 
    [SerializeField] private float spawningSpeed;

    [SerializeField]  private Collider spawnArea;
   
    [SerializeField] private GameObject itemSpawnerPrefab;
    
    private List<Transform> itemSpawners = new List<Transform>();
   
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    private List<Item> drop = new List<Item>();

    private Animator openAnim;

    private ItemLoot itemLoot;

    private ChestState chestState;

    void Awake()
    {
        openAnim = GetComponent<Animator>();
        openAnim.enabled = false;
        itemLoot = GetComponent<ItemLoot>();
        chestState = ChestState.closed;
    }

    void Start()
    {
        for (int i = 0; i < dropCount; i++)
        {
            GameObject gameObject = Instantiate
                (itemSpawnerPrefab, transform.parent.position, Quaternion.identity);

            itemSpawners.Add(gameObject.transform);
        }
    }
 
    void Update()
    {
        if(chestState == ChestState.isOpening)
        {
            for(int i = 0; i < itemSpawners.Count; i++)
            {
                if(spawnPoints[i].Spawned == false)
                    itemSpawners[i].gameObject.SetActive(true);

                itemSpawners[i].position = Vector3.MoveTowards(itemSpawners[i].position,
                    spawnPoints[i].position, spawningSpeed * Time.deltaTime);

                if(itemSpawners[i].position == spawnPoints[i].position)
                {
                    if (spawnPoints[i].Spawned != true)
                    {
                        Instantiate(drop[i], spawnPoints[i].position, Quaternion.identity);
                        spawnPoints[i] = new SpawnPoint(spawnPoints[i].position, true);
                        itemSpawners[i].gameObject.SetActive(false);
                    }                  
                }
            }

            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                if (spawnPoint.Spawned == true)
                    chestState = ChestState.opened;
                else
                {
                    chestState = ChestState.isOpening;
                    break;
                }
            }
        }
    }

    public void Interact()
    {
        if (chestState == ChestState.closed) 
            OpenChest();
    }

    private void OpenChest()
    {
        chestState = ChestState.x;
        
        openAnim.enabled = openAnim.enabled == false ? true : false;

        for (int i = 0; i < itemSpawners.Count; i++)
        {
             spawnPoints.Add(new SpawnPoint
               (RandomPointInBounds(spawnArea.bounds), false));

            drop.Add(itemLoot.RollLoot());
            //itemSpawners[i].GetComponent<Light>().color = drop[i].GetRarityHaloColor();
        }

        chestState = ChestState.isOpening;
    }

    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void HandleInteractionInfoDisplay(bool isVisible)
    {
        OnInteractDisplay?.Invoke(isVisible);
    }
}





