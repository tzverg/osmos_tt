using UnityEngine;
using System.Collections.Generic;

public class ObjectsPool : MonoBehaviour
{
    private List<ObjectForPool> objects;
    private Transform objectsParent;

    public void Initialize(int count, ObjectForPool sample, Transform _objectsParent)
    {
        objects = new List<ObjectForPool>();
        objectsParent = _objectsParent;

        for (int i = 0; i < count; i++)
        {
            AddObject(sample, _objectsParent, i);
        }
    }

    public void AddObject(ObjectForPool sample, Transform _objectsParent, int objectID)
    {
        GameObject temp = Instantiate(sample.gameObject);
        temp.name = sample.name + "_" + objectID;
        temp.transform.SetParent(_objectsParent);
        objects.Add(temp.GetComponent<ObjectForPool>());

        Animator objectAnimator = temp.GetComponent<Animator>();

        if (objectAnimator != null)
        {
            objectAnimator.StartPlayback();
        }

        temp.SetActive(false);
    }

    public ObjectForPool GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject.activeInHierarchy == false)
            {
                return objects[i];
            }
        }
        AddObject(objects[0], objectsParent, 0);
        return objects[objects.Count - 1];
    }
}