using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverPanel;

    public GameObject grassPrefab;
    public GameObject roadPrefab;

    public int extent;
    public int frontDistance = 10;
    public int backDistance = -5;
    public int maxTerrainRepeat = 3;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>();
    TMP_Text gameOverText;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();

        for (int z = backDistance; z <= 0; z++)
        {
            CreateTerrain(grassPrefab, z);
        }

        for (int z = 1; z <= frontDistance; z++)
        {
            GameObject prefab = GetNextTerrainPrefab(z);
            CreateTerrain(prefab, z);
        }

        player.SetUp(backDistance, extent);
    }

    int lastMaxTravel;
    GameObject randomTerrain;
    TerrainBlock backTerrain;
    private void Update()
    {
        if (player == null) return;
        if (player.isDead && !gameOverPanel.activeInHierarchy) StartCoroutine(ShowGameOverPanel());

        if (player.MaxTravel == lastMaxTravel) return;
        lastMaxTravel = player.MaxTravel;

        randomTerrain = GetNextTerrainPrefab(lastMaxTravel + frontDistance);
        CreateTerrain(randomTerrain, lastMaxTravel + frontDistance);

        backTerrain = map[player.MaxTravel - 1 + backDistance];
        map.Remove(player.MaxTravel - 1 + backDistance);
        Destroy(backTerrain.gameObject);

        player.SetUp(player.MaxTravel + backDistance, extent);
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3f);
        gameOverText.text = "YOUR SCORE : " + player.MaxTravel;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CreateTerrain(GameObject prefab, int zPos)
    {
        GameObject go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);
        TerrainBlock terrainBlock = go.GetComponent<TerrainBlock>();
        terrainBlock.Build(extent);
        map.Add(zPos, terrainBlock);
    }
    
    GameObject GetNextTerrainPrefab(int pos)
    {
        bool isUniform = true;
        var tbRef = map[pos - 1];
        for (int i = 2; i <= maxTerrainRepeat; i++)
        {
            if (map[pos - i].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            if (tbRef is Grass) return roadPrefab;
            
            return grassPrefab;
        }
        return (UnityEngine.Random.value > 0.5f) ? roadPrefab : grassPrefab;
    }
}
