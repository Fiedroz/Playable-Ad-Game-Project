using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    [HideInInspector]
    public int towerHP;
    public TextMeshPro healthText;

    public GameObject directionWall;

    private bool isTowerAlive = true;

    private int mobsPerWave = 20;

    private IEnumerator wavesCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        towerHP = GameManager.Instance.parameters.towerHealthLevel1;
        healthText.text = towerHP.ToString();
        wavesCoroutine = SpawnWaves();
        StartCoroutine(wavesCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Friendly")
        {
            Destroy(other.gameObject);
            if (towerHP==0)
            {
                if (isTowerAlive) {
                    GameManager.Instance.Win();
                    StopCoroutine(wavesCoroutine);
                    isTowerAlive = false;
                }
            }
            else
            {
                towerHP -= 1;
                healthText.text=towerHP.ToString();
            }
            
        }
    }
    
    private IEnumerator SpawnWaves()
    {
        while (isTowerAlive)
        {
            yield return new WaitForSeconds(GameManager.Instance.parameters.enemyMobSpawnDelay);
            for (int i=0;i<mobsPerWave;i++)
            {
                SpawnEnemy();
            }

        }
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(-3f,3f);

        Vector3 spawnPosition = transform.position;
        spawnPosition.z -= 2f;
        spawnPosition.x += randomX;


        if (isTowerAlive == true) {
            Instantiate(GameManager.Instance.enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void ReviveTower()
    {
        isTowerAlive = true;
        towerHP = GameManager.Instance.parameters.towerHealthLevel2;
        healthText.text = towerHP.ToString();
        wavesCoroutine = SpawnWaves();
        StartCoroutine(wavesCoroutine);
    }
}
