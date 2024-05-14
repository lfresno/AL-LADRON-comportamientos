using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxLife = 3;
    public int life;
    private float temp;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        temp = Time.time;
        initialPosition = this.transform.position;
        Debug.Log(initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.time - temp > 2.0f) {
        //     life--;
        //     temp = Time.time;
        //     Debug.Log(life);
        // }
    }

    public void ResetPlayer() {
        life = maxLife;
        gameObject.transform.position = initialPosition;
    }
}
