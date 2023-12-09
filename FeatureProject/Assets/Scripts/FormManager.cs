using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public InputManager inputManager;
    public GameObject humanPrefab;
    public GameObject dekuPrefab;
    public GameObject goronPrefab;
    public GameObject zoraPrefab;

    public GameObject activePlayerPrefab;

    private void FixedUpdate()
    {
        PlayerFormSwitch();
    }
    private void PlayerFormSwitch()
    {
        if (inputManager.playerControls.PlayerMovement.LeftItem.triggered && ((GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora)))
        {
            Debug.Log("Input works you're just a fucking moron");
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Deku;
            SwitchPlayerPrefab(dekuPrefab);
        }
        else if (inputManager.playerControls.PlayerMovement.LeftItem.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Human;
            SwitchPlayerPrefab(humanPrefab);
        }
        else if (inputManager.playerControls.PlayerMovement.DownItem.triggered && ((GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora)))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Goron;
            SwitchPlayerPrefab(goronPrefab);
        }
        else if (inputManager.playerControls.PlayerMovement.DownItem.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Human;
            SwitchPlayerPrefab(humanPrefab);
        }
        else if (inputManager.playerControls.PlayerMovement.RightItem.triggered && ((GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Human) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Deku) || (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Goron)))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Zora;
            SwitchPlayerPrefab(zoraPrefab);
        }
        else if (inputManager.playerControls.PlayerMovement.RightItem.triggered && (GameManager.Instance.activePlayerType == GameManager.ActivePlayerType.Zora))
        {
            GameManager.Instance.activePlayerType = GameManager.ActivePlayerType.Human;
            SwitchPlayerPrefab(humanPrefab);
        }
    }

    private void SwitchPlayerPrefab(GameObject newPrefab)
    {
        // Get the current player's position and rotation
        Vector3 currentPosition = activePlayerPrefab.transform.position;
        Quaternion currentRotation = activePlayerPrefab.transform.rotation;

        // Destroy the current player prefab
        Destroy(activePlayerPrefab);

        // Instantiate the new prefab at the same position and rotation
        activePlayerPrefab = Instantiate(newPrefab, currentPosition, currentRotation);
    }
}
