using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionManager : MonoBehaviour
{

    public LayerMask blockMovement;
    public Bounds colliderBounds;

    public BoxCollider col;

    public int numberOfCastsX;

    public int numberOfCastsY;
    public int numberOfCastsZ;

    //----------
    private float castSpacingX;
    private float castSpacingY;
    private float castSpacingZ;

   
    // Start is called before the first frame update
    void Start()
    {
        colliderBounds = col.bounds;

        colliderBounds.max = col.size/2;
        colliderBounds.min = -col.size/2;
        castSpacingX = (colliderBounds.extents.x * 2) / numberOfCastsX;
        castSpacingY = (colliderBounds.extents.y * 2) / numberOfCastsY;
        castSpacingZ = (colliderBounds.extents.z * 2) / numberOfCastsZ;        
    }

    private void Update() {
        
        
    }

    // Update is called once per frame
    public Vector3 checkMovement(Vector3 offset){
        
        Vector3 finalOutput = offset;

        float finalY = offset.y;
        float inputY = offset.y;

        float finalX = offset.x;
        float inputX = offset.x;

        Vector3 down = gameObject.transform.up * -1f;
        Vector3 up = gameObject.transform.up;

        Vector3 right = gameObject.transform.right;
        Vector3 left = gameObject.transform.right * -1f;


        if(offset.magnitude > 0.01f){
            if(offset.y != 0){         
                for(int x = 0; x < numberOfCastsX; x++){
                    for(int z =0; z <numberOfCastsZ; z++){
                        RaycastHit hit;
                        Vector3 posToCheck = new Vector3(colliderBounds.min.x + ((x+0.25f) * castSpacingX), (Mathf.Sign(offset.y) == -1)?colliderBounds.min.y:colliderBounds.max.y, colliderBounds.min.z + ((z +0.25f)* castSpacingZ));
                        posToCheck = transform.TransformPoint(posToCheck);
                        if(Physics.Raycast(posToCheck, ((Mathf.Sign(inputY) == -1)?down:up),out hit, 20f, blockMovement)){
                            Debug.DrawRay(posToCheck, ((Mathf.Sign(inputY) == -1)?down:up) * hit.distance , Color.red);
                        
                            if(Mathf.Abs(inputY) > (hit.distance - colliderBounds.extents.y)){
                                finalY = 0; //((hit.distance) - 0.5f) * Mathf.Sign(inputY);
                                
                            }  
                        }else{
                            Debug.DrawRay(posToCheck, ((Mathf.Sign(inputY) == -1)?down:up) * 5, Color.green);
                        }
                
                    
                }
            }
        }
            if(offset.x != 0){
                for(int z = 0; z<numberOfCastsZ; z++){
                    for(int y = 0; y <numberOfCastsY; y++){
                        

                        RaycastHit hit;
                        Vector3 posToCheck = new Vector3((Mathf.Sign(offset.x) == -1)?colliderBounds.min.x:colliderBounds.max.x, colliderBounds.min.y + ((y)* castSpacingY), colliderBounds.min.z + ((z +0.25f)* castSpacingZ));
                        posToCheck = transform.TransformPoint(posToCheck);
                        if(Physics.Raycast(posToCheck, ((Mathf.Sign(inputX) == -1)?left:right),out hit, 20f, blockMovement)){
                            Debug.DrawRay(posToCheck, ((Mathf.Sign(inputX) == -1)?left:right) * hit.distance , Color.red);
                        
                            if(Mathf.Abs(inputX) > (hit.distance - colliderBounds.extents.x)-0.01f){
                                finalX = 0; //((hit.distance) - 0.5f) * Mathf.Sign(inputX);
                                
                            }  
                        }else{
                            Debug.DrawRay(posToCheck, ((Mathf.Sign(inputX) == -1)?left:right) * 5, Color.green);
                        }
                        

                    }
                }
            }
        }else{
            finalOutput = Vector3.zero;
        }

        

        finalOutput.y = finalY;
        finalOutput.x = finalX;
        
        return finalOutput;
    }

    

    void OnDrawGizmosSelected()
    {
        /*
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(posToPlaceTop, 1);
        Gizmos.DrawSphere(posToPlaceBottom, 1);

        foreach(Vector3 posToDraw in placesToCastX){
            
            Gizmos.DrawSphere(posToDraw, 0.3f);
            
        }
        foreach(Vector3 posToDraw in placesToCastY){
            
            Gizmos.DrawSphere(posToDraw, 0.3f);
            
        }
        */

        
    }

}
