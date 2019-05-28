using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {

    public Tree tree;
    public int number;
    public float spawnRadius = 100;
   
    // list to store all the spawned "trees"
    private List<Tree> trees = new List<Tree>();

    void Start()
    {
        placeTrees();
    }

    void placeTrees()
    {
        float yRotationMin = -100;
        float yRotationMax = 100;

        // spawn a number of trees
        for (int i = 0; i < number; i++)
        {
            float yRotation = UnityEngine.Random.Range(yRotationMin, yRotationMax);
            float zRotation = UnityEngine.Random.Range(yRotationMin / 2, yRotationMax / 2);
            float xRotation = 0;

            // Spawn the "trees", add them to a list for later rotation.
            trees.Add(Instantiate(tree, generateInsideCirlce(), Quaternion.Euler(xRotation, yRotation, zRotation)));
        }

    }

    // return a random position inside a circle 
    Vector3 generateInsideCirlce()
    {
        Vector3 insideCirclePosition = new Vector3();
        // find a random position inside a circle of spawnRadius size.
        insideCirclePosition = Random.insideUnitCircle * spawnRadius;

        // flatten the circle out from a y/z orientated position to a x/z orietated position 
        insideCirclePosition.x += transform.position.x;
        insideCirclePosition.z = insideCirclePosition.y + transform.position.z;
        insideCirclePosition.y = transform.position.y;

        return insideCirclePosition;
    }

    // Update is called once per frame
    void Update ()
    {
        float yRotation = 50;

        // for all the trees that are spawned.
        for (int i = 0; i < trees.Count; i++)
        {
            // for every even tree
            if (i % 2 == 0)
            {
                // rotate tree clockwise
                trees[i].transform.Rotate(0, yRotation * Time.deltaTime, 0);
            }// for every odd tree
            else 
            {   // rotate tree counterclockwise
                trees[i].transform.Rotate(0, -yRotation * Time.deltaTime, 0);
            }
        }
        
    }
}
