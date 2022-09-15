using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToRemove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator waitToRemove(){
        yield return new WaitForSecondsRealtime(0.62f);
        Destroy(gameObject);
    }
}
