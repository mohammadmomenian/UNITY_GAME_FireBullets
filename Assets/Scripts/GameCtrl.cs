using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour {

    public static GameCtrl instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    /// <summary>
    /// Called when the player bullet hits the enemy
    /// </summary>
    /// <param name="enemy"></param>
    public void BulletHitEnemy(Transform enemy)
    {
        // destroy the enemy
        Destroy(enemy.gameObject);
    }
}
