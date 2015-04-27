using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

//Ref: http://blogs.msdn.com/b/windowsazurestorage/archive/2013/11/27/windows-azure-storage-release-introducing-cors-json-minute-metrics-and-more.aspx
//Ref: http://msdn.microsoft.com/en-us/library/windowsazure/dn535601.aspx
//Ref: https://github.com/WindowsAzure/azure-storage-net/blob/c9d52db3f18f971933111f5ba3f7ce4e79927a73/Test/ClassLibraryCommon/Queue/QueueAnalyticsUnitTests.cs



namespace DevNet.Models
{
    public class WAMS
    {
         public void StartWAMS()
        {
            var _storageAccountName = "devnetmedia";
            var _storageAccountKey = "Dz9gNU30SmqUSqPv9vlYs6cABTrXX1DVU7i5p7e5hTHbrdrRtOs8wBKTzCOlTxYTzE9NhtRl/nm8hYxI9byAsw==";

            var accountAndKey = new StorageCredentials(_storageAccountName, _storageAccountKey);
            var storageaccount = new CloudStorageAccount(accountAndKey, true);
            var blobClient = storageaccount.CreateCloudBlobClient();

            Console.WriteLine("Storage Account: " + storageaccount.BlobEndpoint);
            var newProperties = CurrentProperties(blobClient);

            //Set service to new version:
            newProperties.DefaultServiceVersion = "2013-08-15"; //"2012-02-12"; // "2011-08-18"; // null;
            blobClient.SetServiceProperties(newProperties);

            var addRule = true;
            if (addRule)
            {
                //Add a wide open rule to allow uploads:
                var ruleWideOpenWriter = new CorsRule()
                {
                    AllowedHeaders = { "*" },
                    AllowedOrigins = { "*" },
                    AllowedMethods =
                        CorsHttpMethods.Options |
                        CorsHttpMethods.Post |
                        CorsHttpMethods.Put |
                        CorsHttpMethods.Merge,
                    ExposedHeaders = { "*" },
                    MaxAgeInSeconds = (int)TimeSpan.FromDays(5).TotalSeconds
                };
                newProperties.Cors.CorsRules.Add(ruleWideOpenWriter);
                blobClient.SetServiceProperties(newProperties);

                Console.WriteLine("New Properties:");
                CurrentProperties(blobClient);
            }

            Console.ReadKey();
        }

        private static ServiceProperties CurrentProperties(CloudBlobClient blobClient)
        {
            var currentProperties = blobClient.GetServiceProperties();
            if (currentProperties != null)
            {
                if (currentProperties.Cors != null)
                {
                    Console.WriteLine("Cors.CorsRules.Count          : " + currentProperties.Cors.CorsRules.Count);
                    for (int index = 0; index < currentProperties.Cors.CorsRules.Count; index++)
                    {
                        var corsRule = currentProperties.Cors.CorsRules[index];
                        Console.WriteLine("corsRule[index]              : " + index);
                        foreach (var allowedHeader in corsRule.AllowedHeaders)
                        {
                            Console.WriteLine("corsRule.AllowedHeaders      : " + allowedHeader);
                        }
                        Console.WriteLine("corsRule.AllowedMethods      : " + corsRule.AllowedMethods);

                        foreach (var allowedOrigins in corsRule.AllowedOrigins)
                        {
                            Console.WriteLine("corsRule.AllowedOrigins      : " + allowedOrigins);
                        }
                        foreach (var exposedHeaders in corsRule.ExposedHeaders)
                        {
                            Console.WriteLine("corsRule.ExposedHeaders      : " + exposedHeaders);
                        }
                        Console.WriteLine("corsRule.MaxAgeInSeconds     : " + corsRule.MaxAgeInSeconds);
                    }
                }
                Console.WriteLine("DefaultServiceVersion         : " + currentProperties.DefaultServiceVersion);
                Console.WriteLine("HourMetrics.MetricsLevel      : " + currentProperties.HourMetrics.MetricsLevel);
                Console.WriteLine("HourMetrics.RetentionDays     : " + currentProperties.HourMetrics.RetentionDays);
                Console.WriteLine("HourMetrics.Version           : " + currentProperties.HourMetrics.Version);
                Console.WriteLine("Logging.LoggingOperations     : " + currentProperties.Logging.LoggingOperations);
                Console.WriteLine("Logging.RetentionDays         : " + currentProperties.Logging.RetentionDays);
                Console.WriteLine("Logging.Version               : " + currentProperties.Logging.Version);
                Console.WriteLine("MinuteMetrics.MetricsLevel    : " + currentProperties.MinuteMetrics.MetricsLevel);
                Console.WriteLine("MinuteMetrics.RetentionDays   : " + currentProperties.MinuteMetrics.RetentionDays);
                Console.WriteLine("MinuteMetrics.Version         : " + currentProperties.MinuteMetrics.Version);
            }
            return currentProperties;
        }

    }
}