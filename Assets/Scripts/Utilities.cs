﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    //Get all interfaces T of all Components attacted to GameObject objectToSearch
    public static List<T> GetInterfaces<T>(this GameObject objectToSearch) where T : class
    {
        Component[] components = objectToSearch.GetComponents<Component>();
        List<T> resultList = new List<T>();
        foreach (Component co in components)
        {
            if (co is T)
            {
                resultList.Add((T)(object)co);
            }
        }
        return resultList;
    }

    //Get the interface T of a MonoBehavior attached to GameObject objectToSearch
    public static T GetInterface<T>(this GameObject objectToSearch) where T : class
    {
        MonoBehaviour mb = objectToSearch.GetComponent<MonoBehaviour>();
        if (mb is T)
        {
            return (T)(object)mb;
        }
        else
        {
            return null;
        }
    }

    /*
    public static void DisableRaycastTarget(GameObject gameObject)
    {
        UnityEngine.EventSystems.PhysicsRaycaster raycaster = gameObject.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>();
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
    }
    public static void DisableRaycastTarget(UnityEngine.UI.Image image)
    {
        UnityEngine.EventSystems.PhysicsRaycaster raycaster = image.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>();
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
    }
    */

    //Switch to panel and make all other panels invisible
    /*
    public static void SwitchPanel(this GameObject panel)
    {
        if (!panel.CompareTag("Panel"))
        {
            throw new ArgumentException($"Specified panel(\"{panel.name}\") is not tagged with \"Panel\"");
        }
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
        foreach (GameObject p in panels)
        {
            p.GetComponent<CanvasGroup>().alpha = 0;
        }
        panel.GetComponent<CanvasGroup>().alpha = 1;
        PlayerData.Instance.ActivePanel = panel;
        panel.transform.SetAsLastSibling();//bring to front
    }
    */
    private static float from;
    private static CanvasGroup canvasGroup;
    //fade canvas group in speed seconds with steps steps
    public static IEnumerator FadeCanvasGroup(GameObject gameObject, float speed, int steps, float from, float to)
    {
        if (from > 1.0f || from < 0.0f || to > 1.0f || to < 0.0f)
        {
            throw new ArgumentException("\"from\" and \"to\" can only range from 1.0 to 0.0");
        }
        if (speed < 0.0f)
        {
            throw new ArgumentException("\"speed\" cannot be less than 0.0");
        }
        Debug.Log(Time.time);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = from;
        for (int i = 0; i < steps; i++)
        {
            canvasGroup.alpha -= (from - to) / steps;
            yield return new WaitForSeconds(speed / steps);
        }
        Debug.Log(Time.time);
    }
    //fade canvas group in speed seconds with steps steps
    public static IEnumerator FadeCanvasGroupTo(GameObject gameObject, float speed, int steps, float to)
    {
        if (to > 1.0f || to < 0.0f)
        {
            throw new ArgumentException("\"to\" can only range from 1.0 to 0.0");
        }
        if (speed < 0.0f)
        {
            throw new ArgumentException("\"speed\" cannot be less than 0.0");
        }
        Debug.Log(Time.time);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        from = canvasGroup.alpha;
        for (int i = 0; i < steps; i++)
        {
            canvasGroup.alpha -= (from - to) / steps;
            yield return new WaitForSeconds(speed / steps);
        }
        Debug.Log(Time.time);//it somehow waits too long
    }
}