using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadScript : MonoBehaviour {

    public float fadeSpeed = 0.3f;
    public Image loadingImage;
    public Color32 fadeColour = new Color32(255, 255, 255, 255);
    public Color32 fadeOut = new Color32(0, 0, 0, 0);
    bool showLoadScreen = true;


    private void Start()
    {
        loadingImage.color = fadeColour;
        
    }

    void fadingOut()
    {
        loadingImage.color = Color.Lerp(loadingImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update () {

        if (loadingImage.color != fadeOut)
        {
            fadingOut();
        }

    }
}
