using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GridItem
{
    Color GetColor();
    void UpdateToTransparent();
    void DestroyItem();
    void SetAsDestroyed();
    int GetPositionRow();
    int GetPositionColumn();

}
