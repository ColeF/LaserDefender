using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 11.5f;
    public float height = 6.5f;
    public float moveSpeed = 5.0f;
    public float spawnDelay = 0.1f;

    private Vector3 currentDirection = Vector3.right;
    private float padding = 0.0f;
    private float xMin;
    private float xMax;

	// Use this for initialization
	void Start () {
        float zDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zDistance));
        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
        SpawnUntilFull();
	}

    public void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    public void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }

        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    void Update () {
        MoveFormation();
    }

    private void MoveFormation()
    {
        transform.position += currentDirection * moveSpeed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, 0, 0);

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation <= xMin) { currentDirection = Vector3.right; }

        else if (rightEdgeOfFormation >= xMax) { currentDirection = Vector3.left; }

        if (AllMembersDead())
        {
            Debug.Log("All dead, boss!");
            //EnemyController.fireRate += EnemyController.fireRate;
            //if (EnemyController.shotSpeed < 15) { EnemyController.shotSpeed += 3; }
            SpawnUntilFull();
        }
    }

    public Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }

        return null;
    }

    private bool AllMembersDead()
    {

        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }
}
