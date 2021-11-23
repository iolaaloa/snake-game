//using System.Collections;// classes or objects meant for storing collections of objects-groups of objects e.g list, dictionaries,linkedlist, arrays
using System.Collections.Generic;// Same as above but lets you specify the type of object being stored.
using System.Linq;
//using System.Threading;
using UnityEngine;

//Specifying paths to a System library

//Colon in C-sharp means 2 things -> inherits from ,in this case Sanke control inherits from "Monobehavior
//In Unity you must always extend MonoBehavior to be able to use game object
//In other cases it means implement what is on the right of the colon
//Classes can be public- for all access every where; private 
//only classes in this script can access it; protected - only children of this class can access it.
public class SnakeControl : MonoBehaviour 
{
    public GameObject snakeSegment;
    private List <GameObject> snakeSegments = new List <GameObject> ();
    public GameObject snakeFoodPrefab;
    public GameObject snakeFood;
    public int startLength;
    public const int maxX=21, maxZ=11;
    public const float interval = 0.3f;
    float timeout = interval;
    Vector3 direction = Vector3.forward;
    bool gameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        for (var i= 0; i< startLength; i++)
        {
            snakeSegments.Add (Instantiate(snakeSegment, new Vector3 (i, 0, 0), Quaternion.identity));
        }
       snakeFood = Instantiate(snakeFoodPrefab, RandomPosition(), Quaternion.identity);
       //Instatiate the food at some point in the screen
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-21,21),0,Random.Range(-9,9));
    }

    // Update is called once per frame

    // Declaring a member variable  outside "Update" function so that we can use it outside "Update" and to preserve the value
    // By convention declare member variables on top of all methods/functions
    

    void Update() 
    { 
        if (gameOver)
        {
            return;
        }
        var nextDirection= Vector3.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            nextDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            nextDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            nextDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            nextDirection = Vector3.right;
        }
        if (nextDirection!=Vector3.zero && nextDirection!= -direction)
        {
            direction=nextDirection;
        }

        timeout -= Time.deltaTime;
        //print(timeout);
        if (timeout <= 0)
        {
            timeout = interval;
        }
        else {
            return;
        }
        //How the snake grows
        if (snakeSegments[0].transform.position == snakeFood.transform.position)
        {
            snakeFood.transform.position = RandomPosition();
            snakeSegments.Insert(0,Instantiate(snakeSegment,snakeSegments[0].transform.position,Quaternion.identity));
            snakeSegments[0].transform.Translate(direction);
        }
        else 
        //How the snake moves
        {
            var lastSegment=snakeSegments.Last();
            lastSegment.transform.position=snakeSegments[0].transform.position;
            snakeSegments.Remove(lastSegment);
            snakeSegments.Insert(0,lastSegment);
            snakeSegments[0].transform.Translate(direction);
        }
        // Edges + boundaries
        if (Mathf.Abs(snakeSegments[0].transform.position.x) > maxX )
        {
            snakeSegments[0].transform.position = new Vector3(-(snakeSegments[0].transform.position.x - direction.x), 0, snakeSegments[0].transform.position.z); 
        }
        else if (Mathf.Abs(snakeSegments[0].transform.position.z) > maxZ )
        {
            snakeSegments[0].transform.position = new Vector3(snakeSegments[0].transform.position.x, 0, -(snakeSegments[0].transform.position.z - direction.z)); 
        }
        //HW: try SnakeSegments[0].transform.position= newVector3(-snakeSegments[0].transform.position-direction)

        //If the snake touches itself game over

        for (var i=1; i<snakeSegments.Count; i++)
        {
            if (snakeSegments[0].transform.position == snakeSegments[i].transform.position)
            {
                gameOver=true;
            }
        }
        


        print(snakeSegments[0].transform.position);


        //snakeSegments[5].transform.Translate(-direction);
        //SNAKE TOUCHES ITSELF- DIE!
        // for i=1, i<snakelength, i++
        // if snakeSegment[0].transform.postion= snakeSegment[i].transform.position
        // {game over}



        //Moving the snake - capturing key input
        //Class.Function.Input?
        //Capital Starter letter - Class; small starter letter - instantiated class
        // TO DO: WHEN SNAKE TOUCHES ITSELF -> DIE!
        // TO DO: TENET DOORS
        // TO DO: STORY SCENES

    }
}
