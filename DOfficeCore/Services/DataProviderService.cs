using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Serilog;

namespace DOfficeCore.Services
{
    ///<inheritdoc cref="IDataProviderService"/>
    class DataProviderService : IDataProviderService
    {

        ///<inheritdoc/>
        public bool SaveDataToFile<T>(IEnumerable<T> data, string path)
        {
            if (data is null) return false;
            else if (string.IsNullOrWhiteSpace(path)) return false;

            try
            {
                var json = JsonSerializer.Serialize(data);
                File.WriteAllText(path, json, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Log.Error($"Can't save file. Error:\n{0}", e.Message);
                throw new Exception("Unexpected error", e);
            }

            Log.Information("File saved succesfully");
            return true;
        }

        ///<inheritdoc/>
        public List<Sector> LoadSectorsFromJson(string path)
        {
            var result = new List<Sector>();
            try
            {
                if (!File.Exists(path))
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    using FileStream fs = File.Create(path);
                    Log.Verbose($"File {path} doesn't exist");
                }
                else
                {
                    var jsonString = File.ReadAllText(path);
                    if (!string.IsNullOrEmpty(jsonString))
                        result = JsonSerializer.Deserialize<List<Sector>>(jsonString);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Can't load data from file {0}. Error:\n{1}.", path, e.Message);
                throw new Exception("Unexpected error", e);
            }
            return result;
        }
    }
}
