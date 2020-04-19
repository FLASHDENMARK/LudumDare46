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
    private InteractableController _interaction;
    private bool inspect;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _interaction = GetComponent<InteractableController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Called by a trigger when the player steps inside it
    /// </summary>
    /// <param name="interaction"></param>

    private void OnGUI()
    {
        if (trigger != null && trigger.isEnabled)
        {
            bool hasTriggerText = trigger.on ? !string.IsNullOrEmpty(trigger.triggerOffText) : !string.IsNullOrEmpty(trigger.triggerOnText);
            string triggerText = null;

            if (hasTriggerText)
            {
                string action = trigger.on ? trigger.triggerOffText : trigger.triggerOnText;
                triggerText = $"Press E to {action}"; 
            }
            else
            {
                // If no trigger text is set this will be the default UI text
                string onOff = trigger.on ? "off" : "on";

                triggerText = $"Press E to switch {trigger.gameObject.name} " + onOff;
            }

            GUI.Label(new Rect(30, 30, 400, 30), triggerText);
        }
    }


    // Update is called once per frame
    void Update ()
    {
        Vector2 move = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        bool leftMouse = Input.GetMouseButton(0);
        bool rightMouse = Input.GetMouseButton(1);
        bool use = Input.GetKeyDown(KeyCode.E);
        bool watch = Input.GetKey(KeyCode.Q);
        bool flashlight = Input.GetKey(KeyCode.Tab);
        bool esc = Input.GetKey(KeyCode.Escape);

        // TODO May need to be expanded if we need more weapons
        bool weapon = Input.GetKeyDown(KeyCode.Alpha1);

        Move(move);

        Inspect(leftMouse, rightMouse, look);

        if (Input.GetKeyDown(KeyCode.Escape) && inspect == false)
        {
            _allowLook = !_allowLook;
        }

        // Only toggle the watch if the flashlight is unequipped
        if (!flashlight)
        {
            ToggleGadget(inventoryManager.watch, watch);
        }

        if (!watch)
        {
            ToggleGadget(inventoryManager.flashlight, flashlight);
        }

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

        if (use)
        {
            TriggerInteraction();
        }
    }

    private void TriggerInteraction()
    {
        if (trigger != null)
        {
            trigger.ToggleUse();
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
                // TODO If we get more gadgets may need a more general system
                inventoryManager.ToggleGadget(inventoryManager.flashlight, false);
                inventoryManager.ToggleGadget(inventoryManager.watch, false);

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

    private void ToggleGadget (GadgetBase gadget, bool isActive)
    {
        if (inspect == false)
        {
            inventoryManager.ToggleGadget(gadget, isActive);
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
