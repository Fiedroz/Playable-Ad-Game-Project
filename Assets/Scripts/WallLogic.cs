using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallLogic : MonoBehaviour
{
    public GameObject wall;
    public int multiplier;

    public TextMeshPro multiplierText;

    public GameObject[] wallsObjects;
    private void Awake()
    {
        multiplierText.text = "X" + multiplier;
    }
    public void Multiply(Transform spawnPoint,bool isFriendly)
    {
        if (!GameManager.Instance.gameOver)
        {
            if (isFriendly) {
                if (spawnPoint.GetComponent<FriendlyBehavior>().passedWall != this) {
                    for (int i = 0; i < multiplier; i++) {
                        if (spawnPoint.GetComponent<FriendlyBehavior>().isBig==false) {
                            GameObject bob = Instantiate(GameManager.Instance.friendlyPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 0.5f), Quaternion.identity);
                            bob.GetComponent<FriendlyBehavior>().passedWall = this;
                            bob.GetComponent<FriendlyBehavior>().isBig = false;
                        }
                        else 
                        {
                            GameObject bob = Instantiate(GameManager.Instance.friendlyPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 0.5f), Quaternion.identity);
                            bob.GetComponent<FriendlyBehavior>().passedWall = this;
                            bob.GetComponent<FriendlyBehavior>().isBig = true;
                        }
                    }
                    Destroy(spawnPoint.gameObject);
                }
            }
            else
            {
                if (spawnPoint.GetComponent<EnemyBehavior>().passedWall != this)
                {
                    for (int i = 0; i < multiplier; i++)
                    {
                        GameObject bob = Instantiate(GameManager.Instance.enemyPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z - 0.5f), Quaternion.identity);
                        bob.GetComponent<EnemyBehavior>().passedWall = this;
                    }
                    Destroy(spawnPoint.gameObject);
                }
            }
        }
    }
    public void ChangeWallLength(float lenght)
    {
        wall.transform.localScale = new Vector3(lenght, wall.transform.localScale.y, wall.transform.localScale.z);
    }

    public void SetMultiplier(int multiplier)
    {
        this.multiplier = multiplier;
    }
    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void ApplyColors()
    {
        for (int i=0;i<wallsObjects.Length;i++)
        {
            wallsObjects[i].GetComponent<Renderer>().material.color = GameManager.Instance.wallFrameColor;
        }
    }
}
