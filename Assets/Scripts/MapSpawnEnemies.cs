using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnEnemies : MonoBehaviour
{
    private void Start()
    {
        spawnEnemies();
        GameManager.Instance.InterMan.InPawnManagment = true;
        GameManager.Instance.CanvasCon.setPawnManageActive(true);
    }

    private void spawnEnemy(int enemyType, int xPos, int yPos)
    {
        GameObject enemyToSpawn = enemyType > 0 ? enemyType > 1 ? GameManager.Instance.GameDesignScriptObj.ScoutEnemyPrefab : GameManager.Instance.GameDesignScriptObj.KnightEnemyPrefab : GameManager.Instance.GameDesignScriptObj.ArcherEnemyPrefab;
        GameManager.Instance.InterMan.addPawn(Instantiate(enemyToSpawn, new Vector3(xPos, yPos, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f)).GetComponent<PawnBehaviour>(), false);
    }
    private void spawnEnemies()
    {
        if (GameManager.Instance.GameDesignScriptObj.MaxPawn > 8)
        {
            Debug.Log("ERROR: Cannot spawn more that 8 Pawn");
            return;
        }
        List<int> xPoss = new List<int>();
        List<int> yPoss = new List<int>();
        for (int i = 1; i <= GameManager.Instance.GameDesignScriptObj.EnemySpawnMaxX; i++)
        {
            xPoss.Add(i);
        }
        for (int i = -GameManager.Instance.GameDesignScriptObj.EnemySpawnY; i <= GameManager.Instance.GameDesignScriptObj.EnemySpawnY; i++)
        {
            yPoss.Add(i);
        }
        for (int i = 0; i < xPoss.Count; i++)
        {
            int temp = xPoss[i];
            int ran = Random.Range(0, xPoss.Count);
            xPoss[i] = xPoss[ran];
            xPoss[ran] = temp;

        }
        for (int i = 0; i < yPoss.Count; i++)
        {
            int temp = yPoss[i];
            int ran = Random.Range(0, yPoss.Count);
            yPoss[i] = yPoss[ran];
            yPoss[ran] = temp;
        }
        for (int i = 0; i < GameManager.Instance.GameDesignScriptObj.MaxPawn; i++)
        {
            spawnEnemy(Random.Range(0, 3), xPoss[i], yPoss[i]);
        }
    }
}
