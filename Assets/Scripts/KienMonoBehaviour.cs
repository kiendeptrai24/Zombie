using UnityEngine;

public abstract class KienMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void LoadComponent() { }
    private void Reset() => LoadComponent();
}