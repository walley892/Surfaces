using UnityEngine;

public abstract class AbstractDiffeomorphism: AbstractMovingDiffeomorphism{
    
    public abstract Vector3 call(Vector2 pt);
    
    public override Vector3 call(Vector2 pt, float time){
        return call(pt);
    }
}