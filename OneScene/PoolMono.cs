using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono <T> where T : MonoBehaviour
{
    private T _prefab;

    private Transform _container;
    public bool autoSize;

    private List<T> _poolList;

    public PoolMono(T prefab,int count, Transform container)
    {
        _prefab = prefab;
        _container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _poolList = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    T CreateObject(bool isActive = false)                                                           
    {
        var objectPool = Object.Instantiate(_prefab, _container);
        objectPool.gameObject.SetActive(isActive);
        _poolList.Add(objectPool);
        return objectPool;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in _poolList)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                mono.gameObject.SetActive(true);
                element = mono;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (autoSize)
        {
            return CreateObject(true);
        }

        throw new Exception($"Нехватка обьектов или авторасширение выключено {typeof(T)} ");
    }
}
