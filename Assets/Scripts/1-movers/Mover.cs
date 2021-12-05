using UnityEngine;

/**
 * This component moves its object in a fixed velocity.
 * NOTE: velocity is defined as speed+direction.
 *       speed is a number; velocity is a vector.
 */
public class Mover: MonoBehaviour {
    [Tooltip("Movement vector in meters per second")]
    [SerializeField] Vector3 velocity;
    private Vector2 screenBounds;
    private int offset = 9;
    void Start(){
        screenBounds = new Vector2(Camera.main.transform.position.x+offset, Camera.main.transform.position.y+offset);
        
    }
    void Update() {
        Debug.Log(screenBounds.x+" "+ screenBounds.y);
        transform.position += velocity * Time.deltaTime;
        if(this.transform.position.y > screenBounds.y){
            GameObject temp = GameObject.Find("LaserWithScoreAdder(Clone)");
            Destroy(temp);
        }
         if(this.transform.position.y < -screenBounds.y){
            GameObject temp = GameObject.Find("EnemySaucerWithColliderAndDestruction(Clone)");
            Destroy(temp);
        }
        
    }

    public void SetVelocity(Vector3 newVelocity) {
        this.velocity = newVelocity;
    }
   
}
