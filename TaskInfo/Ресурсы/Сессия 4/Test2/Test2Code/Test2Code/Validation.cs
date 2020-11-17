using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2Code
{
   public class Validation
    {
       /// <summary>
       /// Determine whether a year is a leap year.
       /// </summary>
       /// <param name="year">year</param>
       /// <returns>true:this year is a leap year，false:this year is not a leap year</returns>
       public static bool IsLeapYear(int year)
       {
           bool flag = false;

           if (year%4==0)
           {
               if (year%100!=0)
               {
                   flag = true;
               }
               else
               {
                   if (year%400==0)
                   {
                       flag = true;
                   }
               }
           }

           return flag;
       }
       /// <summary>
       /// Calculate the last day of a month.
       /// </summary>
       /// <param name="year"></param>
       /// <param name="month"></param>
       /// <returns></returns>
       public static int LastDayOfMonth(int year,int month)
       {
           int lastDayOfMonth=0;

           switch (month)
           {
               case 1:
               case 3:
               case 5:
               case 7:
               case 8:
               case 10: 
               case 12:
                   lastDayOfMonth=31;
                   break;
               case 4:
               case 6:
               case 9:
               case 11:
                   lastDayOfMonth = 30;
                   break;
               case 2:
                   if (IsLeapYear(year))
                   {
                       lastDayOfMonth = 29;
                       break;
                   }
                   else
                   {
                       lastDayOfMonth = 28;
                       break;
                   }
               default:
                   break;
           }

           return lastDayOfMonth;
       }
       /// <summary>
       /// Determine whether the date entered is valid，the range of year is limited to 1811-2016.
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       public static bool ValidDate(Day date)
       {
           bool flag = false;

           if (date.Year >1811 && date.Year<=2016)
           {
               if (date.Month>=1 && date.Month<=12)
               {
                   if (date.Date>0 && date.Date<=LastDayOfMonth(date.Year,date.Month))
                   {
                       flag = true;
                   }
               }
           }

           return flag;
       }
       /// <summary>
       /// Gets the next day of the current date.
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       public static DateTime IncrementDate(Day date)
       {
           DateTime nowDay = new DateTime(date.Year, date.Month, date.Date);

           DateTime incrementDay = nowDay;
         
                if (nowDay.Day < LastDayOfMonth(date.Year, date.Month))
                   {
                       incrementDay = nowDay.AddDays(1);
                   }
                   else
                   {
                       if (nowDay.Month == 12)
                       {
                           incrementDay = new DateTime(date.Year + 1, 1, 1);
                       }
                       else
                       {
                           incrementDay = new DateTime(date.Year, date.Month + 1, 1);
                       }

                   }

           return incrementDay;
       }
    }
}
