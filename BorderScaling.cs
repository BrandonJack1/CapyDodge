using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScaling : MonoBehaviour
{

    public GameObject border;
    public Camera mainCam;
    public GameObject background;

    private void Start() => this.scaleBorderFitScreen();
    
    private void scaleBorderFitScreen()
    {
        // float num1 = (float)Screen.width / (float)Screen.height;
        // this.mainCam.aspect = num1;
        // float num2 = (float)(100.0 * (double)this.mainCam.orthographicSize * 2.0);
        // float num3 = num2 * num1;
        // SpriteRenderer component1 = this.border.GetComponent<SpriteRenderer>();
        // float height1 = component1.sprite.rect.height;
        // float width1 = component1.sprite.rect.width;
        // float y1 = num2 / height1;
        // this.border.transform.localScale = new Vector3(num3 / width1, y1, 1f);
        // SpriteRenderer component2 = this.background.GetComponent<SpriteRenderer>();
        // float height2 = component2.sprite.rect.height;
        // float width2 = component2.sprite.rect.width;
        // float y2 = num2 / height2;
        // this.background.transform.localScale = new Vector3(num3 / width2, y2, 1f);

    }
}
