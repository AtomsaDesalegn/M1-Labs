using System;

namespace TmsCore.Exercises.M1_Lab_Section_1
{
    public static class Exercise2
    {
        public static void Run()
        {
            //=========Step 1 —SeetheBug

            double grantPerStudent = 1999.99; // Should be decimal, not double
            double totalAllocation = grantPerStudent * 100_000; // 100,000 students
            Console.WriteLine($"Total allocated (double): {totalAllocation}"); // May show precision issues


            //=========Step 2 —FixIt

            decimal grantPerStudentDecimal = 1999.99m; // Use decimal for financial calculations
            decimal totalAllocationDecimal = grantPerStudentDecimal * 100_000; // 100,000 students
            Console.WriteLine($"Total allocated (decimal): {totalAllocationDecimal}"); // Accurate financial output  
            Console.WriteLine($"Total allocated (decimal, formatted): {totalAllocationDecimal:F2}"); // Formatted as currency
        }
    }
}