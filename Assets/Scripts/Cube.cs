using UnityEngine;

public class Cube : MonoBehaviour
{
    //set two positions for the cube move to & from
    public Transform pos1, pos2;
    //set start postion for the cube
    public Transform startPos;
    //set cube movement speed
    public float speed;
    public GameController gameController;
    //set cube movement destination
    private Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //if the cube reaches its destination, than set a next destination
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }

        //if the cube reaches its destination, than set a next destination
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed);
    }

    void OnCollisionEnter(Collision col)
    {   
        //Deduct cube count in GameController
        gameController.cubeCnt--;
       
        //Destroy cube on collision with the Shell
        Destroy(gameObject);
    }
}
