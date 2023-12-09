using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Andrew Olsen
 * Last Updated: 12/07/2023
 * Controls inputs and controls
 */
public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    public GameObject humanPrefab;
    public GameObject dekuPrefab;
    public GameObject goronPrefab;
    public GameObject zoraPrefab;

    public GameObject activePlayerPrefab;

    private Transform playerTransform;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    /// <summary>
    /// Handles all player inputs, including movement and switching forms.
    /// </summary>
    public void HandleAllInputs()
    {
        PlayerFormSwitch();
        HandleMovementInput();
    }

    /// <summary>
    /// Detects player input for directional and camera movement.
    /// </summary>
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
    }

    /// <summary>
    /// Switches the player's current "form."
    /// </summary>
    private void PlayerFormSwitch()
    {
        if (playerControls.PlayerMovement.LeftItem.triggered && ((GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora)))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Deku;
            SwitchPlayerPrefab(dekuPrefab);
        }
        else if (playerControls.PlayerMovement.LeftItem.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Human;
            SwitchPlayerPrefab(humanPrefab);
        }
        else if (playerControls.PlayerMovement.DownItem.triggered && ((GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora)))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Goron;
            SwitchPlayerPrefab(goronPrefab);
        }
        else if (playerControls.PlayerMovement.DownItem.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Human;
            SwitchPlayerPrefab(humanPrefab);
        }
        else if (playerControls.PlayerMovement.RightItem.triggered && ((GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron)))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Zora;
            SwitchPlayerPrefab(zoraPrefab);
        }
        else if (playerControls.PlayerMovement.RightItem.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Human;
            SwitchPlayerPrefab(humanPrefab);
        }
    }

    /// <summary>
    /// Stores transform data for the current prefab and switches prefabs.
    /// </summary>
    /// <param name="newPrefab">The prefab being instantiated to replace the pre-existing one.</param>
    private void SwitchPlayerPrefab(GameObject newPrefab)
    {
        Vector3 currentPosition = activePlayerPrefab.transform.position;
        Quaternion currentRotation = activePlayerPrefab.transform.rotation;

        Transform currentTransform = activePlayerPrefab.transform;

        Destroy(activePlayerPrefab);

        activePlayerPrefab = Instantiate(newPrefab, currentPosition, currentRotation);

        SetPlayerTransform(activePlayerPrefab.transform);
    }

    /// <summary>
    /// Sets new transformation data for prefab
    /// </summary>
    /// <param name="newTransform">New transformation data</param>
    public void SetPlayerTransform(Transform newTransform)
    {
        playerTransform = newTransform;
    }
}
