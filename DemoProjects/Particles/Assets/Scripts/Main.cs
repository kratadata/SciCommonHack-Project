using System;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    public GameObject gameObj;
    private const float superposOffset = 0.4f;

    // copy constructor
    public Particle(Particle original)
    {
        gameObj = GameObject.Instantiate(original.gameObj);
    }

    // constructor
    public Particle(GameObject prefab, String name, float rot, bool isSuper = false)
    {
        // create regular or superposition particle
        if (isSuper)
        {
            GameObject obj1 = GameObject.Instantiate(prefab);
            obj1.name = "p1";
            obj1.transform.Rotate(0, 0, rot);
            obj1.transform.Translate(superposOffset, 0, 0);

            GameObject obj2 = GameObject.Instantiate(prefab);
            obj2.name = "p2";
            obj2.transform.Rotate(0, 0, rot + 180f);
            obj2.transform.Translate(-superposOffset, 0, 0);

            // make the two objs children of the generic GameObject
            gameObj = new GameObject();
            obj1.transform.parent = gameObj.transform;
            obj2.transform.parent = gameObj.transform;
        }
        else
        {
            gameObj = GameObject.Instantiate(prefab);
            gameObj.transform.Rotate(0, 0, rot);
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
}


public class Main : MonoBehaviour
{
    public GameObject ParticlePrefab;

    Dictionary<String, Particle> particleTypes = new Dictionary<string, Particle>();
    List<Particle> particles = new List<Particle>();

    // Start is called before the first frame update
    void Start()
    {
        // create dictionary of particle types
        particleTypes.Add("R", new Particle(ParticlePrefab, "Right", 0));
        particleTypes.Add("U", new Particle(ParticlePrefab, "Up", 90));
        particleTypes.Add("L", new Particle(ParticlePrefab, "Left", 180));
        particleTypes.Add("D", new Particle(ParticlePrefab, "Down", 270));
        particleTypes.Add("RL", new Particle(ParticlePrefab, "RightLeft", 0, true));
        particleTypes.Add("UD", new Particle(ParticlePrefab, "UpDown", 180, true));

        Test();
    }

    void Test()
    {
        String[] typeNames = {"R", "U", "L", "D", "RL", "UD"};
        Color[] colorList = {Color.red, Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow};
        System.Random rnd = new System.Random();

        for (int i = 0; i < 20; ++i)
        {
            String rndTypeName = typeNames[rnd.Next(6)];
            Particle rndParticle = new Particle(particleTypes[rndTypeName]);
            particles.Add(rndParticle);
            rndParticle.SetColor(colorList[rnd.Next(6)]);
            rndParticle.MoveTo(rnd.Next(10) - 5, rnd.Next(10) - 5);
        }
    }
}

