// How to write to the console
Console.WriteLine("Hello World!");

// How to write a variable inside a string to the console
// Use the '$' to allow variables inside of {} in a string
string aFriend = "Dudebro"
Console.WriteLine($"Hello {aFriend}");

// Trim white space
string greeting = "      Hello World!       ";
trimmedGreeting = greeting.Trim();
Console.WriteLine($"{trimmedGreeting}");

// Replace word in string
string sayHello = "Hello World!";
sayHello = sayHello.Replace("Hello", "Greetings");
Console.WriteLine(sayHello);

// Uppercase/lowercase strings
Console.WriteLine(sayHello.ToUpper());
Console.WriteLine(sayHello.ToLower());

// Search for substring in string
string songLyrics = "You say goodbye, and I say hello";
Console.WriteLine(songLyrics.Contains("goodbye")); // true
Console.WriteLine(songLyrics.Contains("greetings")); // false

// Check if string start/ends with substring
Console.WriteLine(songLyrics.StartsWith("You")); // true
Console.WriteLine(songLyrics.StartsWith("goodbye")); // false
Console.WriteLine(songLyrics.EndsWith("hello")); // true
Console.WriteLine(songLyrics.EndsWith("goodbye")); // false

// Parse different number forms based on culture
// (currency symbols, thousands separators, decimal separators)
// https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-numeric#parsing-and-format-providers
//
// This example displays the following output:
//    en-US: 1,304.16 --> 1304.16
//    
//    en-US: Unable to parse '$1,456.78'.
//    fr-FR: Unable to parse '$1,456.78'.
//    
//    en-US: 1,094 --> 1094
//    
//    en-US: 152 --> 152
//    
//    en-US: Unable to parse '123,45 €'.
//    fr-FR: Unable to parse '123,45 €'.
//    
//    en-US: Unable to parse '1 304,16'.
//    fr-FR: 1 304,16 --> 1304.16
//    
//    en-US: Unable to parse 'Ae9f'.
//    fr-FR: Unable to parse 'Ae9f'.
using System;
using System.Globalization;

public class Example
{
   public static void Main()
   {
      string[] values = { "1,304.16", "$1,456.78", "1,094", "152", 
                          "123,45 €", "1 304,16", "Ae9f" };
      double number;
      CultureInfo culture = null;
      
      foreach (string value in values) {
         try {
            culture = CultureInfo.CreateSpecificCulture("en-US");
            number = Double.Parse(value, culture);
            Console.WriteLine("{0}: {1} --> {2}", culture.Name, value, number);
         }   
         catch (FormatException) {
            Console.WriteLine("{0}: Unable to parse '{1}'.", 
                              culture.Name, value);
            culture = CultureInfo.CreateSpecificCulture("fr-FR");
            try {
               number = Double.Parse(value, culture);
               Console.WriteLine("{0}: {1} --> {2}", culture.Name, value, number);
            }
            catch (FormatException) {
               Console.WriteLine("{0}: Unable to parse '{1}'.", 
                                 culture.Name, value);
            }
         }
         Console.WriteLine();
      }   
   }
}

// Parse number styles from strings into numbers
// ("1,304" -> 1304)
// https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-numeric#parsing-and-numberstyles-values
// 
// This example displays the following output:
//       Unable to convert '1,304'
//       1,304 --> 1304
using System;
using System.Globalization;

public class Example
{
   public static void Main()
   {
      string value = "1,304";
      int number;
      IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
      if (Int32.TryParse(value, out number))
         Console.WriteLine("{0} --> {1}", value, number);
      else
         Console.WriteLine("Unable to convert '{0}'", value);
            
      if (Int32.TryParse(value, NumberStyles.Integer | NumberStyles.AllowThousands, 
                        provider, out number))
         Console.WriteLine("{0} --> {1}", value, number);
      else
         Console.WriteLine("Unable to convert '{0}'", value);
   }
}

// Parse unicode
// https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-numeric#parsing-and-unicode-digits
//
// The example displays the following output:
//       '12345' --> 12345
//       Unable to parse '１２３４５'.
//       Unable to parse '١٢٣٤٥'.
//       Unable to parse '১২৩৪৫'.
using System;

public class Example
{
   public static void Main()
   {
      string value;
      // Define a string of basic Latin digits 1-5.
      value = "\u0031\u0032\u0033\u0034\u0035";
      ParseDigits(value);

      // Define a string of Fullwidth digits 1-5.
      value = "\uFF11\uFF12\uFF13\uFF14\uFF15";
      ParseDigits(value);
      
      // Define a string of Arabic-Indic digits 1-5.
      value = "\u0661\u0662\u0663\u0664\u0665";
      ParseDigits(value);
      
      // Define a string of Bangla digits 1-5.
      value = "\u09e7\u09e8\u09e9\u09ea\u09eb";
      ParseDigits(value);
   }

