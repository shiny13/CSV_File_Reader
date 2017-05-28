using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERM_TestTask.Model;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace ERM_TestTask 
{
    class TouReader : IReader
    {
        #region var
        private static readonly string[] HeaderColumns = { "MeterPoint Code", "Serial Number", "Plant Code", "Date/Time", "Data Type", "Energy", "Maximum Demand" ,
                                                            "Time of Max Demand", "Units", "Status", "Period", "DLS Active", "Billing Reset Count", "Billing Reset Date/Time", "Rate" };
        #endregion

        public TouReader() { }

        void IReader.ReadFiles(string path, string filename, out List<LpModel> lpItems, out List<TouModel> touItems)
        {
            lpItems = new List<LpModel>();
            touItems = new List<TouModel>();

            var streamReader = new StreamReader(path + filename);
            using (CsvReader csv = new CsvReader(streamReader, true))
            {
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord())
                {
                    var touModel = new TouModel();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        try
                        {
                            if (csv[i] == null)
                                continue;

                            if (headers[i] == "MeterPoint Code")
                                touModel.MeterPointCode = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Serial Number")
                                touModel.SerialNumber = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Plant Code")
                                touModel.PlantCode = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Date/Time")
                                touModel.DateTime = Utility.ObjToDateTime(csv[i]);
                            else if (headers[i] == "Data Type")
                                touModel.DataType = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Energy")
                                touModel.Energy = Utility.ObjToDecimal(csv[i]);
                            else if (headers[i] == "Maximum Demand")
                                touModel.MaximumDemand = Utility.ObjToDecimal(csv[i]);
                            else if (headers[i] == "Time of Max Demand")
                                touModel.TimeOfMaxDemand = Utility.ObjToDateTime(csv[i]);
                            else if (headers[i] == "Units")
                                touModel.Units = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Status")
                                touModel.Status = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Period")
                                touModel.Period = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "DLS Active")
                                touModel.DlsActive = Utility.ObjToBool(csv[i]);
                            else if (headers[i] == "Billing Reset Count")
                                touModel.BillingResetCount = Convert.ToInt32(csv[i]);
                            else if (headers[i] == "Billing Reset Date/Time")
                                touModel.BillingResetDateTime = Utility.ObjToDateTime(csv[i]);
                            else if (headers[i] == "Rate")
                                touModel.Rate = Utility.ObjToStr(csv[i]);
                        }
                        catch (Exception e)
                        {
                            var dateStr = (touModel.DateTime.HasValue) ? touModel.DateTime.Value.ToString() : "";
                            Console.WriteLine("Exception in " + filename + " Serial No: " + touModel.SerialNumber + " DateTime: " + dateStr + " Energy: " + touModel.Energy);
                        }

                    }
                    /*Console.WriteLine(touModel.MeterPointCode + "," + touModel.SerialNumber + "," + touModel.PlantCode + "," + touModel.DateTime.Value.ToString() + "," + touModel.DataType + "," +
                                                touModel.Energy + "," + touModel.MaximumDemand + "," + touModel.TimeOfMaxDemand + "," + touModel.Units + "," + touModel.Status + "," +
                                                touModel.Period + "," + touModel.DlsActive + "," + touModel.BillingResetCount + "," + touModel.BillingResetDateTime.Value.ToString() + "," + touModel.Rate);*/

                    touItems.Add(touModel);
                }

                Console.WriteLine("Imported " + touItems.Count() + " values in file: " + filename);
                touItems = touItems.OrderBy(x => x.Energy).ToList();
                
            }
            //csv.Dispose();
            streamReader.Close();
        }
    }
}
