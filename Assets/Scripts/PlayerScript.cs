using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rigid;
    public bool onGround;
    RaycastHit ground;
    public CapsuleCollider col;
    public GameObject grabPoint;
    public GameObject mainCamera;
    public GameObject grabbedObj;
    public Vector2 speedBoost;
    public Vector3 aBitAbove;
    public GameObject forwardTarget;
    public GameObject rightTarget;
    public Vector3 toForward;
    public Vector3 toRight;
    public Vector3 respPos;
    public Vector3 respRot;
    public GameObject blackHoleVFX;
    public float lastShot;
    public GameObject gun;
    public List<Vector4> blackHolePosTimes;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        respPos = transform.position;
        respRot = transform.eulerAngles;
        blackHolePosTimes = new List<Vector4>();
    }

    // Update is called once per frame
    void Update()
    {
        // rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, Input.GetAxis("Vertical") * 5);
        rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        toForward = forwardTarget.transform.position - transform.position;
        rigid.velocity += (toForward * 10 * Input.GetAxis("Vertical"));
        toRight = rightTarget.transform.position - transform.position;
        rigid.velocity += (toRight * 10 * Input.GetAxis("Horizontal"));
        // transform.Translate(Input.GetAxis("Horizontal") * 5 * Time.deltaTime, 0, Input.GetAxis("Vertical") * 5 * Time.deltaTime);
        // transform.Translate(speedBoost.x * Time.deltaTime, 0, speedBoost.y * Time.deltaTime);
        rigid.velocity += (toForward * speedBoost.y);
        rigid.velocity += (toRight * speedBoost.x);
        aBitAbove = transform.position + Vector3.up;
        Ray landingRay = new Ray (transform.position, Vector2.down);
        if(Input.GetKey(KeyCode.LeftControl))
            onGround = Physics.Raycast(landingRay, out ground, 0.4f);
        else
            onGround = Physics.SphereCast(transform.position, 0.8f, Vector3.down, out ground, 0.8f);
        Debug.Log(ground);
        if(onGround && ground.collider.gameObject.tag == "death"){
            transform.position = respPos;
            transform.eulerAngles = respRot;
        }
        if(onGround && Input.GetKeyDown(KeyCode.Space))
            rigid.AddRelativeForce(0, 10000, 0);
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
            if(speedBoost != Vector2.zero){
                if(speedBoost.x >= 0)
                    speedBoost = new Vector2(speedBoost.x - Mathf.Min(speedBoost.x, 20 * Time.deltaTime), speedBoost.y);
                else
                    speedBoost = new Vector2(speedBoost.x - Mathf.Max(speedBoost.x, -20 * Time.deltaTime), speedBoost.y);
                if(speedBoost.y >= 0)
                    speedBoost = new Vector2(speedBoost.x, speedBoost.y - Mathf.Min(speedBoost.y, 20 * Time.deltaTime));
                else
                    speedBoost = new Vector2(speedBoost.x, speedBoost.y - Mathf.Max(speedBoost.y, -20 * Time.deltaTime));
            }
        }
        if(onGround){
            if(speedBoost != Vector2.zero){
                if(speedBoost.x >= 0)
                    speedBoost = new Vector2(speedBoost.x - Mathf.Min(speedBoost.x, 15 * Time.deltaTime), speedBoost.y);
                else
                    speedBoost = new Vector2(speedBoost.x - Mathf.Max(speedBoost.x, -15 * Time.deltaTime), speedBoost.y);
                if(speedBoost.y >= 0)
                    speedBoost = new Vector2(speedBoost.x, speedBoost.y - Mathf.Min(speedBoost.y, 15 * Time.deltaTime));
                else
                    speedBoost = new Vector2(speedBoost.x, speedBoost.y - Mathf.Max(speedBoost.y, -15 * Time.deltaTime));
            }
        }
        if(Input.GetKey(KeyCode.LeftControl)){
            col.height = 1.5f;
            col.center = new Vector3(0, 0.375f, 0);
            if(!onGround){
                speedBoost = new Vector2(speedBoost.x + Input.GetAxis("Horizontal") * 5 * Time.deltaTime, speedBoost.y + Input.GetAxis("Vertical") * 5 * Time.deltaTime);
            }
        }
        else{
            col.height = 2.5f;
            col.center = new Vector3(0, 0, 0);
            if(speedBoost != Vector2.zero){
                if(speedBoost.x >= 0)
                    speedBoost = new Vector2(speedBoost.x - Mathf.Min(speedBoost.x, 10 * Time.deltaTime), speedBoost.y);
                else
                    speedBoost = new Vector2(speedBoost.x - Mathf.Max(speedBoost.x, -10 * Time.deltaTime), speedBoost.y);
            }
        }
        if(Input.GetMouseButtonDown(0) && Time.time - lastShot >= 0.42f){
            Vector3 vectorToTarget = grabPoint.transform.position - mainCamera.transform.position;
            GameObject instantiatedBlackhole = (GameObject)Instantiate(blackHoleVFX);
            Vector3 blackHolePos = mainCamera.GetComponent<CamControl>().shootBlackHole(vectorToTarget);
            instantiatedBlackhole.transform.position = blackHolePos;
            lastShot = Time.time;
            gun.GetComponent<Animator>().SetTrigger("Shoot");
            Vector4 curPosAndTime = new Vector4(blackHolePos.x, blackHolePos.y, blackHolePos.z, lastShot);
            blackHolePosTimes.Add(curPosAndTime);
        }
        foreach(Vector4 v in blackHolePosTimes){
            if(Time.time - v.w >= 1.125f){
                blackHolePosTimes.Remove(v);
            }
            else{
                float dist = Vector3.Distance(transform.position, new Vector3(v.x, v.y, v.z));
                Vector3 velToAdd = new Vector3((((v.x - transform.position.x) * 20) / (dist * dist)) / ((Time.time - v.w) * (Time.time - v.w) + 0.1f), (((v.y - transform.position.y) * 20) / (dist * dist)) / ((Time.time - v.w) * 20000 + 0.1f), (((v.z - transform.position.z) * 20) / (dist * dist)) / ((Time.time - v.w) * (Time.time - v.w) + 0.1f));
                rigid.velocity += velToAdd;
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(grabbedObj == null){
                Vector3 vectorToTarget = grabPoint.transform.position - mainCamera.transform.position;
                grabbedObj = mainCamera.GetComponent<CamControl>().GrabObjCheck(vectorToTarget * 1.1f);
            }
            else{
                grabbedObj = null;
            }
        }
        if(grabbedObj != null){
            // grabbedObj.transform.parent = grabPoint.transform;
            // grabbedObj.transform.position = grabPoint.transform.position;
            grabbedObj.GetComponent<Rigidbody>().velocity = (grabPoint.transform.position - grabbedObj.transform.position) * 10;
        }
    }
}
