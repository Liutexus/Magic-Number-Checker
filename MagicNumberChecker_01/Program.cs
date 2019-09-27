// Liutauras Bitinas; liutexus@gmail.com; Written in C#
using System;
using System.Linq;

namespace MagicNumberChecker_01
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputNumber = 0;
            int[] numbers = new int[6];
            int[] digits;
            
            Console.WriteLine("Enter a number which you want to check if it's a magic number :)");

            // Checking if the input was in correct format
            try
            {
                inputNumber = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format");
            }

            // Checking if the input is positive
            if(inputNumber < 0)
            {
                Console.WriteLine("The number must be positive");
                return;
            }

            for(int i = 1; i < 7; i++) // Store first 6 multiplications
            {
                numbers[i - 1] = i * inputNumber;
            }
            
            if(!EvenNumberOfDigits(numbers)) // Check if all multiplications have same amount of digits
            {
                Console.WriteLine("It is not a magic number.");
                return;
            }
            
            // Convert all digits into an array
            digits = Array.ConvertAll(numbers[0].ToString().ToCharArray(), c => (int)Char.GetNumericValue(c));

            for (int i = 1; i < numbers.Length; i++) // Check if all other multiplications have same digits
            {
                int[] tempDigits = Array.ConvertAll(numbers[i].ToString().ToCharArray(), c => (int)Char.GetNumericValue(c));
                if (!CheckIfDigitsExist(digits, tempDigits))
                {
                    Console.WriteLine("Digits between multiplications don't match.");
                    Console.WriteLine("The number is not magic number.");
                    return;
                }
            }
            
            for (int i = 0; i < numbers.Length - 1; i++) // Check if the order of digits is the same
            {
                int[] digitsB = Array.ConvertAll(numbers[i].ToString().ToCharArray(), c => (int)Char.GetNumericValue(c));
                int[] digitsA = Array.ConvertAll(numbers[i + 1].ToString().ToCharArray(), c => (int)Char.GetNumericValue(c));
                
                if(!CheckDigitOrder(digitsB, digitsA))
                {
                    Console.WriteLine("The order of digits doesn't match.");
                    Console.WriteLine("The number is not magic number.");
                    return;
                }
            }

            Console.WriteLine(inputNumber + " is a magic number! Hooray!");
        }

        // Checks if all numbers from multiplication have same amount of digits in them
        static private bool EvenNumberOfDigits(int[] array)
        {
            int countOfDigits = array[0].ToString().ToCharArray().Length;

            foreach(int num in array)
                if(countOfDigits != num.ToString().ToCharArray().Length)
                    return false;

            return true;
        }

        // Check if both arrays have same digits
        static private bool CheckIfDigitsExist(int[] a, int[] b)
        {
            for(int i = 0; i < a.Length; i++)
                if (!a.Contains(b[i]))
                    return false;

            return true;
        }

        // Check if digits have same order
        static private bool CheckDigitOrder(int[] a, int[] b)
        {
            int offset = 0; // Assumed offset between digits

            for(int i = 0; i < b.Length; i++)
            {
                if(a[0] == b[i])
                {
                    offset = i;
                    break;
                }
            }

            for(int i = 0; i < a.Length; i++)
            {
                int index = i + offset;
                if (index >= b.Length)
                    index -= b.Length;

                if(a[i] != b[index])
                {
                    return false;
                }
            }



            return true;
        }

    }
}