   static void ParseDigits(string value)
   {
      try {
         int number = Int32.Parse(value);
         Console.WriteLine("'{0}' --> {1}", value, number);
      }   
      catch (FormatException) {
         Console.WriteLine("Unable to parse '{0}'.", value);      
      }     
   }
}

// Convert string to number / number to string
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number#calling-the-parse-and-tryparse-methods
// 
using System;

public class StringConversion
{
    public static void Main()
    {
       string input = String.Empty;
       try
       {
           int result = Int32.Parse(input);
           Console.WriteLine(result);
       }
       catch (FormatException)
       {
           Console.WriteLine($"Unable to parse '{input}'");
       }
       // Output: Unable to parse ''

       try
       {
            int numVal = Int32.Parse("-105");
            Console.WriteLine(numVal);
       }
       catch (FormatException e)
       {
           Console.WriteLine(e.Message);
       }
       // Output: -105

        if (Int32.TryParse("-105", out int j))
            Console.WriteLine(j);
        else
            Console.WriteLine("String could not be parsed.");
        // Output: -105

        try
        {
            int m = Int32.Parse("abc");
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        // Output: Input string was not in a correct format.

        string inputString = "abc";
        if (Int32.TryParse(inputString, out int numValue))
            Console.WriteLine(inputString);
        else
            Console.WriteLine($"Int32.TryParse could not parse '{inputString}' to an int.");
        // Output: Int32.TryParse could not parse 'abc' to an int.
     }
}

// Parse strings using split
// .Split can also take an array of characters
string phrase = "The quick brown fox jumps over the lazy dog.";
string[] words = phrase.Split(' ');

foreach (var word in words)
{
    System.Console.WriteLine($"<{word}>");
}

// Navigate hero through grid to find princess
// m = hero; p = princess;
// where grid like: 
//    [---]
//    [-m-]
//    [--p]
using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    
static void displayPathtoPrincess(int n, String [] grid){
    int princessX = 0;
    int princessY = 0;
    int heroX = 0;
    int heroY = 0;
    int xDif = 0;
    int yDif = 0;
    
    // Loop through each row
    for (int i = 0; i < n; i++) {
        // Loop through each column
        for (int j = 0; j < n; j++) {
            if (grid[i][j].Equals('p')) {
                // Find princess location
                princessX = i;
                princessY = j;
            } else if (grid[i][j].Equals('m')) {
                // Find hero location
                heroX = i;
                heroY = j;
            }
        }
    }
    
    // Get X and Y difference between hero and princess
    xDif = heroX - princessX;
    yDif = heroY - princessY;
    
    
    // Navigate hero LEFT OR RIGHT
    if (xDif > 0) {
        for (int i = 0; i < xDif; i++) {
            Console.WriteLine("LEFT");
        }
    } else if (xDif < 0) {
        int xDifPos = Math.Abs(xDif);
        for (int i = 0; i < xDifPos; i++) {
            Console.WriteLine("RIGHT");
        }
    }
    
    // Navigate hero UP OR DOWN
    if (yDif > 0) {
        for (int i = 0; i < yDif; i++) {
            Console.WriteLine("UP");
        }
    } else if (yDif < 0) {
        int yDifPos = Math.Abs(yDif);
        for (int i = 0; i < yDifPos; i++) {
            Console.WriteLine("DOWN");
        }
    }
}

static void Main(String[] args) {
    int m;

    m = int.Parse(Console.ReadLine());

    String[] grid  = new String[m];
    for(int i=0; i < m; i++) {
        grid[i] = Console.ReadLine(); 
    }

    displayPathtoPrincess(m,grid);
    }
}


// Log hero's next move to navigate through grid to princess
// m = hero; p = princess;
// where grid like: 
//    [-----]
//    [-m---]
//    [-----]
//    [-----]
//    [----p]
using System;
using System.Collections.Generic;
using System.IO;
class Solution {

static void nextMove(int gSize,int heroY, int heroX, String [] grid){
    int princessX = 0;
    int princessY = 0;
    int xDif = 0;
    int yDif = 0;
    string nextMove = "";
    
    for (int i = 0; i < gSize; i++) {
        for (int j = 0; j < gSize; j++) {
            if (grid[i][j].Equals('p')) {
                princessX = j;
                princessY = i;
            }
        }
    }
    
    xDif = heroX - princessX;
    yDif = heroY - princessY;
    
    if (xDif != 0) {
        if (xDif > 0) {
            nextMove = "LEFT";
        } else if (xDif < 0) {
            nextMove = "RIGHT";
        }
    }
    
    if (yDif != 0) {
        if (yDif > 0) {
            nextMove = "UP";
        } else if (yDif < 0) {
            nextMove = "DOWN";
        }
    }
    
    Console.WriteLine(nextMove);
}

static void Main(String[] args) {
    int n;

    n = int.Parse(Console.ReadLine());
    String pos;
    pos = Console.ReadLine();
    String[] position = pos.Split(' ');
    int [] int_pos = new int[2];
    int_pos[0] = Convert.ToInt32(position[0]);
    int_pos[1] = Convert.ToInt32(position[1]);
    String[] grid  = new String[n];
    for(int i=0; i < n; i++) {
        grid[i] = Console.ReadLine(); 
    }

    nextMove(n, int_pos[0], int_pos[1], grid);

    }
}

