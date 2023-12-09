using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Andrew Olsen
 * Last Updated: 12/07/2023
 * Controls player health, rupee count, and active form
 */
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    // Player health and rupee count
    public int playerHealth = 80;
    public int rupeeCount = 0;

    // Enums for active player type and ability
    public enum ActivePlayerType
    {
        Human,
        Deku,
        Goron,
        Zora
    }

    // Current active player type and ability
    public ActivePlayerType activePlayerType = ActivePlayerType.Human;

    private void Awake()
    {
        // Ensure there's only one instance of the GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
