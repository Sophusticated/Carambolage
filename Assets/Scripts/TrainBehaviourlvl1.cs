using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrainBehaviourlvl1 : MonoBehaviour


{
    public static float speedConst;
    private bool isCurrentlyColliding = false;
    public GameObject colliderObj;
    private bool goingUp;
    private bool goingRight;
    private bool goingDown;
    private bool goingLeft;
    private bool hasCollided = false;
    

    public static float speedVar;

    AudioManager audioManager;


    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "Turn"){
            isCurrentlyColliding = true;
            colliderObj = col.gameObject;
        }
        else if(col.transform.parent.name == "Houses"){
            if (gameObject.name == col.name && General.gameOver == false){
                General.Score += 1;
                General.arrivedTrains += 1;
                audioManager.PlaySFX(audioManager.dingSound);
                Destroy(gameObject);
            }
            else if(gameObject.name != col.name && General.gameOver == false) {
                General.arrivedTrains += 1;
                Destroy(gameObject);
            }

        }  
    }
    
    void OnTriggerExit2D(Collider2D col) {
        isCurrentlyColliding = false;
        hasCollided = false;
    }

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        isCurrentlyColliding = false;

        hasCollided = false;
        if (General.levelNo == 1)
        {
            speedConst = 0.8f;
            goingUp = false;
            goingRight = true;
            goingDown = false;
            goingLeft = false;
        }
        if (General.levelNo == 2)
        {
            speedConst = 1f;
            goingUp = false;
            goingRight = false;
            goingDown = false;
            goingLeft = true;
        }
        speedVar = speedConst;
    }

    //make a small gameobject at each switchtrack, then each frame, check for collision. if the train collides, 
    //switch the direction to the new destination depending on the state of the switch which must be constantly updated in a public variable.

    private void FixedUpdate() {

        //checks for all goingup conditions
        if(!hasCollided && isCurrentlyColliding && ((colliderObj.name == "Turnupsolo") || (colliderObj.name == "Turnupright" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 1) || (colliderObj.name == "Turnupleft" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 1) || (colliderObj.name == "Turnupdown" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 1))){
            goingUp = true;
            goingRight = false;
            goingDown = false;
            goingLeft = false;
            hasCollided = true;
            }
    
        else if (!hasCollided && isCurrentlyColliding && ((colliderObj.name == "Turnrightsolo") || (colliderObj.name == "Turnupright" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 2) || (colliderObj.name == "Turndownright" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 2) || (colliderObj.name == "Turnrightleft" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 1))){
            goingRight = true;
            goingUp = false;
            goingDown = false;
            goingLeft = false;
            hasCollided = true;
        }
        else if (!hasCollided && isCurrentlyColliding && ((colliderObj.name == "Turndownsolo") || (colliderObj.name == "Turnupdown" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 2) || (colliderObj.name == "Turndownright" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 1) || (colliderObj.name == "Turndownleft" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 1))){
            goingRight = false;
            goingUp = false;
            goingDown = true;
            goingLeft = false;
            hasCollided = true;
        }
        else if (!hasCollided && isCurrentlyColliding && ((colliderObj.name == "Turnleftsolo") || (colliderObj.name == "Turnupleft" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 2) || (colliderObj.name == "Turndownleft" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 2) || (colliderObj.name == "Turnrightleft" && colliderObj.transform.parent.gameObject.GetComponent<Tilerotator>().currentTile == 2))){
            goingRight = false;
            goingUp = false;
            goingDown = false;
            goingLeft = true;
            hasCollided = true;
        }


        if(goingUp){
            //takes start position, end position, and speed  
            transform.position = Vector2.MoveTowards (transform.position, new Vector2 (transform.position.x, 100f) , speedVar * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if(goingRight){
            transform.position = Vector2.MoveTowards (transform.position, new Vector2 (100f, transform.position.y) , speedVar * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if(goingDown){ 
            //going down, now make logic so it knows which way to go
            transform.position = Vector2.MoveTowards (transform.position, new Vector2 (transform.position.x, -100f) , speedVar * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(goingLeft){ 
            transform.position = Vector2.MoveTowards (transform.position, new Vector2 (-100f, transform.position.y) , speedVar * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        }


}
