using UnityEngine;
using System.Collections;

public class Oscillation : MonoBehaviour {

    public float Period = 2f;

    private void Update()
    { float delta;
        if (Period < Mathf.Epsilon) {
            Period = Mathf.Epsilon;
        }
        delta = (Mathf.Sin(Time.timeSinceLevelLoad)+1) / 2;
        transform.position = new Vector3(transform.position.x, delta*Period ,transform.position.z );
        
    }
   
}
