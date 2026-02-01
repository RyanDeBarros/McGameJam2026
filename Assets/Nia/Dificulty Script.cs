using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// Start is called once before the first execution of Update after the MonoBehaviour is created


public class DifficultySpeedChanger : MonoBehaviour
{
public Animator animator;
public int Babies = CollectorManager.Babies;

[SerializeField] float Multiplier = 1.2f;
[SerializeField] float MaxSpeed = 5.0f;
[SerializeField] float MinSpeed = 3.0f;
public int PrevBabyCount = 0;

void Awake()
    {
        animator = GetComponent<Animator>();
    }

public void OnBabyIncrease(int UpdatedBabies)
    {
        PrevBabyCount = UpdatedBabies; 

        animator.GetBehaviour<ChaseBehavior>().chasingSpeed  *= Multiplier;
        animator.GetBehaviour<WalkingBehavior>().WalkingSpeed *= Multiplier;
    }
    void Update()
    {
        if (Babies != PrevBabyCount) {
            OnBabyIncrease(Babies);

        }

        float xPos = Mathf.Clamp(animator.GetBehaviour<ChaseBehavior>().chasingSpeed , MinSpeed, MaxSpeed);
    }
}