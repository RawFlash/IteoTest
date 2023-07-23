using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyScrollController : FancyScrollView<SliderInfoFancy>
{
    [SerializeField] Scroller scroller;
    [SerializeField] GameObject cellPrefab;

    protected override GameObject CellPrefab => cellPrefab;

    void Start()
    {
        scroller.OnValueChanged(base.UpdatePosition);
    }

    public void UpdateData(IList<SliderInfoFancy> items)
    {
        base.UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }
}
