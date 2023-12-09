using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Author: Andrew Olsen
 * Last Updated: 12/07/2023
 * Controls player movement, attack damage, sword attack
 */
public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    public CameraManager cameraManager;
    public PlayerMovement playerMovement;
    public float playerAttack;
    public GameObject swordPrefab;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        if (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human)
        {
            playerAttack = 1f;
            playerMovement.movementSpeed = 2f;
        }
        else if (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku)
        {
            playerAttack = 0.5f;
            playerMovement.movementSpeed = 3f;
        }
        else if (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron)
        {
            playerAttack = 2f;
            playerMovement.movementSpeed = 1f;
        }
        else if (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora)
        {
            playerAttack = 1f;
            playerMovement.movementSpeed = 2f;
        }
        playerAbility();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }

    /// <summary>
    /// Detects if the player's "Ability" input is triggered and performs the related function
    /// </summary>
    private void playerAbility()
    {
        if (inputManager.playerControls.PlayerMovement.Ability.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human))
        {
            swordSwing();
        }
        if (inputManager.playerControls.PlayerMovement.Ability.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku))
        {
            swordSwing();
        }
        if (inputManager.playerControls.PlayerMovement.Ability.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron))
        {
            swordSwing();
        }
        if (inputManager.playerControls.PlayerMovement.Ability.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora))
        {
            swordSwing();
        }
    }

    /// <summary>
    /// Swings the sword around the player.
    /// </summary>
    private void swordSwing()
    {
        Vector3 swordSpawnPosition = transform.position + transform.forward * 0.5f + Vector3.up * 0.5f;

        // Instantiate the sword with a -90 degree rotation on its own Z-axis
        GameObject sword = Instantiate(swordPrefab, swordSpawnPosition, Quaternion.Euler(0, -90, -90));

        sword.transform.RotateAround(transform.position, transform.up, 35);

        StartCoroutine(SwingSword(sword));
    }

    /// <summary>
    /// Controls the rate of the sword's swing.
    /// </summary>
    /// <param name="sword">The sword prefab.</param>
    /// <returns></returns>
    IEnumerator SwingSword(GameObject sword)
    {
        float elapsedTime = 0f;
        float swingDuration = 0.3f;

        while (elapsedTime < swingDuration)
        {
            float rotationAmount = Mathf.Lerp(0, -360, elapsedTime / swingDuration);
            sword.transform.RotateAround(transform.position, transform.up, rotationAmount * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(sword);
    }
}
