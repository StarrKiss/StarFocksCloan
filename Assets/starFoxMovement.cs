using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colData{
        public Vector2 collisionVector;
        public bool canMove;

        public colData(Vector2 colVector, bool canTempMove){
            collisionVector = colVector;
            canMove = canTempMove;
        }
    }
public class starFoxMovement : MonoBehaviour
{
    public Vector2 bounds = new Vector2(10f, 5f);

    public float accelerationTime = 0.2f;

     [HideInInspector]
    public Vector3 velocity;
 
    float velocityXSmoothing;

    float velocityZSmoothing;
    

    private float actualSpeed;

    public GameObject graphics;

    float xRotation = 15f;

    float yRotation = 10f;

    private float realRot = 0;
    
    float rotationSmoothing;
    float rotationSmoothingY;

    public float moveSpeed;

    public float rotationAccelerationTime = 10f;

    Vector3 graphicsRotation;

    public float dodgeRollLength = 4f;

    public float dodgeRollSpeed = 10f;

    private float currLength = 0f;

    private float dodgeRollVelocity;

    private bool isDodging = false;

    public boostManager bm;

    public float maxBoostLength = 3f;

    private float currBoostLength = 3f;

    public UIManager man;

    public collisionManager colMan;

    float map(float s, float a1, float a2, float b1, float b2)
{
    return b1 + (s-a1)*(b2-b1)/(a2-a1);
}

float amountToRoll = 0f;
    // Start is called before the first frame update
    void Start()
    {
        actualSpeed = moveSpeed;
        currBoostLength = maxBoostLength;
        amountToRoll = 360f/dodgeRollLength;
    
        colMan = gameObject.GetComponent<collisionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 turnMovement = Vector3.zero;
        
        turnMovement += Input.GetAxis("Horizontal") * Vector3.left;
        turnMovement += Input.GetAxis("Vertical") * Vector3.up;
        

        float targetVelocityX = turnMovement.x * actualSpeed;

        float targetVelocityZ = turnMovement.y * actualSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityZ, ref velocityZSmoothing, accelerationTime);

        if(Input.GetKeyDown(KeyCode.Joystick1Button5)){
            currLength = dodgeRollLength;
            dodgeRollVelocity = -dodgeRollSpeed;
        }
        if(Input.GetKeyDown(KeyCode.Joystick1Button4)){
            currLength = dodgeRollLength;
            dodgeRollVelocity = dodgeRollSpeed;
        }

        if(Input.GetKey(KeyCode.Joystick1Button1)){
            if(currBoostLength > 0 ){
                bm.isBoosting = true;
                currBoostLength = currBoostLength - Time.deltaTime;
            } else{
                bm.isBoosting = false;
            }
        } else{
            bm.isBoosting = false;
            if(currBoostLength < maxBoostLength){
                currBoostLength = currBoostLength + (Time.deltaTime * 0.25f);
            }
        }

        man.updateGauge(currBoostLength/maxBoostLength);

        if(currLength > 0){
            isDodging  = true;
            currLength = currLength - Time.deltaTime;
            velocity.x = dodgeRollVelocity;
        }
        else{
            isDodging = false;
        }

        

        

        Vector3 normVel = Vector3.Normalize(velocity);

        float targetRotation = 0f;
        if(!isDodging){
             targetRotation = xRotation * Input.GetAxis("Horizontal");
        }
        else{
            targetRotation = graphicsRotation.z + (amountToRoll * Time.deltaTime * -Mathf.Sign(dodgeRollVelocity));
        }
        
        

        float targetYRotation = yRotation * Input.GetAxis("Vertical");
        if(!isDodging){
            graphicsRotation.z = Mathf.SmoothDamp(graphicsRotation.z, targetRotation, ref rotationSmoothing, rotationAccelerationTime);
        }
        graphicsRotation.z = targetRotation;
        graphicsRotation.x = Mathf.SmoothDamp(graphicsRotation.x, targetYRotation, ref rotationSmoothingY, rotationAccelerationTime);
        

        graphics.transform.localRotation = Quaternion.Euler(graphicsRotation);

        move();
    }

    void move() {
        /*
        
        if(!checkForBounds(transform.localPosition,velocity).canMove){
            transform.localPosition += velocity * Time.deltaTime;
            
        }else{
            
            velocity = Vector3.zero;
        }
        */

        transform.localPosition += colMan.checkMovement(velocity * Time.deltaTime);
        
        
        
    }



    colData checkForBounds(Vector3 curPos, Vector3 curVel){
        Vector3 tempMove = curPos + (curVel * Time.deltaTime);
        colData TemporaryCol = new colData(new Vector2(0,0), false);
        
        if(tempMove.x > bounds.x || tempMove.x < -bounds.x){
            TemporaryCol.canMove = true;
        }
        else{
            
        }

        if(tempMove.y > bounds.y || tempMove.y < -bounds.y + 12){
            TemporaryCol.canMove = true;   
        }

        return TemporaryCol;
    }
    
    
    
}
