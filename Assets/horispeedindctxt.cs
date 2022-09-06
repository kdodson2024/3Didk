using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class horispeedindctxt : MonoBehaviour
{
    public PlayerScript player;
    public GameObject speedIndc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = (int) player.speedBoost.x + "";
        GetComponent<RectTransform>().anchoredPosition = new Vector2(667 + ((speedIndc.transform.localScale.x / Mathf.Abs(speedIndc.transform.localScale.x)) * 30) + ((speedIndc.transform.localScale.x) * 100), -301);
    }
}
