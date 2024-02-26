using NumbersExasoft.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace NumbersExasoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data", "numbers.txt");


            string[] lines = System.IO.File.ReadAllLines(filePath);

            List<int> numberList = new List<int>();

            foreach (string line in lines)
            {
                string[] values = line.Split(' ');

                foreach (string value in values)
                {

                    if (int.TryParse(value, out int intValue))
                    {
                        numberList.Add(intValue);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse '{value}' as an integer.");
                    }
                }
            }
                        
            Dictionary<int, int> countDict = new Dictionary<int, int>();
            foreach (int num in numberList)
            {
                if (countDict.ContainsKey(num))
                    countDict[num]++;
                else
                    countDict[num] = 1;
            }

            // Order the numbers by count (largest count first) and take top 5
            var orderedCounts = countDict.OrderByDescending(kv => kv.Value).Take(5);

            // Output the ordered counts
            Console.WriteLine("Top 5 numbers ordered by count (largest count first):");
            foreach (var item in orderedCounts)
            {
                Console.WriteLine($"Number: {item.Key}, Count: {item.Value}");
            }

            ViewBag.Numbers = orderedCounts ;

            return View();
        }
    }
}
