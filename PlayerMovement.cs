using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Rigidbody2D characterBody;
    [SerializeField] private GameObject timerBar;
    [SerializeField] private Camera mainCamera;
    
    public static bool allowMovement = true;
    public static bool playerMove = true;

    private float leftOffset;
    private float rightOffset;
    private float playerWidth;
    private float playerHeight;
    private const float PLAYER_SPEED = 10.7f;
    private float iPadOffset;
    private int fpsOffset;
    
    void Start()
    {
        //reduce player speeds for iPAD
#if UNITY_IOS
        
        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {
            playerSpeed = 8.7f;
            iPadOffset = 0.2f;
        }
#endif
        
        characterBody = character.GetComponent<Rigidbody2D>();
        playerMove = true;
        
        //unless its the players first time, time should always be normal
        if (PlayerPrefs.GetString("FirstTime", "Yes") != "Yes")
        {
            Time.timeScale = 1f;
        }
        
        //Get screen offsets to prevent user from leaving the area
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 17;
        
        character.transform.localScale = new Vector3(PlayerSize.PLAYER_WIDTH, PlayerSize.PLAYER_HEIGHT, 1);
    }

    void Update()
    {
        if (Input.GetKey("d"))
        {
            //move character
            RunCharacter(1f);
            
            //flip character
            character.transform.localScale = new Vector3(-playerWidth, playerHeight, 1);
            
            //flip power up loading bar
            timerBar.transform.localScale = new Vector3(1, 1, 1);

        }
        else if (Input.GetKey("a"))
        {
            //move character
            RunCharacter(-1f);
            
            //flip character
            character.transform.localScale = new Vector3(playerWidth, playerHeight, 1);
            
            //flip power up loading bar
            timerBar.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            //if no keys are being pressed then stop the character
            character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        
        if (PlayerPrefs.GetString("Controls", "Tilt") == "Tilt")
        {
            //if the player moves out of these bounds then stop them
            if(mainCamera.WorldToScreenPoint(character.transform.position).x < (leftOffset) && Input.acceleration.x<0f)
            {
            }
            else if(mainCamera.WorldToScreenPoint(character.transform.position).x > (Screen.width -rightOffset) && Input.acceleration.x>0f)
            {
            }
            else
            {
                if (playerMove)
                {
                    character.transform.Translate(Input.acceleration.x/ (1.4f + iPadOffset), 0f, 0f);
                    
                    if (Input.acceleration.x < 0f)
                    {
                        character.transform.localScale = new Vector3(playerWidth, playerHeight, 1);
                        timerBar.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if(Input.acceleration.x >0f)
                    {
                        character.transform.localScale = new Vector3(-playerWidth, playerHeight, 1);
                        timerBar.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
        }
    }
    void RunCharacter(float horizontalInput)
    {
        if (!allowMovement)
            return;
        if (horizontalInput != 0.0)
            characterBody.velocity = new Vector2(PLAYER_SPEED * horizontalInput, 0.0f);
    }
}

