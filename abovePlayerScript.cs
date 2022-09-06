using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class abovePlayerScript : MonoBehaviour
{
    [SerializeField] private Tilemap tileMapColor;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tileMapColor.color = new Color(tileMapColor.color.r, tileMapColor.color.g, tileMapColor.color.b, 0.5f);

            
        }
    }

    /*void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tileMap.SetActive(true);
        }
    }*/

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tileMapColor.color = new Color(tileMapColor.color.r, tileMapColor.color.g, tileMapColor.color.b, 1f);
        }
    }
}
