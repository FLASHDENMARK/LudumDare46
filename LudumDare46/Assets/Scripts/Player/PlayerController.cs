using System;
using UnityEngine;

public class PlayerController : ControllerBase
{
    public float moveSpeed = 5.0F;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float lookSensitivity;

    public Camera playerCamera;
    public InventoryManager inventoryManager;
    public InventoryMovement inventoryMovement;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _allowLook = true;
    private PlayerInteraction _interaction;
    public bool inspect;

    private void OnEnable()
    {

    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _interaction = GetComponent<PlayerInteraction>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void OnDisable()
    {
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        bool leftMouse = Input.GetMouseButton(0);
        bool rightMouse = Input.GetMouseButton(1);
        bool use = Input.GetKeyDown(KeyCode.E);
        bool watch = Input.GetKey(KeyCode.Q);

        // TODO May need to be expanded if we need more weapons
        bool weapon = Input.GetKeyDown(KeyCode.Alpha1);

        Move(move);

        ToggleWatch(watch);

        Inspect(leftMouse, rightMouse, look);

        if(_allowLook)
        {
            Look(look);
        } 

        if (weapon)
        {
            ToggleWeapon();
        }

        if (leftMouse)
        {
            ShootWeapon();
        }
    }

    private void Inspect (bool leftMouse, bool rightMouse, Vector2 look)
    {
        if (inventoryManager.isWeaponEquipped)
        {
            return;
        }

        if (leftMouse)
        {
            if (!inspect && _interaction.InteractionPossible())
            {
                inventoryManager.ToggleWatch(false);

                inspect = true;
                _interaction.PickUp();
            }
        }
        else
        {
            if (inspect)
            {
                _interaction.LetGo();
                inspect = false;
            }
        }

        if (inspect && rightMouse)
        {
            _allowLook = false;
            _interaction.Rotate(look);
        }
        else
        {
            _allowLook = true;
        }
    }

    private void Move (Vector2 vector)
    {
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(vector.y, 0.0f, vector.x);

            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= moveSpeed;

        }
        
        inventoryMovement.Move(vector);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Look (Vector2 vector)
    {
        inventoryMovement.Look(vector);

        float rotationX = transform.localEulerAngles.y + vector.x * lookSensitivity;
        float rotationY = playerCamera.transform.localEulerAngles.x - vector.y * lookSensitivity;

        // TODO Clamp rotationY

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        playerCamera.transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    private void ToggleWatch (bool isActive)
    {
        if (inspect == false)
        {
            inventoryManager.ToggleWatch(isActive);
        }
    }

    private void ToggleWeapon ()
    {
        inventoryManager.ToggleWeapon();
    }

    private void ShootWeapon()
    {
        // We cannot shoot while inspecting an object
        if (!inspect)
        {
            inventoryManager.ShootWeapon();
        }
    }

    public override void Die (IDamageGiver damageGiver)
    {
        
    }
}
