using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.transform.TryGetComponent<IClickable>(out IClickable iter))
            {
                iter.OnClick();

            }
        }
    }
}
public interface IClickable
{
    void OnClick();
}
