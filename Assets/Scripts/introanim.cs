using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introanim : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToRoll());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator waitToRoll(){
        yield return new WaitForSecondsRealtime(5.0f);
        GetComponent<Rigidbody>().AddRelativeForce(500, 0, 0);
        yield return new WaitForSecondsRealtime(3.0f);
        canvas.SetActive(true);
    }
}
