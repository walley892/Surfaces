using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRenderer: AbstractSurfaceRenderer{
    
    //Model of triangles floating on UV plane
    private Vector2[,] _centers;
    private Vector2[, ] _directions;
    private int _numTriangles;
    private float _triangleSpeed;
    private float _triangleLength;
    
    void Awake(){
        base.Awake();
        _triangles = new List<int>();
        for(int i = 0; i < _numTriangles; ++i){
            _triangles.Add(i);
        }
        
        _vertices = new List<Vector3>();
        _colors = new List<Color32>();
    }
    
    public void setNTriangles(int n){
        _numTriangles = n;
        _centers = null;
        _directions = null;
    }
    
    public void setTriangleSpeed(float s){
        _triangleSpeed = s;
    }
    
    public void setTriangleLength(float l){
        _triangleLength = l;
    }
    
    void Update(){
        if(_centers == null){
            placeTriangles();
        }
        moveTriangles();
        base.Update();
    }
    
    public void placeTriangles(){
        ITimeDiffeomorphism[] patches = _surface.getPatches();
        
        _centers = new Vector2[patches.Length, _numTriangles/patches.Length];
        _directions = new Vector2[patches.Length, _numTriangles/patches.Length];
        
        for(int i = 0; i < patches.Length; ++i){
            for(int j = 0; j < _numTriangles/patches.Length; ++j){
                _centers[i, j] = randomPointOnUVPlane(patches[i]);
                _directions[i, j] = randomUnitVector();
            }
        }
    }
    
    public void moveTriangles(){
        ITimeDiffeomorphism[] patches = _surface.getPatches();
        for(int i = 0; i < patches.Length; ++i){
            for(int j = 0; j < _numTriangles/patches.Length; ++j){
              _centers[i, j] = new Vector2(_centers[i,j].x + _directions[i,j].x*_triangleSpeed, _centers[i,j].y + _directions[i,j].y*_triangleSpeed);
            }
        }
    }
    
    
    public void readySurface(){
        ITimeDiffeomorphism[] patches = _surface.getPatches();
        
        _vertices.Clear();
        _colors.Clear();
        _triangles.Clear();
        
        for(int i = 0; i < _numTriangles * 3; ++i){
            _triangles.Add(i);
        }
        for(int i = 0; i < patches.Length; ++i){
            readyPatch(i);
        }
        
    }
    
    public void readyPatch(int patchIndex){
        ITimeDiffeomorphism patch = _surface.getPatches()[patchIndex];
        float time = Time.time;
        Debug.Log(_centers.GetLength(1));
    	    for (int i = 0; i < _centers.GetLength(1); ++i) {
            
			Vector2 uv_coord = _centers[patchIndex, i];
            
            Vector3 image = patch.call(uv_coord, time);
            
            
            Vector3[] basis = new Vector3[2];
            basis[0] = patch.uVelocity(uv_coord, time);
            basis[1] = patch.vVelocity(uv_coord, time);
            //TODO: make not ugly
			_vertices.Add(new Vector3(image.x + (_triangleLength * (Mathf.Sin(0)*basis[0].x + Mathf.Cos(0)*basis[1].x)) ,image.y + (_triangleLength * (Mathf.Sin(0)*basis[0].y + Mathf.Cos(0)*basis[1].y)) , image.z + (_triangleLength * (Mathf.Sin(0)*basis[0].z + Mathf.Cos(0)*basis[1].z))));
			_vertices.Add(new Vector3(image.x + (_triangleLength * (Mathf.Sin(2.0944f)*basis[0].x + Mathf.Cos(2.0944f)*basis[1].x)) ,image.y + (_triangleLength * (Mathf.Sin(2.0944f)*basis[0].y + Mathf.Cos(2.0944f)*basis[1].y)) , image.z + (_triangleLength * (Mathf.Sin(2.0944f)*basis[0].z + Mathf.Cos(2.0944f)*basis[1].z))));
			_vertices.Add(new Vector3(image.x + (_triangleLength * (Mathf.Sin(4.18879f)*basis[0].x + Mathf.Cos( 4.18879f)*basis[1].x)) ,image.y + (_triangleLength * (Mathf.Sin( 4.18879f)*basis[0].y + Mathf.Cos( 4.18879f)*basis[1].y)) , image.z + (_triangleLength * (Mathf.Sin( 4.18879f)*basis[0].z + Mathf.Cos( 4.18879f)*basis[1].z))));
		   
           for(int q = 0; q < 3; ++q){
              _colors.Add(Color.red);
            }
        
        }
    }
    
    public override void readyRender(){
        readySurface();
    }
    
    public Vector2 randomPointOnUVPlane(ITimeDiffeomorphism patch){
        float[] bounds = patch.getUVBounds();
        return new Vector2(Random.Range(bounds[0], bounds[1]), Random.Range(bounds[2], bounds[3]));
    }

    public Vector2 randomUnitVector(){
        return Random.insideUnitCircle;
    }
    
}