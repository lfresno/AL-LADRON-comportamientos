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
        temp = Time.time;
        initialPosition = this.transform.position;
        Debug.Log(initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.State != GameState.GameState) return;
    }

    public void ResetPlayer() {
        gameObject.transform.position = initialPosition;
    }
}
