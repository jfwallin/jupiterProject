

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class moonScript : MonoBehaviour
{


    public float orbitDistance = 10.0f;
    public float rotationTime  = 1.0f;
    public float orbitTime = 1.0f;
    //public GameObject planet;
    public float xpos = 0.0f;
    public float ypos = 0.0f;
    public float zpos = 0.0f;
    public float orbitThetaOffset = 0.0f;
    public float rotationThetaOffset = 0.0f;
    public float xplanet = 0.0f;
    public float yplanet = 0.0f;
    public float zplanet = 0.0f;

    private bool isBlue;
    private Texture mytexture;
    private Vector3 originalSize;

    private float orbitRate, rotationRate;
    //private float twoPi = Mathf.PI * 2.0f;

    // Use this for initialization
    void Start()
    {

        orbitRate = 1.0f / orbitTime;
        rotationRate = 1.0f / rotationTime;
        isBlue = false;
        Renderer rend;
        rend=  GetComponent<Renderer>();
        mytexture = rend.material.mainTexture;

        originalSize = transform.localScale;
        //orbitRate = twoPi / orbitTime;
        //rotationRate = twoPi / rotationTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        //change the colour of the cube
        if (isBlue)
        {
            //GetComponent<Renderer>().material.color = Color.white;
            //GetComponent<Renderer>().material.mainTexture = mytexture;

            //GetComponent<Renderer>().material.color = Color.white;
            isBlue = false;

            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        }
        else
        {
            //GetComponent<Renderer>().material.color = Color.blue;
            isBlue = true;
            transform.localScale = originalSize;
        }

    }



    // Update is called once per frame
    void LateUpdate()
    {
        //float xplanet, yplanet, zplanet;
        float orbitTheta;
        float rotationTheta;

        orbitTheta = orbitThetaOffset + orbitRate * Time.time;
        //rotationTheta = rotationThetaOffset + rotationRate * Time.time;
        rotationTheta = rotationThetaOffset + orbitRate * Time.time;

        xpos = orbitDistance * Mathf.Cos(orbitTheta) + xplanet;
        ypos = yplanet;
        zpos = orbitDistance * Mathf.Sin(orbitTheta) + zplanet;

        transform.localPosition = new Vector3(xpos, ypos, zpos);
        //transform.Rotate(transform.up * orbitTheta);
        //transform.Rotate(transform.up * rotationTheta);
    }
}


