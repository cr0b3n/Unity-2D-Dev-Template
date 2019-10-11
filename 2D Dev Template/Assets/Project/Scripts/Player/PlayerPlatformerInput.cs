using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class PlayerPlatformerInput : MonoBehaviour {

    [HideInInspector] public float horizontal;      //Float that stores horizontal input
    [HideInInspector] public bool jumpHeld;         //Bool that stores jump pressed
    [HideInInspector] public bool jumpPressed;      //Bool that stores jump held
    [HideInInspector] public bool attackPressed;
    [HideInInspector] public bool dashPressed;      //bool that store dash pressed
    [HideInInspector] public float direction;       //float to identify the facing direction
    private float lastPressedTime;                  //float to store last pressed time since dash was pressed
    private const float DOUBLE_PRESS_TIME = .2f;    //max time allowed to register dash

    private bool readyToClear;                      //Bool used to keep input in sync
    [HideInInspector] public bool isActive;         //Bool check if game is still running

    private void Update() {
        //Clear out existing input values
        ClearInput();

        //If the Game Manager says the game is over, exit
        //if (GameManager.IsGameOver())
        //    return;

        //if (!isActive) return;

        //Process keyboard, mouse, gamepad (etc) inputs
        ProcessInputs();

        //Clamp the horizontal input to be between -1 and 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    private void FixedUpdate() {
        //In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
        //next Update(). This ensures that all code gets to use the current inputs
        readyToClear = true;
    }

    private void ClearInput() {
        //If we're not ready to clear input, exit
        if (!readyToClear)
            return;

        //Reset all inputs
        horizontal = 0f;
        jumpPressed = false;
        jumpHeld = false;

        attackPressed = false;
        dashPressed = false;

        readyToClear = false;
    }

    private void ProcessInputs() {
        //Accumulate horizontal axis input
        horizontal += Input.GetAxis("Horizontal");

        //Accumulate button inputs
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpHeld = jumpHeld || Input.GetButton("Jump");

        //To avoid attack when pressing pause or UI elements
        //if (!EventSystem.current.IsPointerOverGameObject()) {
        //    attackPressed = attackPressed || Input.GetButtonDown("Fire1");
        //}

        float lastDirection = direction;
        float curDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Horizontal")) {

            direction = curDirection;

            float timeSinceLastPressed = Time.time - lastPressedTime;

            if (timeSinceLastPressed <= DOUBLE_PRESS_TIME && lastDirection == direction)
                dashPressed = true;

            lastPressedTime = Time.time;
        }
    }
}
