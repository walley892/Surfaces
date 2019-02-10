using UnityEngine;

public class Test: MonoBehaviour{
    public int _numTriangles;
    public float _triangleSpeed, _triangleLength, _radius;
    
    
    void Start(){
        TriangleRenderer r = gameObject.AddComponent<TriangleRenderer>();
        r.setNTriangles(_numTriangles);
        r.setTriangleSpeed(_triangleSpeed);
        r.setTriangleLength(_triangleLength);
        Sphere s1, s2;
        s1 = new Sphere(_radius);
        s2 = new Sphere(0);
        
        r.setSurface(new LinearSurfaceInterpolation(s1, s2));
    }
    
}