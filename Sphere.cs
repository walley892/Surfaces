using UnityEngine;

public class Sphere: AbstractSurface{
    
    public class diffeo: AbstractDiffeomorphism{
    
        float r;
        
        public diffeo(float rad){
            r = rad;
            _uMin = 0;
            _uMax = 3.14f;
            _vMin = 0;
            _vMax = 6.28f;
        }
        
        public override Vector3 call(Vector2 pt){
            float u = pt.x;
            float v = pt.y;
            return new Vector3(r * Mathf.Sin(u) * Mathf.Cos(v), r * Mathf.Sin(u) * Mathf.Sin(v), r * Mathf.Cos(u));
        }
        
    }
    
    public Sphere(float radius): base(){
        diffeo d = new diffeo(radius);
        base.setPatches(new AbstractDiffeomorphism[] {d});
    }
    
}