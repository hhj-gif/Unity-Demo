using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieEffect : MonoBehaviour
{
    Material mat;
    float amount;
    bool disappear;
    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        disappear = false;
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Resources.Load<Material>(MonsterSetting.DestoryMaterialPath);
        skinnedMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mat = skinnedMeshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (amount < 1)
        {
            mat.SetFloat("_DissolveAmount", amount);
            amount += Time.deltaTime/2;
        }
        else if (!disappear)
		{
            disappear = true;
            mat.SetFloat("_DissolveAmount", 1);
        }
    }
}
