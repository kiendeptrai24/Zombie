using System.Collections;
using UnityEngine;

public class ZombieDissolve : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private float dissolveDuration = 1f;

    private MaterialPropertyBlock propertyBlock;

    private static readonly int DissolveAmount =
        Shader.PropertyToID("_DissolveAmount");

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }
    [ContextMenu("playds")]
    public void PlayDissolve()
    {
        StartCoroutine(DissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        float time = 0f;

        while (time < dissolveDuration)
        {
            time += Time.deltaTime;

            float value = time / dissolveDuration;

            SetDissolve(value);

            yield return null;
        }

        SetDissolve(1f);

        // Sau khi biến mất
        ObjectPool.Instance.ReturnObject(gameObject);
    }
    void OnEnable()
    {
        ResetDissolve();
    }
    public void ResetDissolve()
    {
        SetDissolve(0);
    }
    private void SetDissolve(float value)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetFloat(
                DissolveAmount,
                value
            );

            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}