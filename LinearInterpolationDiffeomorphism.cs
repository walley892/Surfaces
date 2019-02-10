using UnityEngine;

public class LinearInterpolationDiffeomorpism: AbstractMovingDiffeomorphism{
    private ITimeDiffeomorphism _d1, _d2;
    
    public LinearInterpolationDiffeomorpism(ITimeDiffeomorphism d1, ITimeDiffeomorphism d2){
        _d1 = d1;
        _d2 = d2;
    }
    
    public override Vector3 call(Vector2 pt, float time){
        return _d1.call(pt, time)*(1 - Mathf.Abs(Mathf.Sin(time))) + Mathf.Abs(Mathf.Sin(time))*_d2.call(pt, time);
    }
}