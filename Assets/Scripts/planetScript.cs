using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetScript : MonoBehaviour {

    public float timeAdjust = 10.0f; //6.28f;
    public float planetSizeScale = 0.25f; //0.5f;
    public float xplanet, yplanet, zplanet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void OnCollisionEnter(Collision collision)
    {
        //change the colour of the cube
        GetComponent<Renderer>().material.color = Color.blue;
    }



    void LateUpdate()
    {
        float oRate;
        oRate = -6.28f * timeAdjust;
        transform.Rotate(transform.up * oRate * Time.deltaTime);

    }


}
