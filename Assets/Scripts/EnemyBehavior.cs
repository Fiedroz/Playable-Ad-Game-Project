using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int hp = 2;
    private bool canTakeDamage = true;
    public WallLogic passedWall;
    public SkinnedMeshRenderer skinnedRenderer;

    private bool addToExplode = false;

    void Start()
    {
        skinnedRenderer.material.color = GameManager.Instance.enemyColor;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        AddToExplode(); 
    }

    private void MoveForward()
    {
        if (!GameManager.Instance.gameOver) {
            Vector3 direction = new Vector3(0, 0, -1);
            transform.Translate(direction * GameManager.Instance.parameters.enemyMobSpeed* Time.deltaTime, Space.World);
            transform.LookAt(new Vector3(0, 0, -50));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Friendly")
        {
            if (canTakeDamage)
            {
                FriendlyBehavior otherMob = collision.gameObject.GetComponent<FriendlyBehavior>();
                if (otherMob != null)
                {
                    TakeDamage();
                    otherMob.TakeDamage();
                }
            }
        }
    }

    public void TakeDamage()
    {
        hp -= 1;

        if (hp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    private void AddToExplode()
    {
        if (!addToExplode && GameManager.Instance.gameOver)
        {
            GameManager.Instance.mobsToExplode.Add(gameObject);
            addToExplode = true;
        }
    }
}
