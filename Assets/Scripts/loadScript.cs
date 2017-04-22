using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadScript : MonoBehaviour {

    public float flashSpeed = 25f;
    public Image loadingImage; 

	// Use this for initialization
	void Start () {

        loadingImage.color = Color.Lerp(loadingImage.color, Color.clear, flashSpeed * Time.deltaTime);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
