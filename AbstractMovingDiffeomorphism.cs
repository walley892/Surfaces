using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMovingDiffeomorphism: ITimeDiffeomorphism{
    
    //The derivative delta
    public float _delta = 0.01f;
    
    
    public float _uMin, _uMax, _vMin, _vMax;
    
    /*
    For optimization purposes
    */
    private float _lastCalledX, _lastCalledY;
    private Vector3 _lastUnitNormal;
    
	public abstract Vector3 call(Vector2 point, float time);
   
   public float[] getUVBounds(){
       return new float[] {_uMin, _uMax, _vMin, _vMax};
   }

	public Vector3 covariant(Vector2 point, Vector3 direction, float time){
		Vector3 normal = unitNormalR(point, time);
		
		Vector3 cpt = call(point, time);
		
		Vector2 next_pt = inverseOfQAroundP(point, cpt + (direction *_delta), cpt, time);
		
		return (unitNormalR(next_pt, time) - normal)* (1/_delta);
	}
	
	public Vector3 shapeOperator(Vector2 point, Vector3 direction, float time){
		return -1*covariant(point, direction, time);
	}
	
	public float normalCurvature(Vector2 point,Vector3 direction, float time){
		return vec3Dot(direction, shapeOperator(point, direction, time)) / vec3Dot(direction, direction);
	}
    
    public Vector3 uVelocity(Vector2 point,float  time){
        Vector3 cpt = call(point, time);
        Vector3 cdu = call(point + new Vector2(_delta, 0), time);
        float scale = 1/_delta;
        return new Vector3(scale* (cdu.x - cpt.x), scale* (cdu.y - cpt.y), scale* (cdu.z - cpt.z));
        ;
	}
	
	public Vector3 vVelocity(Vector2 point, float time){
        Vector3 cpt = call(point, time);
        Vector3 cdu = call(point + new Vector2(0, _delta), time);
        float scale = 1/_delta;
        return new Vector3(scale* (cdu.x - cpt.x), scale* (cdu.y - cpt.y), scale* (cdu.z - cpt.z));
	}
	
	public Vector3 unitNormalR(Vector2 point, float time){
       Vector3 uvel = uVelocity(point, time);
       Vector3 vvel = vVelocity(point, time);
       return new Vector3(vvel.y*uvel.z - vvel.z*uvel.y, vvel.z*uvel.x - vvel.x*uvel.z, vvel.x*uvel.y - vvel.y*uvel.x).normalized;
    
	}

	public float meanCurvature(Vector2 point, float time){
		Vector3 uVel = uVelocity(point, time);
		Vector3 vVel = vVelocity(point, time);
		
		float E = Vector3.Dot(uVel, uVel);
		float G = Vector3.Dot(vVel, vVel);
		float F = Vector3.Dot(uVel,vVel);
		
		Vector3 shapeU = shapeOperator(point, uVel, time);
		Vector3 shapeV = shapeOperator(point, vVel, time);
		
		float L = Vector3.Dot(shapeU, uVel);
		float N = Vector3.Dot(shapeV, vVel);
		float M = Vector3.Dot(shapeV, uVel);
		
		return (G*L + E*N - 2*F*M)/ (2*(E*G - F*F));
	}
    
	public float  gaussianCurvature(Vector2 point, float time){
		Vector3 uVel = uVelocity(point, time);
		Vector3 vVel = vVelocity(point, time);
		
		float E = vec3Dot(uVel, uVel);
		float G = vec3Dot(vVel, vVel);
		float F = vec3Dot(uVel,vVel);
		
		Vector3 shapeU = shapeOperator(point, uVel, time);
		Vector3 shapeV = shapeOperator(point, vVel, time);
		
		float L = vec3Dot(shapeU, uVel);
		float N = vec3Dot(shapeV, vVel);
		float M = vec3Dot(shapeV, uVel);
		
		return ( (L*N) - (M*M) ) / ( (E*G) - (F*F) );
	}
    
    
    
    /*
    Helper methods
    */
    
    /*
    Given a point P in u-v space, and a tangent vector to R3 at f(P) Vec, 
    return the projection of vec onto the tangent space of the surface at f(P)
    */
    	public Vector3 tangentSpaceProjection(Vector2 point, Vector3 vec, float time){
		Vector3 normal = unitNormalR(point, time);
        float dt = vec.x*normal.x + vec.y * normal.y + vec.z * normal.z;
        return new Vector3(vec.x - dt*normal.x, vec.y - dt*normal.y, vec.z - dt*normal.z);
	}
	
    /*
    Given a known point on the surface with known u-v coordinates, estimate the 
    u-v coordinates of q \in R3
    */
	public Vector2 inverseOfQAroundP(Vector2 pinv, Vector3 q, Vector3 p, float time){
        Vector3 tsp = tangentSpaceProjection(pinv, q, time);
		Vector3 v = new Vector3(p.x - tsp.x, p.y - tsp.y, p.z - tsp.z);
        Vector3 uVel = uVelocity(pinv, time);
        Vector3 vVel = vVelocity(pinv, time);
		Vector2 vInv = new Vector2(
			v.x*uVel.x + v.y*uVel.y + v.z*uVel.z,
			v.x*vVel.x + v.y*vVel.y + v.z*vVel.z
		);
		
		return new Vector2(pinv.x + vInv.x, pinv.y + vInv.y);
	}
    
    public float vec3Dot(Vector3 a, Vector3 b){
        return a.x*b.x + a.y*b.y + a.z*b.z;
    }
    
}