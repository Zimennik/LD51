using UnityEngine;

public class LocationChanger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _currentLocation;
    [SerializeField] private GameObject _newLocation;

    [SerializeField] private ItemSO _requiredItem;
    [SerializeField] private string _requiredFlag;

    public string InteractionText
    {
        get
        {
            if (!string.IsNullOrEmpty(_requiredFlag))
            {
                if (CharacterController.Instance.flagController.GetFlag(_requiredFlag))
                {
                    return "Open the door";
                }
                else
                {
                    return "Door is locked";
                }
            }

            return (_requiredItem == null || CharacterController.Instance.inventory.HasItem(_requiredItem))
                ? "Open the door"
                : $"You need a {_requiredItem.Name}";
        }
    }


    public void Interact()
    {
        if (!string.IsNullOrEmpty(_requiredFlag))
        {
            if (CharacterController.Instance.flagController.GetFlag(_requiredFlag))
            {
                _currentLocation.SetActive(false);
                _newLocation.SetActive(true);
            }

            return;
        }


        if (_requiredItem == null || CharacterController.Instance.inventory.HasItem(_requiredItem))
        {
            _currentLocation.SetActive(false);
            _newLocation.SetActive(true);
        }
    }

    public void EnterInteractionZone()
    {
    }

    public void ExitInteractionZone()
    {
    }
}