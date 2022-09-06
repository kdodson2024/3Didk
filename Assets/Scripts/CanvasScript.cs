using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;
public class CanvasScript : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLevel(int value){
        QualitySettings.SetQualityLevel(value);
    }
    public void loadGame(){
        SceneManager.LoadScene(1);
    }
}