// Network custom menu
public class NetworkCustomMenu : NetworkBehaviour {
    public Text ipTextBox;
    public Text portTextBox;
    
    public void StartServer()
    {
        //note that you can do this directly from hooking the button to the network manager if you want
        NetworkManager.singleton.StartHost();
        Debug.Log(NetworkManager.singleton.networkAddress);

    }

    public void JoinAsClient()
    {
        //You'll probably want to do a more robust check that the ip is either localhost or an ip format here
        if (ipTextBox.text != null && ipTextBox.text.Length > 0)
        {
            NetworkManager.singleton.networkAddress = ipTextBox.text;
            //again, we need a more careful check that we have a valid value for port here ideally
            int x;
            int.TryParse(portTextBox.text, out x); //usually the port will just be 7777 so this part isn't really nescessary unless you're specifying the port on a server
            
            NetworkManager.singleton.networkPort = x;
            //this is the actual code to start the client
            NetworkManager.singleton.StartClient();
        }
    }
}

// Animate object
public class RisingGameObject : MonoBehaviour {
    Animator anim;
	void Start () {
        anim = GetComponent<Animator>();
        DelegaterSubject.methodDefinedElsewhere += makePlanetRise;
        DelegaterSubject.methodDefinedElsewhere += logAnimationHappened;
	}
	
    public void makePlanetRise()
    {
        anim.SetBool("isRising", true);
        DelegaterSubject.methodDefinedElsewhere -= makePlanetRise;
        DelegaterSubject.methodDefinedElsewhere += stopPlanetRise;
    }
    public void stopPlanetRise()
    {
        anim.SetBool("isRising", false);
        DelegaterSubject.methodDefinedElsewhere -= stopPlanetRise;
        DelegaterSubject.methodDefinedElsewhere += makePlanetRise;
    }
    public void logAnimationHappened()
    {
        Debug.Log("Hey, it worked");
    }
}

// Weighted sum
public static class InputHandler
{
    public static string processText(string inS)
    {
       return inS;
    }

    public static double getWeightedSum(double[] gradesArr, double weight)
    {
        double sum = 0;
        foreach (double grade in gradesArr)
        {
            sum += grade;
        }
        return (sum / gradesArr.Length) * weight;
    }

    public static double[] parseAsArray(string doubleString)
    {
        string[] numAsString = doubleString.Split(' ');
        double[] arr = new double[numAsString.Length];

        for (int i = 0; i < numAsString.Length; i++)
        {
            arr[i] = getNum(numAsString[i]);
        }

        return arr;
    }

    public static double getNum(string inS)
    {
        try
        {
            double num = Double.Parse(inS);
            return num;
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            return -1;
        }
    }

    public static bool isNum(string inS)
    {
        try
        {
            double num = Double.Parse(inS);
            return true;
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public static bool betterIsNum(string inS)
    {
        if (Double.TryParse(inS, out double j))
            return true;
        else
            return false;
    }
}

// Generate objects dynamically
public class Utilities : MonoBehaviour {
    public GameObject prefab;
    public Instantiate instScript;
    public float spacing = 2f;

    public void instantiatePrefab()
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
    public void instantiatePrefabAtPosition(Vector3 pos)
    {
        Instantiate(prefab, pos, Quaternion.identity);
    }
    public void instantiatPrefabsEqualSpacing()
    {
        int i = 0;
        int r = instScript.r; 
        Vector3 pos;
        while (i < r) //i=0, r = whatever is input
        {
            pos = new Vector3(i * spacing, 2, i * spacing);
            instantiatePrefabAtPosition(pos);
            for (int j = 0; j < r; j++)
            {
                pos = new Vector3(i * spacing, 2 + j * spacing, i * spacing);
                instantiatePrefabAtPosition(pos);
                
                for (int k = 0; k < r; k++)
                {
                    pos = new Vector3(i * spacing + k * spacing, 2 + j * spacing, i * spacing);
                    instantiatePrefabAtPosition(pos);
                }
                
            }
            
            i++;
        }
    }
}
