using System;
using System.Collections.Generic;
using UnityEngine;

public class HomePageView : MonoBehaviour
{
    [SerializeField] private MissionItem misItemPrefab;
    [SerializeField] private Transform content;
    public void Show(List<MissionData> missions)
    {
        foreach (var item in missions)
        {
            var misItem = Instantiate(misItemPrefab, content);
            misItem.Show(item);
        }
    }
}
