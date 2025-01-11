using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public Parameters parameters;
    public UIController uiController;
    public PlayerMovement playerMovement;
    public VFXManager vFXManager;
    public SoundManager soundManager;

    public int round = 1;

    public TowerLogic tower;

    public GameObject friendlyPrefab;
    public GameObject enemyPrefab;
    public GameObject obstacle;
    public GameObject obstacleSpawnerLeft;
    public GameObject obstacleSpawnerRight;
    private bool areObstaclesSpawned = false;

    public GameObject landArea;

    public WallLogic leftWall;
    public WallLogic rightWall;
    public List<GameObject> mobsToExplode = new List<GameObject>();

    [HideInInspector]
    public Color enemyColor;
    [HideInInspector]
    public Color obstacleColor;
    [HideInInspector]
    public Color friendlyColor;
    [HideInInspector]
    public Color wallFrameColor;
    [HideInInspector]
    public Color landAreaColor;

    public bool won = false;
    public bool wonFlag = false;
    public bool lost = false;
    public bool lostFlag = false;

    public bool gameOver = false;

    public static GameManager Instance
    {
        get { return _instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        parameters.ApplyMap();
        StartCoroutine(ApplyParameters());
    }
    private IEnumerator ApplyParameters()
    {
        yield return new WaitForSeconds(0.1f);
        landArea.GetComponent<Renderer>().material.color = landAreaColor;

        leftWall.ApplyColors();
        leftWall.ChangeWallLength(parameters.leftWallLenghtLevel1);
        leftWall.SetMultiplier(parameters.leftWallMultiplierLevel1);
        leftWall.SetPosition(parameters.leftWallPositionLevel1);

        rightWall.ApplyColors();
        rightWall.ChangeWallLength(parameters.rightWallLenghtLevel1);
        rightWall.SetMultiplier(parameters.rightWallMultiplierLevel1);
        rightWall.SetPosition(parameters.rightWallPositionLevel1);
    }
    public void Win() 
    {
        if (!wonFlag&&lost == false) {
            won = true;
            gameOver = true;
            soundManager.PlaySound(parameters.winSound);
            uiController.WinScreen(true);
            StartCoroutine(WinCooldown());
            StartCoroutine(ExplodeMobs());
            uiController.FadeIn(1f);
            wonFlag = true;
        }
    }

    public void Lose()
    {
        if (!lostFlag&&won==false)
        {
            lost = true;
            gameOver = true;
            soundManager.PlaySound(parameters.loseSound);
            uiController.DefeatScreen(true);
            StartCoroutine(DefeatCooldown());
            StartCoroutine(ExplodeMobs());
            uiController.FadeIn(2f);
            lostFlag = true;
        }
    }

    private void NewRound()
    {
        won = false;
        wonFlag= false;
        lost = false;
        lostFlag= false;
        gameOver= false;
        tower.ReviveTower();
        uiController.WinScreen(false);
        uiController.FadeOut(1f);
        playerMovement.ResetPowerUp();
        NewRoundParameters();
    }

    private void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator WinCooldown()
    {
        yield return new WaitForSeconds(3f);
        NewRound();
    }

    private IEnumerator DefeatCooldown()
    {
        yield return new WaitForSeconds(3f);
        GameRestart();
    }

    private IEnumerator ExplodeMobs()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i=0;i<mobsToExplode.Count;i++)
        {
            if (mobsToExplode[i])
            {
                yield return new WaitForSeconds(0.05f);

                Destroy(mobsToExplode[i]);
            }
        }
        mobsToExplode.Clear();
    }

    private void NewRoundParameters()
    {
        if (areObstaclesSpawned==false) {
            Vector3 spawnerLeft = obstacleSpawnerLeft.transform.position;
            Vector3 spawnerRight = obstacleSpawnerRight.transform.position;
            Quaternion rotation1 = Quaternion.Euler(0, 90, 0);
            Quaternion rotation2 = Quaternion.Euler(0, 270, 0);
            GameObject obstacleLeft = Instantiate(obstacle, spawnerLeft, rotation1);
            GameObject obstacleRight = Instantiate(obstacle, spawnerRight, rotation2);
            obstacleLeft.GetComponent<Renderer>().material.color = obstacleColor;
            obstacleRight.GetComponent<Renderer>().material.color = obstacleColor;
            areObstaclesSpawned = true;
        }

        leftWall.ChangeWallLength(parameters.leftWallLenghtLevel2);
        leftWall.SetMultiplier(parameters.leftWallMultiplierLevel2);
        leftWall.SetPosition(parameters.leftWallPositionLevel2);

        rightWall.ChangeWallLength(parameters.rightWallLenghtLevel2);
        rightWall.SetMultiplier(parameters.rightWallMultiplierLevel2);
        rightWall.SetPosition(parameters.rightWallPositionLevel2);
    }
}
