using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private bool isFacingLeft = true;
    
    private Vector3 portalNormal;
    private PortalManager managerScript;

    private void Start()
    {
        if (isFacingLeft)
            portalNormal = -transform.right;
        else
            portalNormal = transform.right;

        managerScript = transform.parent.GetComponent<PortalManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            managerScript.TeleportPlayer(gameObject);
    }

    public Vector3 GetPortalNormal()
    {
        return portalNormal;
    }
}
