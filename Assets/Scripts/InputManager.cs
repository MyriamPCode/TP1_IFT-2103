using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        LoadKeyBindings();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void RebindMoveAction()
    {
        var moveAction = inputActions.Gameplay.Déplacement;

        moveAction.PerformInteractiveRebinding()
            .OnComplete(operation =>
            {
                operation.Dispose();
                SaveKeyBindings();
                Debug.Log("Rebind complété !");
            })
            .Start();
    }

    public void SaveKeyBindings()
    {
        var moveAction = inputActions.Gameplay.Déplacement;
        PlayerPrefs.SetString("MoveBinding", moveAction.bindings[0].effectivePath);
        PlayerPrefs.Save();
    }

    public void LoadKeyBindings()
    {
        if (PlayerPrefs.HasKey("MoveBinding"))
        {
            var moveBinding = PlayerPrefs.GetString("MoveBinding");
            inputActions.Gameplay.Déplacement.ApplyBindingOverride(moveBinding);
        }
    }
}