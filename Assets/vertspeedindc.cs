using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertspeedindc : MonoBehaviour
{
    public PlayerScript player;
    public GameObject noSpeedIndc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, (4/Mathf.PI) * Mathf.Atan(player.speedBoost.y / 5), 1);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(663, -251 + ((transform.localScale.y - 1) * 50));
        noSpeedIndc.SetActive(player.speedBoost == Vector2.zero);
    }
}
