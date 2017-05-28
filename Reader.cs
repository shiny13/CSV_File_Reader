using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERM_TestTask.Model;
using System.IO;

namespace ERM_TestTask
{
    class Reader
    {
        private IReader ReadLp;
        private IReader ReadTou;
        public List<LpModel> LpValues;
        public List<TouModel> TouValues;

        public Reader()
        {
            ReadLp = new LpReader();
            ReadTou = new TouReader();
            LpValues = new List<LpModel>();
            TouValues = new List<TouModel>();
        }

        public void ReadCsv(string path, string filename)
        {
            decimal median = 0M;
            var tempLpItems = new List<LpModel>();
            var tempTouItems = new List<TouModel>();
            if (filename.Contains("LP"))
            {
                //Console.WriteLine("This is a LP file");
                ReadLp.ReadFiles(path, filename, out tempLpItems, out tempTouItems);
                if (tempLpItems.Count > 0)
                    LpValues.AddRange(tempLpItems);

                if (Utility.IsOdd(tempLpItems.Count()))
                {
                    if (tempLpItems.Count == 1)
                    {
                        var medianItem = tempLpItems.ElementAtOrDefault(0);
                        median = medianItem.DataValue;
                    }
                    else if (tempLpItems.Count > 2)
                    {
                        int medianPos = (tempLpItems.Count() / 2);
                        medianPos++;

                        var medianItem = tempLpItems.ElementAtOrDefault(medianPos);
                        median = medianItem.DataValue;
                    }
                }
                else
                {
                    if (tempLpItems.Count() == 0)
                    {
                        median = 0;
                    }
                    else if (tempTouItems.Count() == 2)
                    {
                        var medianItem1 = tempLpItems.ElementAtOrDefault(0);
                        var medianItem2 = tempLpItems.ElementAtOrDefault(1);
                        median = (medianItem1.DataValue + medianItem2.DataValue) / 2;
                    }
                    else if (tempTouItems.Count() > 2)
                    {
                        int medianPos1 = (tempLpItems.Count() / 2);
                        int medianPos2 = medianPos1 + 1;
                        var medianItem1 = tempLpItems.ElementAtOrDefault(medianPos1);
                        var medianItem2 = tempLpItems.ElementAtOrDefault(medianPos2);
                        median = (medianItem1.DataValue + medianItem2.DataValue) / 2;
                    }
                }
                Console.WriteLine("Median Data Value for " + filename + ": " + median);
                var medianLess = median - (median * 0.2M);
                var medianMore = median + (median * 0.2M);
                var printItems = tempLpItems.Where(x => x.DataValue < medianLess || x.DataValue > medianMore).ToList();
                PrintLpItems(printItems, filename, median);
            }
            else if (filename.Contains("TOU"))
            {
                //Console.WriteLine("This is a TOU file");
                ReadTou.ReadFiles(path, filename, out tempLpItems, out tempTouItems);
                if (tempTouItems.Count > 0)
                    TouValues.AddRange(tempTouItems);

                if (Utility.IsOdd(tempTouItems.Count()))
                {
                    if (tempTouItems.Count() == 1)
                    {
                        var medianItem = tempTouItems.ElementAtOrDefault(0);
                        median = medianItem.Energy;
                    }
                    else if (tempTouItems.Count() > 2)
                    {
                        int medianPos = (tempTouItems.Count() / 2);
                        medianPos++;

                        var medianItem = tempTouItems.ElementAtOrDefault(medianPos);
                        median = medianItem.Energy;
                    }
                }
                else
                {
                    if (tempTouItems.Count() == 0)
                    {
                        median = 0;
                    }
                    else if (tempTouItems.Count() == 2)
                    {
                        var medianItem1 = tempTouItems.ElementAtOrDefault(0);
                        var medianItem2 = tempTouItems.ElementAtOrDefault(1);
                        median = (medianItem1.Energy + medianItem2.Energy) / 2;
                    }
                    else if (tempTouItems.Count() > 2)
                    {
                        int medianPos1 = (tempTouItems.Count() / 2);
                        int medianPos2 = medianPos1 + 1;
                        var medianItem1 = tempTouItems.ElementAtOrDefault(medianPos1);
                        var medianItem2 = tempTouItems.ElementAtOrDefault(medianPos2);
                        median = (medianItem1.Energy + medianItem2.Energy) / 2;
                    }
                }
                Console.WriteLine("Median Energy for " + filename + ": " + median);
                var medianLess = median - (median * 0.2M);
                var medianMore = median + (median * 0.2M);
                var printItems = tempTouItems.Where(x => x.Energy < medianLess || x.Energy > medianMore).ToList();
                PrintTouItems(printItems, filename, median);
            }
        }

        #region Private Methods
        private void PrintLpItems(List<LpModel> items, string filename, decimal median)
        {
            foreach (var i in items)
            {
                try
                {
                    var dateStr = (i.DateTime.HasValue) ? i.DateTime.Value.ToString() : "";
                    Console.WriteLine(filename + " " + dateStr + " " + i.DataValue + " " + median);
                } catch (Exception e)
                {
                    Console.WriteLine("Exception on file " + filename + ": " + e.ToString());
                }
            }
            Console.WriteLine("");

        }

        private void PrintTouItems(List<TouModel> items, string filename, decimal median)
        {
            foreach (var i in items)
            {
                try
                {
                    var dateStr = (i.DateTime.HasValue) ? i.DateTime.Value.ToString() : "";
                    Console.WriteLine(filename + " " + dateStr + " " + i.Energy + " " + median);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception on file " + filename + ": " + e.ToString());
                }
            }
            Console.WriteLine("");
        }

        #endregion

    }
}
