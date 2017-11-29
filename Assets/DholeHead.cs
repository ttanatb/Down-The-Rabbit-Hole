using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DholeHead : MonoBehaviour
{

    private Transform root;

    private void Start()
    {
        root = transform.root;
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = root.up;
    }
}
