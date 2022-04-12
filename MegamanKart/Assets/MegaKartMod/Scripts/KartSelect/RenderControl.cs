using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RenderControl : MonoBehaviour
{
    public List<GameObject> kartRenders;
    private void FixedUpdate()
    {
        IsMouseOverUIElement();
    }
    private void IsMouseOverUIElement()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            KartSelectInfos kartSelectInfos = raycastResults[i].gameObject.GetComponent<KartSelectInfos>();
            if (kartSelectInfos != null)
            {
                kartSelectInfos.kartRendered.SetActive(true);
                foreach(GameObject go in kartRenders)
                {
                    if(kartSelectInfos.kartRendered.gameObject != go) go.SetActive(false);
                }
            }
        }
    }

}
