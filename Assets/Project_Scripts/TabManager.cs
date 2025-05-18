using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [Header("Objects related to Project 1")]
    public List<GameObject> project1Objects;

    [Header("Objects related to Project 2")]
    public List<GameObject> project2Objects;

    [Header("Objects related to Project 3")]
    public List<GameObject> project3Objects;

    [Header("Tab buttons (used to switch project)")]
    public List<GameObject> Tabs; // All tab buttons (clickable UI)

    [Header("Active Tabs (visual marker per project)")]
    public List<GameObject> ActiveTabs; // Only one should be visible at a time

    /// <summary>
    /// Show the selected project by index (1 = Project1, 2 = Project2, 3 = Project3)
    /// </summary>
    /// <param name="index">Project number to show (1-based)</param>
    public void ShowProject(int index)
    {
        // Enable/Disable project objects
        SetGroupActive(project1Objects, index == 1);
        SetGroupActive(project2Objects, index == 2);
        SetGroupActive(project3Objects, index == 3);

        // Tabs: deactivate the one for the current project, activate the others
        for (int i = 0; i < Tabs.Count; i++)
        {
            bool isCurrent = (i == index - 1);
            Tabs[i].SetActive(!isCurrent);
        }

        // Active tab indicators: activate only the one for the current project
        for (int i = 0; i < ActiveTabs.Count; i++)
        {
            bool isCurrent = (i == index - 1);
            ActiveTabs[i].SetActive(isCurrent);
        }
    }

    /// <summary>
    /// Helper to activate/deactivate a group of GameObjects
    /// </summary>
    private void SetGroupActive(List<GameObject> group, bool active)
    {
        foreach (var obj in group)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }

    /// <summary>
    /// Optional: Automatically select project 1 on startup
    /// </summary>
    private void Start()
    {
        ShowProject(2);
    }
}
