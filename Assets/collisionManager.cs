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

    private List<Vector3> placesToCastY = new List<Vector3>();

    private List<Vector3> placesToCastX = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        colliderBounds = col.bounds;
        castSpacingX = (colliderBounds.extents.x * 2) / numberOfCastsX;
        castSpacingY = (colliderBounds.extents.y * 2) / numberOfCastsY;
        castSpacingZ = (colliderBounds.extents.z * 2) / numberOfCastsZ;        
    }

    private void Update() {
        
        
    }

    // Update is called once per frame
    public Vector3 checkMovement(Vector3 offset){
        
        colliderBounds = col.bounds;
        Vector3 topPositions = new Vector3(colliderBounds.max.x, colliderBounds.max.y, colliderBounds.max.z);
        Vector3 bottomPositions = new Vector3(colliderBounds.min.x, colliderBounds.max.y, colliderBounds.min.z);

        float finalY = offset.y;
        float inputY = offset.y;

        if(offset.y != 0){         
            for(int x = 0; x < numberOfCastsX; x++){
                for(int z =0; z <numberOfCastsZ; z++){
                    RaycastHit hit;
                    Vector3 posToCheck = new Vector3(colliderBounds.min.x + ((x+0.25f) * castSpacingX), (Mathf.Sign(offset.y) == -1)?colliderBounds.min.y:colliderBounds.max.y, colliderBounds.min.z + ((z +0.25f)* castSpacingZ));
                    if(Physics.Raycast(posToCheck, ((Mathf.Sign(inputY) == -1)?Vector3.down:Vector3.up),out hit, 20f, blockMovement)){
                        Debug.DrawRay(posToCheck, ((Mathf.Sign(inputY) == -1)?Vector3.down:Vector3.up) * hit.distance , Color.red);
                        
                        if(Mathf.Abs(inputY) > (hit.distance - colliderBounds.extents.y)-0.01f){
                            finalY = 0;
                            Debug.Log("TRIGGERED");
                        }  
                    }else{
                        Debug.DrawRay(posToCheck, ((Mathf.Sign(inputY) == -1)?Vector3.down:Vector3.up) * 5, Color.green);
                    }
                
                    
                }
            }
        }
        if(offset.x != 0){
            for(int z = 0; z<numberOfCastsZ; z++){
                for(int y = 0; y <numberOfCastsY; y++){
                    placesToCastX.Add(new Vector3((Mathf.Sign(offset.x) == -1)?colliderBounds.min.x:colliderBounds.max.x, colliderBounds.min.y + ((y)* castSpacingY), colliderBounds.min.z + ((z +0.25f)* castSpacingZ)));
                }
            }
        }

        Vector3 finalOutput = offset;

        finalOutput.y = finalY;
        
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
