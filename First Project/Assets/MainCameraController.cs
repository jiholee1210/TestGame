using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] Transform player, background;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);

        //targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = targetPos;
        background.position = new Vector2(targetPos.x, 0);
    }
}
