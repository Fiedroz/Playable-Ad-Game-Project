using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    [Header("General Settings")]
    public MapType currentMapType;
    public float playerShootInterval;
    public float enemyMobSpawnDelay;
    public int regularMobTowerDmg;
    public int regularMobHP;
    public float regularMobScale;
    public int bigBoiMobTowerDmg;
    public int bigBoiMobHP;
    public float bigBoiMobScale;
    public float enemyMobSpeed;
    public float friendlyMobSpeed;
    public AudioClip loseSound;
    public AudioClip winSound;
    public AudioClip shootSound;


    [Header("Level 1")]
    public int towerHealthLevel1;
    public Vector3 leftWallPositionLevel1;
    public Vector3 rightWallPositionLevel1;
    public int leftWallMultiplierLevel1;
    public int rightWallMultiplierLevel1;
    public float leftWallLenghtLevel1;
    public float rightWallLenghtLevel1;

    [Header("Level 2")]
    public int towerHealthLevel2;
    public Vector3 leftWallPositionLevel2;
    public Vector3 rightWallPositionLevel2;
    public int leftWallMultiplierLevel2;
    public int rightWallMultiplierLevel2;
    public float leftWallLenghtLevel2;
    public float rightWallLenghtLevel2;

    [Header("Maps")]
    public MapData Defalut;
    public MapData MoltenCore;
    public MapData PoisonousJungle;
    public MapData Galaxy;
    public MapData GreenDay;

    public enum MapType {
        Defalut,
        MoltenCore,
        PoisonousJungle,
        Galaxy,
        GreenDay
    }
    [Serializable]
    public class MapData{
        public MapType type;
        public Color landColor;
        public Color skyColor;
        public Color friendlyMobColor;
        public Color enemyMobColor;
        public Color wallFrameColor;
        public Color obstacleColor;
    }
    [Serializable]
    public class MapContainer
    {
        public List<MapData> mapList;
    }
    public void ApplyMap()
    {
        MapData selectedMap = null;

        switch (currentMapType)
        {
            case MapType.Defalut:
                selectedMap = Defalut;
                break;
            case MapType.MoltenCore:
                selectedMap = MoltenCore;
                break;
            case MapType.PoisonousJungle:
                selectedMap = PoisonousJungle;
                break;
            case MapType.Galaxy:
                selectedMap = Galaxy;
                break;
            case MapType.GreenDay:
                selectedMap = GreenDay;
                break;
        }

        if (selectedMap != null)
        {
            Camera.main.backgroundColor= selectedMap.skyColor;

            GameManager.Instance.landAreaColor = selectedMap.landColor;
            GameManager.Instance.friendlyColor= selectedMap.friendlyMobColor;
            GameManager.Instance.enemyColor = selectedMap.enemyMobColor;
            GameManager.Instance.obstacleColor = selectedMap.obstacleColor;
            GameManager.Instance.wallFrameColor = selectedMap.wallFrameColor;
        }
        else
        {
            Debug.LogWarning("No map data available for "+currentMapType.ToString()+", it's sad man ;( ");
        }
    }

}
