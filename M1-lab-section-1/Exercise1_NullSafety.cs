using System;

namespace TmsCore.Exercises.M1_Lab_Section_1
{
    public static class Exercise1
    {
        public static void Run()
        {
            //This is how the legacy system declared region — no indication it could be empty

            //=============Step 1 —SeeWhattheCompiler Catches==============


            /* string region = null;
            Console.WriteLine(region.ToUpper()); */


            //===========Step 2 —FixIt Three Ways============

            // Declare the variable as nullable with '?'
            // string? region = null;

            /* string? displayRegion = region?.ToUpper();
            Console.WriteLine($"Region (coalesced): {displayRegion}".ToUpper()); */

            // Null-coalescing operator '??' — provide a fallback value
            // If region is null, use "Unassigned" instead.  

            /* string displayRegion = region ?? "Unassigned!";
            Console.WriteLine($"Region (coalesced): {displayRegion} "); */

            /* if (region == null)
                region = "Addis Ababa";
            Console.WriteLine($"Region (assigned): {region}"); */


            //=======Step 3 —DeclareYour First TMS Variables=========
            string studentName = "John";
            string studentId = "STU-001";
            int enrollmentCount = 3;
            decimal grantAmount = 1999.99m;
            DateTime dateTimeOfEnrollment = DateTime.Now;
            string? campusRegion = null; // Nullable string for region, as it may not be assigned yet

            Console.WriteLine($"Student: {studentName}({studentId})");
            Console.WriteLine($"Courses: {enrollmentCount}");
            Console.WriteLine($"Grant: {grantAmount:F2}");
            Console.WriteLine($"Enrolled: {dateTimeOfEnrollment: yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Campus Region: {campusRegion ?? "Not Assigned"}");
        }
    }
}
