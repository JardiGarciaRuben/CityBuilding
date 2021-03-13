using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

     UIGeneral uIGeneral;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (Time.timeScale == 1){
                PauseGame();
                //uIGeneral.SetActive(true);
             } else{
                ResumeGame();
                //uIGeneral.SetActive(false);
            }
        }

    }


    void PauseGame ()
    {
        Time.timeScale = 0;
    }

    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
