using UnityEngine;

public class LinearSurfaceInterpolation: AbstractSurface {

    public LinearSurfaceInterpolation(AbstractSurface m1, AbstractSurface m2){
        ITimeDiffeomorphism[] _patches1 = m1.getPatches();
        ITimeDiffeomorphism[] _patches2 = m1.getPatches();
        _patches = new ITimeDiffeomorphism[] {new LinearInterpolationDiffeomorpism(_patches1[0], _patches2[0])};
    }
    
    
    
}