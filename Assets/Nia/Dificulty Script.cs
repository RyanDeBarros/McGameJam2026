using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// Start is called once before the first execution of Update after the MonoBehaviour is created


public class DifficultySpeedChanger : MonoBehaviour
{
public NavMeshAgent agent;
public int Babies = CollectorManager.Babies;

public int PrevBabyCount = 0;

void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

public void OnBabyIncrease(int UpdatedBabies)
    {
        PrevBabyCount = UpdatedBabies;
        agent.velocity *= 1.2f;
    }
    void Update()
    {
        if (Babies != PrevBabyCount) {
            OnBabyIncrease(Babies);
        }
    }
}