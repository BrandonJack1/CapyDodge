using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private Rigidbody2D characterBody;
    private float ScreenWidth;
    public static bool allowMovement = true;
    [SerializeField] private GameObject timerBar;
    private const float PLAYER_SPEED = 11f;

    private void Start()
    {
        characterBody = character.GetComponent<Rigidbody2D>();
        
        //reduce player speed for ipad
#if UNITY_IOS
        
        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {
            playerSpeed = 8.7f;

        }
#endif
    }

    private void Update()
    {
        //if the player is using touch controls
        if (PlayerPrefs.GetString("Controls", "Tilt") == "Touch" && PlayerMovement.playerMove)
        {
            for (int index = 0; index < Input.touchCount; ++index)
            {
                //if the player is touching on the right side of the screen
                if (Input.GetTouch(index).position.x > Screen.width / 2.0)
                {
                    RunCharacter(1f);
                    character.transform.localScale = new Vector3(-0.2481744f, 0.2857763f, 1);
                    timerBar.transform.localScale = new Vector3(-1, 1, 1);
                }
                //if the player is touching on the left side of the screen
                else if(Input.GetTouch(index).position.x < Screen.width / 2.0)
                {
                    RunCharacter(-1f);
                    character.transform.localScale = new Vector3(0.2481744f, 0.2857763f, 1);
                    timerBar.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }
        }
    }
    void RunCharacter(float horizontalInput)
    {
        if (!PlayerMovement.allowMovement)
            return;
        if (horizontalInput != 0.0)
            characterBody.velocity = new Vector2(PLAYER_SPEED * horizontalInput, 0.0f);
        
    }
}
