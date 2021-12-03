using System;

namespace cartapp
{
  public class Utils
  {
    // How to "Round to the Nearest Hundredth"
    // To round a decimal number to the nearest hundredth, check the value of its thousands. 
    // If the value is equal to 5 or greater, increase the value in the hundredth place by 1 and then remove all the digits to the right after the second decimal place. 
    // If the value is equal to 4 or less, simply remove all the digits to the right after the second decimal place.
    public static decimal RoundToTheNearestHundredth(decimal d)
    {
      var pd = Math.Abs(d);
      AssertMaximumValueNotExceeded(pd * 100);

      var theSign = Math.Sign(d);
      ulong roundedHundreth = (ulong)(pd * 100);
      if (GetThousandth(pd) >= 5)
      {
        return theSign * ((decimal)(roundedHundreth + 1) / 100);
      }
      return theSign * ((decimal)(roundedHundreth) / 100);
    }

    public static bool HasThousandth(decimal d)
    {
      return (GetThousandth(d) > 0);
    }

    public static ulong GetThousandth(decimal d)
    {
        AssertMaximumValueNotExceeded(d * 1000);

        return (ulong)((d * 1000)) - ((ulong)((d * 100))) * 10;
    }

    private static void AssertMaximumValueNotExceeded(decimal d)
    {
        if (d > ulong.MaxValue) throw new Exception("Exceeded the maximum number ("+ulong.MaxValue+") supported!");
    }
  }
}