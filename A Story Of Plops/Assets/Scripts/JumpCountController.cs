using UnityEngine;

public class JumpCountController : MonoBehaviour
{
    private int maxJumpCount = 3;
    private int jumpCount;

    public void AddJumpCount()
    {
        if(jumpCount < maxJumpCount)
        {
            ++jumpCount;
        }
    }

    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

    public bool CheckCanJump()
    {
        return jumpCount < maxJumpCount;
    }

    public void IncreaseMaxJumpCount(int jumpsToIncrease)
    {
        maxJumpCount += jumpsToIncrease;
    }

    public int GetMaxJumpCount()
    {
        return maxJumpCount;
    }
    
    public int GetJumpCount()
    {
        return jumpCount;
    }
}
