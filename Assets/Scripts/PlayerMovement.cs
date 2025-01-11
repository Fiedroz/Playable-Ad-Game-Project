using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float minX = -3.2f;
    public float maxX = 3.2f;
    public Transform spawnPoint;

    private Camera mainCamera;
    private float shootTimer;

    public float currentPowerUp = 0;
    public float maxPowerUp = 15;

    public RectTransform powerBackgroundUI;
    public RectTransform powerFillUI;
    public Vector3 offset = new Vector3(-50, 50, 0);

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleTouchMovement();
        HandleShooting();
        FollowCannon();
    }

    void HandleTouchMovement()
    {

        if (Input.GetMouseButton(0) && !GameManager.Instance.gameOver)
        {

            Vector3 touchPosition = Input.mousePosition;
            MoveTurret(touchPosition);

        }
    }

    void MoveTurret(Vector3 inputPosition)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, mainCamera.transform.position.z * -1));
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        transform.position = Vector3.Lerp(transform.position, newPosition, movementSpeed * Time.deltaTime);
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;


        if (Input.GetMouseButton(0) && shootTimer >= GameManager.Instance.parameters.playerShootInterval && !GameManager.Instance.gameOver)
        {
            ShootMob();
        }
        if (Input.GetMouseButtonUp(0)&&currentPowerUp==maxPowerUp)
        {
            ActivatePowerUp();
        }

    }

    void ShootMob()
    {
        GameManager.Instance.soundManager.PlaySound(GameManager.Instance.parameters.shootSound);
        GameObject bob = Instantiate(GameManager.Instance.friendlyPrefab, spawnPoint.position, Quaternion.identity);
        if (currentPowerUp<maxPowerUp) {
            currentPowerUp++;
        }
        UpdatePowerBar();
        shootTimer = 0f;
    }

    void ShootBigMob()
    {
        GameManager.Instance.soundManager.PlaySound(GameManager.Instance.parameters.shootSound);
        GameObject bob = Instantiate(GameManager.Instance.friendlyPrefab, spawnPoint.position, Quaternion.identity);
        bob.GetComponent<FriendlyBehavior>().isBig= true;
        shootTimer = 0f;
    }

    private void ActivatePowerUp()
    {
        ShootBigMob();
        currentPowerUp = 0;
        UpdatePowerBar();
    }

    private void FollowCannon()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(this.transform.position);
        powerBackgroundUI.position = screenPosition + offset;
    }

    private void UpdatePowerBar()
    {
        float normalizedPower = Mathf.Clamp01(currentPowerUp / maxPowerUp);
        float newTop = Mathf.Lerp(-100, 0, normalizedPower);
        Vector2 offsetMax = powerFillUI.offsetMax;
        offsetMax.y = newTop;
        powerFillUI.offsetMax = offsetMax;
    }

    public void ResetPowerUp()
    {
        currentPowerUp = 0;
        UpdatePowerBar();
    }
}
