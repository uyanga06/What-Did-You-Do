using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public FPController player;

    public void Attack()
    {
        player.Attack();
    }
}
