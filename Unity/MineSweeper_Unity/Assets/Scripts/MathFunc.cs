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
        int randomResult = UnityEngine.Random.Range(1, 100);

        if (randomResult <= ratio)
            return (true);
        return (false);
    }

    //Given a boolean 2D array and a position, check the surrounding elements of the position to see if the surrounding contains the value "True"
    /// <param name="array"> The boolean 2D array </param>
    /// <param name="height"> Height of the array </param>
    /// <param name="width"> Width of the array </param>
    /// <param name="x"> Coordinate x </param>
    /// <param name="y"> Coordinate y </param>
    /// <returns> A boolean </returns>
    public static bool CheckSurrounding(bool[,] array, int height, int width, int x, int y)
    {
        for (int i = x - 1; i <= x + 1; i++)
            for (int j = y - 1; j <= y + 1; j++)
                if ((i >= 0) && (i <= height - 1) && (j >= 0) && (j <= width - 1))
                {
                    if (array[i, j] == true)
                        return (true);
                }

        return (false);
    }

}