﻿using System.Collections.Generic;
using ResultsToGlycoGridX64.ImportedFromGetPeaks.GetPeaks;

namespace ResultsToGlycoGridX64.ImportedFromGetPeaks.FIFO
{
    public class ImportGlyQResult
    {
        public List<GlyQIqResult> Import(string fileName)
        {
            List<GlyQIqResult> GlyQIqResults = new List<GlyQIqResult>();

            StringLoadTextFileLine reader = new StringLoadTextFileLine();

            List<string> textIn = reader.SingleFileByLine(fileName);

            char seperator = '\t';

            foreach (string line in textIn)
            {
                string[] words = line.Split(seperator);

                GlyQIqResult result = new GlyQIqResult(words);

                GlyQIqResults.Add(result);
            }

            return GlyQIqResults;
        }

        
    }
}