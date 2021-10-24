using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class NoLight : MonoBehaviour {
 
    public Light Light;
    public bool cullLights = true;
     
    private void Start() {    
    }

    void OnPreCull()
    {
        if(cullLights == true)
            Light.enabled = false;
    }
     
    void OnPostRender()
    {
        if(cullLights == true)
            Light.enabled = true;
    }
}