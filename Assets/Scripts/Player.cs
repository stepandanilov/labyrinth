using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void Start()
    {
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                transform.position = new Vector2(0.5f, 0.5f);
                break;
            case 2:
                transform.position = new Vector2(0.75f, 0.4330127f);
                break;
            case 3:
                transform.position = new Vector2(0, 0);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.getInstance().CollisionCheck(collision, transform);
    }
}
