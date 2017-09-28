using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        enum PlayerSlot
        {
            First,
            Second
        }


        private ThirdPersonCharacter character;     // A reference to the ThirdPersonCharacter on the object
        private Transform cameraTransform;           // A reference to the main camera in the scenes transform
        private Vector3 cameraDirection;             // The current forward direction of the camera
        private Vector3 movement;
        private bool isJumping;                      // the world-relative desired move direction, calculated from the camForward and user input.
        [SerializeField]
        PlayerSlot playerControls = PlayerSlot.First;


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!isJumping)
            {
                isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

                if (playerControls == PlayerSlot.First)
                {
                    isJumping = CrossPlatformInputManager.GetButtonDown("Jump_P1");
                }
                else// if (playerControls == PlayerSlot.Second)
                {
                    isJumping = CrossPlatformInputManager.GetButtonDown("Jump_P2");
                }
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            if (playerControls == PlayerSlot.First)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal_P1");
                v = CrossPlatformInputManager.GetAxis("Vertical_P1");
            }
            else// if (playerControls == PlayerSlot.Second)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal_P2");
                v = CrossPlatformInputManager.GetAxis("Vertical_P2");
            }
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (cameraTransform != null)
            {
                // calculate camera relative direction to move:
                cameraDirection = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
                movement = v * cameraDirection + h * cameraTransform.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                movement = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) movement *= 0.5f;
#endif

            // pass all parameters to the character control script
            character.Move(movement, crouch, isJumping);
            isJumping = false;
        }
    }
}
