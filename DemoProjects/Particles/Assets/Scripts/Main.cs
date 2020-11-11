using System;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    public GameObject gameObj;
    private const float superposOffset = 0.15f;

    // copy constructor
    public Particle(Particle original)
    {
        gameObj = GameObject.Instantiate(original.gameObj);
    }

    // constructors
    public Particle(GameObject prefab, ID id, Color color) :
        this(prefab, types[id].name, types[id].angle, types[id].isSuper, color)
    {
    }

    public Particle(GameObject prefab, String name, float rot, bool isSuper, Color color)
    {
        // create regular or superposition particle
        if (isSuper)
        {
            GameObject obj1 = GameObject.Instantiate(prefab);
            obj1.name = "p1";
            obj1.transform.Translate(0, superposOffset, 0);
            obj1.transform.GetComponent<Renderer>().material.color = color;

            GameObject obj2 = GameObject.Instantiate(prefab);
            obj2.name = "p2";
            obj2.transform.Translate(0, -superposOffset, 0);
            obj2.transform.Rotate(0, 0, 180f);
            obj2.transform.GetComponent<Renderer>().material.color = Color.white - color;

            // make the two objs children of the generic GameObject
            gameObj = new GameObject();
            obj1.transform.parent = gameObj.transform;
            obj2.transform.parent = gameObj.transform;
            gameObj.transform.Rotate(0, 0, rot);
        }
        else
        {
            gameObj = GameObject.Instantiate(prefab);
            gameObj.transform.Rotate(0, 0, rot);
            gameObj.transform.GetComponent<Renderer>().material.color = color;
        }

        // set name for hierarchy
        gameObj.name = name;
    }

    public void SetColor(Color color)
    {
        if (gameObj.transform.childCount == 0)
        {
            gameObj.transform.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Transform t1 = gameObj.transform.Find("p1");
            Transform t2 = gameObj.transform.Find("p2");
            Debug.Assert(t1 != null && t2 != null);
            t1.GetComponent<Renderer>().material.color = color;
            t2.GetComponent<Renderer>().material.color = color;
        }
    }

    public void MoveTo(float x, float y)
    {
        gameObj.transform.Translate(x, y, 3);
    }

    public struct Info
    {
        public String  name;
        public float   angle;
        public bool    isSuper;
    }
    public enum ID : int {R, U, L, D, RL, UD};

    public static Dictionary<ID, Info> types = new Dictionary<ID, Info>()
    {
        {ID.R,  new Info() {name = "Right",       angle = 0,      isSuper = false}},
        {ID.U,  new Info() {name = "Up",          angle = 90,     isSuper = false}},
        {ID.L,  new Info() {name = "Left",        angle = 180,    isSuper = false}},
        {ID.D,  new Info() {name = "Down",        angle = 270,    isSuper = false}},
        {ID.RL, new Info() {name = "RightLeft",   angle = 0,      isSuper = true}},
        {ID.UD, new Info() {name = "UpDown",      angle = 90,     isSuper = true}}
    };
}

public class Main : MonoBehaviour
{
    public GameObject ParticlePrefab;

    List<Particle> particles = new List<Particle>();

    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    void Test()
    {
        Color[] colorList = {Color.red, Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow};
        System.Random rnd = new System.Random();
        int numIDs = Enum.GetNames(typeof(Particle.ID)).Length;
        int numColors = colorList.Length;
        const int spread = 12;


        for (int i = 0; i < 12; ++i)
        {
            particles.Add(new Particle(ParticlePrefab, (Particle.ID) rnd.Next(numIDs), colorList[rnd.Next(numColors)]));
            //particles.Add(new Particle(ParticlePrefab, (Particle.ID) rnd.Next(numIDs), new Color((float) rnd.NextDouble(), (float) rnd.NextDouble(), (float) rnd.NextDouble())));
            particles[i].MoveTo(rnd.Next(spread) - spread / 2, rnd.Next(spread) - spread / 2);
        }
    }
}

