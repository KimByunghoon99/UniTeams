using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class option_controller : MonoBehaviour
{
    private bool state;
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (state == true){
                Target.SetActive(false);
                state = false;
            }
            else{
                Target.SetActive(true);
                state = true;
            }
        }
    }
}
