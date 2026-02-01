using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



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

        animator.GetBehaviour<ChaseBehavior>().chasingSpeed = Mathf.Clamp(animator.GetBehaviour<ChaseBehavior>().chasingSpeed* Multiplier , MinSpeed, MaxSpeed);
        animator.GetBehaviour<WalkingBehavior>().WalkingSpeed = Mathf.Clamp(animator.GetBehaviour<WalkingBehavior>().WalkingSpeed* Multiplier , MinSpeed, MaxSpeed);
        animator.GetBehaviour<PatrolBehavior>().probabilityOccurence = Mathf.Clamp(1/ (animator.GetBehaviour<PatrolBehavior>().probabilityOccurence + 1) , 1, 5);
        
    }
    void Update()
    {
        if (Babies != PrevBabyCount) {
            OnBabyIncrease(Babies);

        }

        
    }
}