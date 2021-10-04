using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class triggerInfo{
    public bool isLeft;
    public bool isTop;
    public bool isRight;
    public bool isBottom;

    public triggerInfo(bool left, bool top, bool right, bool bottom){
        isLeft = left;
        isTop = top;
        isRight = right;
        isBottom = bottom;
    }

    public void clear(){
        isLeft = false;
        isTop = false;
        isRight = false;
        isBottom = false;
    }

}
public class triggerManager : MonoBehaviour
{
    public BoxCollider left;
    public BoxCollider right;
    public BoxCollider top;
    public BoxCollider bottom;


    triggerInfo passOn = new triggerInfo(false, false, false, false);

    public LayerMask toCollide;


    // Update is called once per frame
    void Update()
    {
        passOn.clear();

        passOn.isLeft = Physics.CheckBox(left.bounds.center, left.bounds.extents, Quaternion.identity, toCollide);
        passOn.isRight = Physics.CheckBox(right.bounds.center, right.bounds.extents, Quaternion.identity, toCollide);
        passOn.isTop = Physics.CheckBox(top.bounds.center, top.bounds.extents, Quaternion.identity, toCollide);
        passOn.isBottom = Physics.CheckBox(bottom.bounds.center, bottom.bounds.extents, Quaternion.identity, toCollide);

        Debug.Log("Left: " + passOn.isLeft + "\nRight: " + passOn.isRight + "\nTop: " + passOn.isTop + "\nBottom: " + passOn.isBottom);
    }
}
