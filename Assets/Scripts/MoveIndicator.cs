using System.Collections;
using UnityEngine;

namespace DangerousPenguin
{

public class MoveIndicator : MonoBehaviour
{
    [SerializeField] private MoveIndicatorPing visual;
    
    public void PingLocation(Vector3 position)
    {
        var ping = Instantiate(visual, position + new Vector3(0,0.1f,0), Quaternion.Euler(90,0,0), transform);
        StartCoroutine(DismissPing(ping));
    }

    private IEnumerator DismissPing(MoveIndicatorPing ping)
    {
        yield return new WaitForSeconds(2);
        Destroy(ping.gameObject);
    }
}

}