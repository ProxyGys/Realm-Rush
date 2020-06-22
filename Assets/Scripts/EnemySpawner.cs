using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawn = 5f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnEnemies;
    [SerializeField] AudioClip spawnedEnemySFX;

    int score;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedSpawnEnemies());
        spawnEnemies.text = score.ToString();
    }

    IEnumerator RepeatedSpawnEnemies()
    {
        while (true)
        {
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
    }

    private void AddScore()
    {
        score++;
        spawnEnemies.text = score.ToString();
    }
}
