using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component moves its object when the player clicks the arrow keys.
 */
public class KeyboardMover: MonoBehaviour {
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] float speed = 1f;
    private float offsetx = 6.5f;
    private float offsety = 3.5f;
    private Vector2 screenBounds;
  
    void Update() {
        float horizontal = Input.GetAxis("Horizontal"); // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise
        float vertical = Input.GetAxis("Vertical");     // +1 if up arrow is pushed, -1 if down arrow is pushed, 0 otherwise
        Vector3 movementVector = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        transform.position += movementVector;
    }
      void Start(){
        screenBounds = new Vector2(Camera.main.transform.position.x+offsetx, Camera.main.transform.position.y+offsety);
        
    }
        void LateUpdate(){
        if (this.transform.position.x <= -screenBounds.x){
            this.transform.position = new Vector3(-screenBounds.x, transform.position.y, transform.position.z);
        }
        else if (this.transform.position.x >= screenBounds.x){
            this.transform.position = new Vector3(screenBounds.x, transform.position.y, transform.position.z);
        }
        if (this.transform.position.y <= -screenBounds.y){
            this.transform.position = new Vector3(transform.position.x, -screenBounds.y , transform.position.z);
        }
        else if (this.transform.position.y >= screenBounds.y){
            this.transform.position = new Vector3(transform.position.x, screenBounds.y, transform.position.z);
        }
    }
}
