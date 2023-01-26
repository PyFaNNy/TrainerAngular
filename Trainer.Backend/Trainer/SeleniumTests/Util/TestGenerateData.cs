using System.Text;

namespace SeleniumTests.Util;

public class TestGenerateData
{
    public static string GenerateRandomString(int size, bool lowerCase = true)
    {
        StringBuilder stringBuilder = new StringBuilder();
        Random random = new Random();

        char ch;

        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            stringBuilder.Append(ch);
        }

        if (lowerCase)
        {
            return stringBuilder.ToString().ToLower();
        }

        return stringBuilder.ToString();
    }
    
    public static string GenerateRandomEmail(int size, string emailDomain)
    {
        string randomEmail = GenerateRandomString(size) + emailDomain;

        return randomEmail;
    }

    public static string GenerateRandomData(int size)
    {
        int[] array = new int[size];
        Random random = new Random();
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(33, 125);
            stringBuilder.Append((char) array[i]);
        }

        return stringBuilder.ToString();
    }

    public static string GenerateRandomPassword(int size)
    {
        string randomPassword = GenerateRandomData(size);

        return randomPassword;
    }

    public static int GenerateRandomNumber(int minValue, int maxValue)
    {
        Random random = new Random();

        return random.Next(minValue, maxValue);
    }
}