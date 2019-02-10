using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeDiffeomorphism 
{
    
	Vector3 call(Vector2 point, float time);
	Vector3 uVelocity(Vector2 point, float time);
	Vector3 vVelocity(Vector2 point, float time);
	Vector3 unitNormalR(Vector2 point, float time);
    float[] getUVBounds();
    
    
	Vector3 shapeOperator(Vector2 point, Vector3 direction, float time);
    

	float normalCurvature(Vector2 point, Vector3 direction, float time);
	float meanCurvature(Vector2 point, float time);
	float gaussianCurvature(Vector2 point, float time);
    
}