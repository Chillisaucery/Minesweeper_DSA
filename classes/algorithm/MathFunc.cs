using System;

public class MathFunc
{
    /// <summary>
    /// All the customs mathematic functions needed for the game.
    /// It works as a library, other pieces of code can not access the constructor, only the functions that are public static.
    /// Contributors: Chillisauce 20 April
    /// </summary>
	
    //The constructor is private, not allowing any access from other codes
    private MathFunc()
    {

    }

    // Return a bool with the true/false ratio equal to a given ratio. 
    /// <param name="ratio"> 0 < ratio < 100 </param>
    public static bool RandomWithProbability(float ratio)
    {
        Random random = new Random();
        int randomResult = random.Next(1, 99);

        if (randomResult <= ratio)
            return (true);
        return (false);
    }

}