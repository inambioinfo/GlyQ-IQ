﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YAFMS_DB.GetPeaks;

namespace GetPeaks_DLL.SQLite.DataTransferObjects
{
    public class DatabaseDeconPeakObject : DatabaseTransferObject
    {
        public int PeakIndex { get; set; }

        public int ScanNum { get; set; }

        public double MZ { get; set; }

        public double Intensity { get; set; }

        public double FWHM { get; set; }

        public double SignalToNoise { get; set; }

        public double MSFeatureID { get; set; }

        public DatabaseDeconPeakObject()
        {
            Columns.Add("peak_index");
            Columns.Add("scan_num");
            Columns.Add("mz");
            Columns.Add("intensity");
            Columns.Add("fwhm");
            Columns.Add("signal_noise");
            Columns.Add("MSFeatureID");

            IndexedColumns.Add("scan_num");
            IndexedColumns.Add("mz");
           
            ValuesTypes.Add(DbType.Int32);
            ValuesTypes.Add(DbType.Int32);
            ValuesTypes.Add(DbType.Double);
            ValuesTypes.Add(DbType.Single);
            ValuesTypes.Add(DbType.Double);
            ValuesTypes.Add(DbType.Double);
            ValuesTypes.Add(DbType.Double);

            TableName = "PeakTable";
        }
    }

    public class DatabasePeakObjectList : DatabaseTransferObjectList
    {
        public override List<DatabaseTransferObject> DatabaseTransferObjects { get; set; }

        public DatabasePeakObjectList()
        {
            DatabaseTransferObjects = new List<DatabaseTransferObject>();
        }
    }
}
