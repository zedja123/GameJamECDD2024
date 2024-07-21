using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lock : MonoBehaviour
{
    [SerializeField] private EnemyMaster enemyMaster;

    private void Start()
    {
        enemyMaster = GetComponent<EnemyMaster>();
    }

    private void FixedUpdate()
    {
        if (enemyMaster.health <= 1)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
