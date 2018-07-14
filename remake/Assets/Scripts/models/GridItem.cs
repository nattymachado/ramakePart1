using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GridItem
{
    Color GetColor();
    void UpdateToTransparent();
    void DestroyItem();
    void SetAsDestroyed();
    void PlayOnlyPillsAudio();
    int GetPositionRow();
    int GetPositionColumn();
    string GetGridItemType();
    bool FinalizedMoviment();
    bool OnlyDownMoviment();


}
