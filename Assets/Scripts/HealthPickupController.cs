using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public float health;
    public float Pickup()
    {
        return health;
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
