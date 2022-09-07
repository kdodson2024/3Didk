using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class vertspeedindctxt : MonoBehaviour
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
        GetComponent<TextMeshProUGUI>().text = Mathf.Abs((int) player.speedBoost.y) + "";
        GetComponent<RectTransform>().anchoredPosition = new Vector2(663, -291 + ((speedIndc.transform.localScale.y / Mathf.Abs(speedIndc.transform.localScale.y)) * 30) + ((speedIndc.transform.localScale.y) * 100));
    }
}
