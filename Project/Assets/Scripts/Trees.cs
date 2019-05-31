using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour {

    public Tree tree;
    public int number;
    public int xaxis;
    public int yaxis;
    public int zaxis;
   
    private List<Tree> trees = new List<Tree>();

    void Start()
    {
        PlaceTree();
    }

    void PlaceTree()
    {
        // spawn a number of trees
        for (int i = 0; i < number; i++)
        {  
            // add the spawned trees to a list
            trees.Add(Instantiate(tree, generateInsideCirlce(), Quaternion.Euler(0, UnityEngine.Random.Range(-xaxis, xaxis), UnityEngine.Random.Range(-50, 50))));
        }

    }

    // return a random position inside a circle 
    Vector3 generateInsideCirlce()
    {
        Vector3 newPosition = new Vector3();
        newPosition = Random.insideUnitCircle * 100;
        newPosition.x += transform.position.x;
        newPosition.z = newPosition.y + transform.position.z;
        newPosition.y = transform.position.y;

        return newPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        // for all the trees that are spawned.
        for (int i = 0; i < trees.Count; i++)
        {
            if (i % 2 == 0)
            {
                // rotate trees clockwise
                trees[i].transform.Rotate(0, 50 * Time.deltaTime, 0);
            }
            else
            {   // rotate trees counterclockwise
                trees[i].transform.Rotate(0, -50 * Time.deltaTime, 0);
            }
        }
        
    }
}
