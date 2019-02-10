public abstract class AbstractSurface{

    protected ITimeDiffeomorphism[] _patches;
    
    public AbstractSurface(){
        
    }
    
    public AbstractSurface(ITimeDiffeomorphism patch){
        _patches = new ITimeDiffeomorphism[] {patch};
    }
    
    public AbstractSurface(ITimeDiffeomorphism[] patches){
        _patches = patches;
    }
    
    public void setPatches(ITimeDiffeomorphism[] p){
        _patches = p;
    }
    
    public ITimeDiffeomorphism[] getPatches(){
        return _patches;
    }
}