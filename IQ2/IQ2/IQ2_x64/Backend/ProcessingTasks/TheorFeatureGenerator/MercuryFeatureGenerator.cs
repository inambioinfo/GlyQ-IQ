﻿using System;
 


using Run64.Backend.Core;
using Run64.Backend.Core.Results;
using Run64.Backend.Data;
using Run64.Utilities;

namespace IQ_X64.Backend.ProcessingTasks.TheorFeatureGenerator
{
    public class MercuryFeatureGenerator : ITheorFeatureGenerator
    {

       
        
        #region Constructors
        public MercuryFeatureGenerator()
        {
            this.Name = "MercuryFeatureGenerator";
        }
        #endregion

        #region Properties
        
        #endregion

        #region Public Methods
        public override void LoadRunRelatedInfo(ResultCollection resultColl)
        {
            Check.Require(resultColl.Run.CurrentMassTag != null, this.Name + " failed; CurrentMassTag hasn't been declared");

            TargetBase mt = resultColl.Run.CurrentMassTag;

            TargetedResultBase result = resultColl.CurrentTargetedResult;

            Check.Require(result != null, this.Name + " failed; This task requires a MassTagResult, which is null");
            double fwhm = result.IsotopicProfile.GetFWHM();

            
        }
        #endregion

        #region Private Methods
        #endregion
        public override void GenerateTheorFeature(TargetBase mt)
        {
            Check.Require(mt != null, this.Name + " failed; CurrentMassTag hasn't been declared");
            //Check.Require(results.Run.CurrentMassTag.EmpiricalFormula!=null, this.Name + "failed; Problem with EmpiricalFormular of current mass tag.");




            //XYData theorXYData = new XYData();
            //for (int i = 0; i < mt.IsotopicProfile.Peaklist.Count; i++)
            //{

            //    XYData peakXYData = getTheorPeakData(mt.IsotopicProfile.Peaklist[i], fwhm);




            //}


            //distributionCreator.MolecularFormula = new MolecularFormula();
            //distributionCreator.getIsotopicProfile();
            //distributionCreator.OffsetDistribution(result.IsotopicProfile);

            //AreaFitter areafitter = new AreaFitter(distributionCreator.Data, result.Run.XYData, 10);
            //double fitval = areafitter.getFit();

            //if (fitval == double.NaN || fitval > 1) fitval = 1;

            //result.IsotopicProfile.Score = fitval;

        }

        private XYData getTheorPeakData(MSPeak mSPeak, double fwhm)
        {
            throw new NotImplementedException();
        }

  
    }
}
