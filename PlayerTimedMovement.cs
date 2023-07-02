using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimedMovement : MonoBehaviour
{
   
    public GameObject character;
    private Rigidbody2D characterBody;
    private float ScreenWidth;
    public static bool allowMovement = true;
    public GameObject timerBar;
    public static float playerSpeed = 11f;
    //prev was 8.7 for speed

    private void Start()
    {
        this.ScreenWidth = (float) Screen.width;
        this.characterBody = this.character.GetComponent<Rigidbody2D>();
        
#if UNITY_IOS
        
        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {
            playerSpeed = 8.7f;

        }
#endif
    }

    private void Update()
    {

        if (PlayerPrefs.GetString("Controls", "Tilt") == "Touch" && PlayerMovement.playerMove)
        {
            for (int index = 0; index < Input.touchCount; ++index)
            {
                if ((double)Input.GetTouch(index).position.x > (double)this.ScreenWidth / 2.0)
                {
                    this.RunCharacter(1f);
                    character.transform.localScale = new Vector3(-0.2481744f, 0.2857763f, 1);
                    timerBar.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if((double)Input.GetTouch(index).position.x < (double)this.ScreenWidth / 2.0)
                {
                    this.RunCharacter(-1f);
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
            characterBody.velocity = new Vector2(playerSpeed * horizontalInput, 0.0f);
        
    }
}
