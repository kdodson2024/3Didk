using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float rot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 5.2f);
        rot += -4 * Input.GetAxis("Mouse Y");
        rot = Mathf.Clamp(rot, -89.5f, 89.5f);
        transform.eulerAngles = new Vector3(rot, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    public GameObject GrabObjCheck(Vector3 vectorToTarget){
        RaycastHit hit;
        Ray checkRay = new Ray (transform.position, vectorToTarget * 1.5f);
        if(Physics.Raycast(checkRay, out hit, 10.4f)){
            Debug.Log(hit.collider.gameObject);
            if(hit.collider.tag == "grab"){
                return hit.collider.gameObject;
            }
            else{
                return null;
            }
        }
        Debug.Log("hit nothing");
        return null;
    }
    public Vector3 shootBlackHole(Vector3 vectorToTarget){
        Ray checkRay = new Ray (transform.position, vectorToTarget * 200f);
        RaycastHit pointHit;
        Physics.Raycast(checkRay, out pointHit, 200f);
        return pointHit.point;
    }
}