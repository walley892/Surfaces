using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSurfaceRenderer: MonoBehaviour{
    
    protected AbstractSurface _surface;
    protected Mesh _mesh;
    protected List<Vector3> _vertices;
    protected List<int> _triangles;
    protected List<Color32> _colors;
    
    
    public void Awake(){
        gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ().material = new Material (Shader.Find ("vertexShader"));
		_mesh = GetComponent<MeshFilter> ().mesh;
    }
    
    public void setSurface(AbstractSurface m){
        _surface = m;
    }
    
    public void Update(){
        render();
    }
    
    public abstract void readyRender();
    
    public void render(){
        readyRender();
        
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0, false);
        _mesh.SetColors(_colors);
        System.GC.Collect();
    }
    
}