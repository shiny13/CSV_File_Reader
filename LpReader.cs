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
    class LpReader : IReader
    {
        #region var
        private static readonly string[] HeaderColumns = { "MeterPoint Code", "Serial Number", "Plant Code", "Date/Time", "Units", "Status", "Data Type", "Data Value" };
        #endregion

        public LpReader() { }

        void IReader.ReadFiles(string path, string filename, out List<LpModel> lpItems, out List<TouModel> touItems)
        {
            lpItems = new List<LpModel>();
            touItems = new List<TouModel>();

            var streamReader = new StreamReader(path + filename);
            using (CsvReader csv = new CsvReader(streamReader, true))
            {
                int fieldCount = csv.FieldCount;
                //var indexDic = new Dictionary<string, int>();
                string[] headers = csv.GetFieldHeaders();

                while (csv.ReadNextRecord())
                {
                    var lpModel = new LpModel();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        try
                        {
                            //Console.Write(string.Format("{0} = {1};", headers[i], csv[i]));
                            if (csv[i] == null)
                                continue;

                            if (headers[i] == "MeterPoint Code")
                                lpModel.MeterPointCode = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Serial Number")
                                lpModel.SerialNumber = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Plant Code")
                                lpModel.PlantCode = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Date/Time")
                                lpModel.DateTime = Utility.ObjToDateTime(csv[i]);
                            else if (headers[i] == "Units")
                                lpModel.Units = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Status")
                                lpModel.Status = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Data Type")
                                lpModel.DataType = Utility.ObjToStr(csv[i]);
                            else if (headers[i] == "Data Value")
                                lpModel.DataValue = Utility.ObjToDecimal(csv[i]);

                        } catch (Exception e)
                        {
                            var dateStr = (lpModel.DateTime.HasValue) ? lpModel.DateTime.Value.ToString() : "";
                            Console.WriteLine("Exception in " + filename + " Serial No: " + lpModel.SerialNumber + " DateTime: " + dateStr + " DataValue: " + lpModel.DataValue);
                        }
                    }

                    //Console.WriteLine(lpModel.MeterPointCode + "," + lpModel.SerialNumber + "," + lpModel.PlantCode + "," + lpModel.DateTime.Value.ToString() + "," + lpModel.Units + "," + lpModel.Status + "," + lpModel.DataType + "," + lpModel.DataValue);

                    lpItems.Add(lpModel);
                }

                Console.WriteLine("Imported " + lpItems.Count() + " values in file: " + filename);
                lpItems = lpItems.OrderBy(x => x.DataValue).ToList();
            }
            //csv.Dispose();
            streamReader.Close();
        }
    }
}
