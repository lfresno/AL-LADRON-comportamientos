using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 distanceToPlayer;
    private bool lookingBack = false;

    void Start()
    {
        distanceToPlayer = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) || lookingBack) {
            //if player presses Space, the camera will move a little further so that they can see the animals following
            transform.position = player.transform.position + distanceToPlayer + new Vector3(0, 2, -6);
            StartCoroutine(LookBack());
        } else {
            //camera will follow player 
            transform.position = player.transform.position + distanceToPlayer;
        }
    }

    private IEnumerator LookBack() {
        lookingBack = true;
        float t = Time.realtimeSinceStartup;
        Time.timeScale = 0f;

        while(Time.realtimeSinceStartup - t < 3.0f){
            yield return 0;
        }
        
        lookingBack = false;
        Time.timeScale = 1.0f;
        Debug.Log("finished coroutine camera");
    }
}
