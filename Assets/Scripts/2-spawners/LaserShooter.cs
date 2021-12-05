using UnityEngine;
public class LaserShooter: KeyboardSpawner {
    [SerializeField] NumberField scoreField;
    private float resttime = 1f;
    private float timeloop;
    protected override GameObject spawnObject() {
        if (Time.time >= timeloop && (Input.GetKeyDown(keyToPress))){
            GameObject newObject = base.spawnObject();  
            ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
            if (newObjectScoreAdder){
                newObjectScoreAdder.SetScoreField(scoreField);
            }
            timeloop += resttime;
            return newObject;
        } else return null;
        
        
    }
}

