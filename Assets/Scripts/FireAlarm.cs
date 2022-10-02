using UnityEngine;

public class FireAlarm : MonoBehaviour, IInteractable, IResetable
{
    [SerializeField] private GameObject _alarmGO;

    private bool _isActive;


    public string InteractionText => (_isActive) ? "Turn fire alarm off" : "Turn fire alarm on";

    public void Interact()
    {
        if (_isActive)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void ResetObject()
    {
        TurnOff();
    }

    public void TurnOn()
    {
        _isActive = true;
        _alarmGO.SetActive(true);
        CharacterController.Instance.flagController.SetFlag("fire_alarm", true);
        CharacterController.Instance.RefreshInteractionText();
    }

    public void TurnOff()
    {
        _isActive = false;
        _alarmGO.SetActive(false);

        CharacterController.Instance.flagController.SetFlag("fire_alarm", false);
        CharacterController.Instance.RefreshInteractionText();
    }
}