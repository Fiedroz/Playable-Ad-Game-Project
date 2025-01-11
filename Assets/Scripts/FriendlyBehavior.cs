using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBehavior : MonoBehaviour
{
    private Transform tower;

    public float switchDistance;
    public bool movingToTower = false;

    public WallLogic passedWall;
    public SkinnedMeshRenderer skinnedRenderer;

    public int hp = 2;
    public int dmg = 1;
    private bool canTakeDamage = true;

    private bool addToExplode = false;

    public bool isBig = false;

    private void Start()
    {
        tower = GameManager.Instance.tower.transform;
        skinnedRenderer.material.color = GameManager.Instance.friendlyColor;
        if (isBig) {
            hp = GameManager.Instance.parameters.bigBoiMobHP;
            dmg = GameManager.Instance.parameters.bigBoiMobTowerDmg;
            this.transform.localScale = new Vector3(GameManager.Instance.parameters.bigBoiMobScale, GameManager.Instance.parameters.bigBoiMobScale, GameManager.Instance.parameters.bigBoiMobScale);
        }
        else
        {
            hp = GameManager.Instance.parameters.regularMobHP;
            dmg = GameManager.Instance.parameters.regularMobTowerDmg;
            this.transform.localScale = new Vector3(GameManager.Instance.parameters.regularMobScale, GameManager.Instance.parameters.regularMobScale, GameManager.Instance.parameters.regularMobScale);
        }
    }

    void Update()
    {
        if (!movingToTower)
        {
            MoveTowards();
        }else if (movingToTower && tower != null)
        {
            MoveTowards(tower.position);
        }
        AddToExplode();
    }

    void MoveTowards(Vector3 target = new Vector3())
    {
        if (!GameManager.Instance.gameOver)
        {
            if (target == Vector3.zero)
            {
                Vector3 direction = new Vector3(0, 0, 1);
                transform.Translate(direction * GameManager.Instance.parameters.friendlyMobSpeed * Time.deltaTime, Space.World);
                transform.LookAt(new Vector3(tower.position.x, transform.position.y, tower.position.z));

            }
            else
            {
                Vector3 direction = (target - transform.position).normalized;
                transform.Translate(direction * GameManager.Instance.parameters.friendlyMobSpeed * Time.deltaTime, Space.World);
                transform.LookAt(new Vector3(tower.position.x, transform.position.y, tower.position.z));
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            if (canTakeDamage)
            {
                EnemyBehavior otherMob = collision.gameObject.GetComponent<EnemyBehavior>();
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
        hp -= dmg;

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
            addToExplode= true;
        }
    }
}
