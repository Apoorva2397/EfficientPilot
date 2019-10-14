using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShipLogic : MonoBehaviour {
    Rigidbody rb;
    
    [SerializeField] private float upForce;
    [SerializeField] private float TorqueSens;
    [SerializeField] private float reactivity;
    [SerializeField] float maxSpeed;
    public float seconds;
    public ParticleSystem Explode;
    private bool alive;
    
	// Use this for initialization
	void Start () {
        alive = !false;
        var Explode = GetComponent<ParticleSystem>();
        maxSpeed = 10;
        reactivity = 0.03f;
        TorqueSens = 15;
        seconds = 5;
        upForce = 15;
        rb = GetComponent<Rigidbody>();
     
        
        
	}

    // Update is called once per frame
    void Update()
    {


        thrust();
        torque();
        velClamp();
        // hack for loading next level
        if (Input.GetKeyDown(KeyCode.E)) {
            Utils.LoadNextLevel();
        } 

        
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0) {
            print("On Ground");
        }
        if (LayerMask.LayerToName(collision.gameObject.layer) == "EndPad") {
            Utils.LoadNextLevel();
        }
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Obstacle") {
            Debug.Log("Obstacle");
            StartCoroutine(killplayer(seconds));

        }
        
    }

    void thrust() {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * upForce);
        }

       
    }
    void torque() {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(new Vector3(0, 0, 1) * TorqueSens);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(new Vector3(0, 0, -1) * TorqueSens);
        }
    }
    void velClamp() {
        //for controlling the torque
        Vector3 velocity = rb.velocity;
        Vector3 proj = Vector3.Project(velocity, transform.up);
        rb.AddForce(-reactivity * (velocity - proj), ForceMode.Impulse);
        //for controlling the thrust
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            float velMag = velocity.magnitude;
            rb.AddForce(velocity.normalized * (maxSpeed - velMag), ForceMode.Impulse);
        }
    }
        public IEnumerator killplayer(float seconds) {
        float currentTime = Time.time;
        Explode.Play();
        if (currentTime < currentTime + seconds) {
            
            alive = false;
       
            yield return null;
            
        }
        Utils.ResetLevel();

    }
     
    }
  

