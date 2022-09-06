using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horispeedindc : MonoBehaviour
{
    public PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3((4/Mathf.PI) * Mathf.Atan(player.speedBoost.x / 5), 1, 1);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(713 + ((transform.localScale.x - 1) * 50), -301);
    }
}
