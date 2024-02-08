﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SPERDLC
{
    public static class RPTPathClass
    {
        public static Stream GetReportFilePath(string path)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            Stream stream1 = assembly1.GetManifestResourceStream("SPERDLC." + path + ".rdlc");//R_24_CC.CancellationAddWork
            return stream1;
        }

        public static LocalReport SetRDLCReportDatasets(this LocalReport report, Dictionary<string, object> datasets = null)
        {
            if (datasets != null)
            {
                foreach (var dataset in datasets)
                {
                    report.DataSources.Add(new ReportDataSource(dataset.Key, dataset.Value));
                }
            }

            return report;
        }
    }
}
