using System.Collections.Generic;

namespace UoN.AccrediTool.Core.Models
{
    public static class UoCoreModels
    {

        private const int StartingYear = 1960;

        private static readonly IDictionary<int, string> _TermDict = new Dictionary<int, string>()
        {
            [0] = "Summer 1",
            [1] = "ELICOS Session 1",
            [2] = "ELICOS Session 2",
            [3] = "ELICOS Session 3",
            [4] = "ELICOS Session 4",
            [5] = "ELICOS Session 5",
            [6] = "ELICOS Session 6",
            [7] = "ELICOS Session 7",
            [8] = "ELICOS Session 8",
            [9] = "ELICOS Session 9",
            [11] = "Summer 2",
            [15] = "Trimester 1 (Singapore)",
            [25] = "Trimester 1",
            [40] = "Semester 1",
            [45] = "Trimester 2 (Singapore)",
            [55] = "Trimester 2",
            [60] = "Winter",
            [75] = "Trimester 3 (Singapore)",
            [80] = "Semester 2",
            [85] = "Trimester 3",
            [91] = "Quarter 1", 
            [92] = "Quarter 2",
            [93] = "Quarter 3",
            [94] = "Quarter 4"
        };

        public static string GetTerm(int? termCode)
        {
            if (termCode is int x)
            {
                if (_TermDict.TryGetValue((x % 100), out string result))
                {
                    return result;
                }
            }
            return "Unknown";
        }

        public static int GetYearCode(int year)
        {
            if(year > StartingYear)
            {
                return year - StartingYear;
            }
            else
            {
                return 0;
            }
        }
    }
}
