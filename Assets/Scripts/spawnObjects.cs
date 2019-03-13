using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour {

    Texture2D thisTexture;

    public GameObject moonTemplate;

    //public GameObject[] planets = new GameObject[10];
    public GameObject[] particlebursts = new GameObject[3];
    public GameObject[] planet = new GameObject[1];
    public GameObject[] moonSign = new GameObject[1];
    public GameObject[] moons = new GameObject[4];

    public ParticleSystem theParticles;

    private string[] mnames = { "Io", "Europa", "Ganymede", "Callisto" };
    private float[] moonSizes = {3660.0f, 3121.6f, 5262.4f, 4820.6f };
    private float[] orbitDistance = { 421700.0f, 671034.0f, 1070412.0f, 1882709.0f };
    private float[] orbitalPeriod = { 1.7691f, 3.5521f, 7.1546f, 16.689f };
    private float distanceScale = 140000.0f;   // 421700  140000
    private float rotationScale = 0.41f;

    private float timeAdjust = 10.0f; //6.28f;
    private float planetSizeScale = 0.25f; //0.5f;

    private float moonSizeScale = 2.0f; //0.5f;
    private float moonOrbitScale = 0.5f;
    private float orbitScale = 0.25f;//0.5f;

    private float signAspect = 0.5625f;
    private float signSize = 0.2f;
    private bool signSmall = true;


    private Vector3 logoLocation = new Vector3(0.0f, 1.5f, 1.9f);
    private Vector3 logoSize = new Vector3(0.5f, 0.28f, 0.01f);


    private float xplanet = 0.0f;
    private float yplanet = 1.9f;
    private float zplanet = 2.0f;

    private float nsigns = 1;
    private float nmoons = 4;

    private float signVerticalDisplacement = 0.25f;
    bool objectsCreated = false;


    public int simulationState = 0;

    // Use this for initialization
    void Start() {
        theParticles.Play();
        createSigns();
    }

    void logo()
    {

        moonSign[0].transform.position = logoLocation;
        Texture2D mySignTexture = Resources.Load("TcomsMark") as Texture2D;
        //Texture2D mySignTexture = Resources.Load("InfoIo") as Texture2D;
        moonSign[0].GetComponent<Renderer>().material.mainTexture = mySignTexture;
        moonSign[0].transform.localScale = logoSize;

    }

    void createPlanet()
    {
        planet[0] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet[0].AddComponent<Rigidbody>();
        planet[0].GetComponent<Rigidbody>().useGravity = false;
        planet[0].transform.position = new Vector3(xplanet, yplanet, zplanet);
    
        planet[0].transform.localScale = new Vector3(planetSizeScale, planetSizeScale, planetSizeScale); 
        Texture2D myPlanetTexture = Resources.Load("Jupiter") as Texture2D;
        planet[0].GetComponent<Renderer>().material.mainTexture = myPlanetTexture;
        planet[0].name = "Jupiter";

        planet[0].AddComponent<planetScript>();

        planet[0].GetComponent<planetScript>().timeAdjust = timeAdjust; 
        planet[0].GetComponent<planetScript>().xplanet = xplanet;
        planet[0].GetComponent<planetScript>().yplanet = yplanet;
        planet[0].GetComponent<planetScript>().zplanet = zplanet;

    }

    void createSigns()
    {
        for (int i = 0; i < nsigns; i++)
        {

            moonSign[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            moonSign[i].AddComponent<imageBox>();
            moonSign[i].AddComponent<Rigidbody>();
            moonSign[i].GetComponent<Rigidbody>().useGravity = false;
            moonSign[i].transform.position = new Vector3(xplanet, yplanet + signVerticalDisplacement, zplanet);
            moonSign[i].transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            moonSign[i].transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            Texture2D mySignTexture = Resources.Load("InfoIo") as Texture2D;
            moonSign[i].GetComponent<Renderer>().material.mainTexture = mySignTexture;
            moonSign[i].name = "MoonSign";
        }

    }


    void createMoons()
    {
        float ds, r, y;
        y = 0.0f;

        for (int x = 0; x < nmoons; x++)
        {
            ds = moonSizes[x] / distanceScale * moonSizeScale;
            r = orbitDistance[x] / distanceScale * moonOrbitScale;

            //planets[x] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            moons[x] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            moons[x].AddComponent<Rigidbody>();
            moons[x].GetComponent<Rigidbody>().useGravity = false;
            moons[x].transform.position = new Vector3(r, y, 0);
            moons[x].transform.localScale = new Vector3(ds, ds, ds);
            moons[x].name = mnames[x];

            moons[x].AddComponent<moonScript>();

            moons[x].GetComponent<moonScript>().orbitDistance = r * orbitScale;
            moons[x].GetComponent<moonScript>().orbitTime = orbitalPeriod[x] / rotationScale / timeAdjust;
            moons[x].GetComponent<moonScript>().xplanet = xplanet;
            moons[x].GetComponent<moonScript>().yplanet = yplanet;
            moons[x].GetComponent<moonScript>().zplanet = zplanet;
            moons[x].GetComponent<moonScript>().moonName = mnames[x];

            //Debug.Log(cube);
            Texture2D mytexture = Resources.Load(mnames[x]) as Texture2D;
            moons[x].GetComponent<Renderer>().material.mainTexture = mytexture;

        }

    }



    void LateUpdate()
    {
        const int fireworks = 0;
        const int displayLogo = 1;
        const int startPlanets = 2;
        const int displaySign = 3;
        float fireworkEndTime = 14;
        float logoEndTime = 20;
        float planetEndTime = 25;

        switch(simulationState)
        {


            case fireworks:
                if (Time.time > fireworkEndTime) {
                    simulationState = simulationState + 1;
                    logo();
                }
                break;

            case displayLogo:
                if (Time.time > logoEndTime) {
                    simulationState = simulationState + 1;
                    moonSign[0].transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                    moonSign[0].transform.position = new Vector3(xplanet, yplanet + signVerticalDisplacement, zplanet);
                    createPlanet();
                    createMoons();
                    //objectsCreated = true;
                }
                break;

            case startPlanets:
                if (Time.time > planetEndTime) {
                    simulationState = simulationState + 1;
                    Texture2D mySignTexture = Resources.Load("InfoIo") as Texture2D;
                    moonSign[0].GetComponent<Renderer>().material.mainTexture = mySignTexture;
                    moonSign[0].transform.localScale = new Vector3(signSize, signSize * signAspect, 0.001f);
                    signSmall = false;
                }
                break;

        }





    }



    // Update is called once per frame
    void Update () {
		
	}
}
