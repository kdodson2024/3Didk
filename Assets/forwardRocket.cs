using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardRocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 200 * Time.deltaTime);
        RaycastHit ground;
        if(Physics.SphereCast(transform.position, 0.8f, Vector3.forward, out ground, 0.8f))
            Destroy(gameObject);
    }
    public void OnCollisionEnter(Collision collision){
        
    }
}