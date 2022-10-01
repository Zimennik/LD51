using System;
using UnityEngine;


//Player can move left and right.
//Player can interact with objects and NPCs.
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] public FlagController flagController;

    public Inventory inventory;
    public bool IsCutscene = false;

    private IInteractable _currentInteractable;


    // Singleton
    public static CharacterController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Movement
    private void Update()
    {
        //movement
        if (!IsCutscene)
        {
            var horizontalInput = Input.GetAxis("Horizontal");

            _playerSprite.flipX = horizontalInput switch
            {
                > 0 => false,
                < 0 => true,
                _ => _playerSprite.flipX
            };

            _playerAnimator.SetBool("IsWalking", Mathf.Abs(horizontalInput) > 0);

            //move through rigidbody2D
            _rigidbody2D.velocity = new Vector2(horizontalInput * _speed, 0);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            _playerAnimator.SetBool("IsWalking", false);
        }

        if (!IsCutscene)
        {
            //interaction
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!IsCutscene)
                {
                    _currentInteractable?.Interact();
                }
            }
        }
    }


    public void StartCutscene()
    {
        IsCutscene = true;
        _currentInteractable = null;
        UIController.Instance.HideInteractionText();
    }

    public void EndCutscene()
    {
        IsCutscene = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsCutscene)
        {
            if (!other.TryGetComponent(out IInteractable interactable)) return;
            if (_currentInteractable == interactable) return;
            _currentInteractable?.ExitInteractionZone();

            _currentInteractable = interactable;
            _currentInteractable.EnterInteractionZone();
            UIController.Instance.ShowInteractionText(_currentInteractable.InteractionText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IInteractable interactable)) return;
        if (_currentInteractable != interactable) return;
        _currentInteractable?.ExitInteractionZone();

        _currentInteractable = null;
        UIController.Instance.HideInteractionText();
    }
}