using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateJupiter : MonoBehaviour {

    Texture2D thisTexture;

    public GameObject moonTemplate;

    //public GameObject[] planets = new GameObject[10];
    public GameObject[] moons = new GameObject[4];
    private string[] mnames = { "Io", "Europa", "Ganymede", "Callisto" };
    private float[] moonSizes = {3660.0f, 3121.6f, 5262.4f, 4820.6f };
    private float[] orbitDistance = { 421700.0f, 671034.0f, 1070412.0f, 1882709.0f };
    private float[] orbitalPeriod = { 1.7691f, 3.5521f, 7.1546f, 16.689f };
    private float distanceScale = 140000.0f;   // 421700  140000
    private float rotationScale = 0.41f;

    private float timeAdjust = 40.0f; //6.28f;
    private float orbitScale = 0.5f;
    private float sizeScale = 0.5f;

    private float xplanet = 0.0f;
    private float yplanet =0.0f;
    private float zplanet = 2.0f;

    // Use this for initialization
    void Start()
    {

        float y;
        float ds;
        float r;
        y = 0.0f;
        //moons = new GameObject[4];

        for (int x = 0; x < 4; x++)
        { 
            ds = moonSizes[x]/ distanceScale * sizeScale;
            r = orbitDistance[x] / distanceScale;

            //planets[x] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            moons[x] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            moons[x].AddComponent<Rigidbody>();
            moons[x].GetComponent<Rigidbody>().useGravity = false;
            moons[x].transform.position = new Vector3(r, y, 0);
            moons[x].transform.localScale = new Vector3(ds, ds, ds);
            moons[x].name = mnames[x];

            moons[x].AddComponent<moonScript>();

            moons[x].GetComponent<moonScript>().orbitDistance = r * orbitScale;
            moons[x].GetComponent<moonScript>().orbitTime = orbitalPeriod[x] / rotationScale * timeAdjust;
            moons[x].GetComponent<moonScript>().xplanet = xplanet;
            moons[x].GetComponent<moonScript>().yplanet = yplanet;
            moons[x].GetComponent<moonScript>().zplanet = zplanet;


    /*
    moons[x] = Instantiate(moonTemplate);
    moons[x].orbitDistance = r;
    moons[x].transform.localScale = new Vector3(ds, ds, ds);
    moons[x].transform.position = new Vector3(r, y, 0);
    moons[x].name = mnames[x];
    */

    //Debug.Log(cube);
        Texture2D mytexture = Resources.Load(mnames[x]) as Texture2D;
        moons[x].GetComponent<Renderer>().material.mainTexture = mytexture;



        }


    }

    void OnCollisionEnter(Collision collision)
    {
        //change the colour of the cube
        GetComponent<Renderer>().material.color = Color.blue;
    }


    void LateUpdate()
    {
        //float rotateSpeed;
        float oRate;
        //float oDistance;
        //float xpos, ypos, zpos;
        oRate = -6.28f * timeAdjust;
        transform.Rotate(transform.up * oRate * Time.deltaTime);

    }



	// Update is called once per frame
	void Update () {
		
	}
}
