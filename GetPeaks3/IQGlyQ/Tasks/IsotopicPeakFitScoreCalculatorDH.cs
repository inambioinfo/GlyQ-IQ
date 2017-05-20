﻿using System;
using System.Collections.Generic;
using IQGlyQ.Objects;
using IQGlyQ.Objects.EverythingIsotope;
using IQ_X64.Backend.Core;
using IQ_X64.Workflows.Core;
using Run64.Backend.Core;
using Run64.Utilities;

namespace IQGlyQ.Tasks
{
    public class IsotopicPeakFitScoreCalculatorDH : TaskIQ
    {
        #region properties

        private IsotopeParameters IsotopeParameterPile { get; set; }

        private IGenerateIsotopeProfile TheorFeatureGenV2 { get; set; }

        private TheoreticalIsotopicProfileWrapper Monster { get; set; }

        public IsotopicPeakFitScoreCalculatorDH()
        {
        }

        public IsotopicPeakFitScoreCalculatorDH(IsotopeParameters isoParameters, IGenerateIsotopeProfile theorFeatureGenV2, TheoreticalIsotopicProfileWrapper monster)
        {
            IsotopeParameterPile = isoParameters;
            TheorFeatureGenV2 = theorFeatureGenV2;
            Monster = monster;
        }


        #endregion

        #region Public Methods


        public override void Execute(ResultCollection resultList)
        {
            Check.Require(resultList.Run.CurrentMassTag != null, this.Name + " failed; CurrentMassTag is empty");
            Check.Require(resultList.Run.XYData != null && resultList.Run.XYData.Xvalues != null && resultList.Run.XYData.Xvalues.Length > 0, this.Name + " failed; Run's XY data is empty. Need to Run an MSGenerator");
            Check.Require(resultList.CurrentTargetedResult != null, "No MassTagResult has been generated for CurrentMassTag");

            IqTarget target = new FragmentIQTarget();
            target.ChargeState = resultList.CurrentTargetedResult.Target.ChargeState;
            target.EmpiricalFormula = resultList.CurrentTargetedResult.Target.EmpiricalFormula;


            IGenerateIsotopeProfile localTheorFeatureGenV2 = TheorFeatureGenV2;
            //TheoreticalIsotopicProfileWrapper.Generate(ref localTheorFeatureGenV2, target, IsotopeParameterPile.IsotopeProfileMode, IsotopeParameterPile.DeltaMassCalibrationMZ, IsotopeParameterPile.DeltaMassCalibrationMono, IsotopeParameterPile.ToMassCalibrate, IsotopeParameterPile.PenaltyMode);
            Monster.Generate(ref localTheorFeatureGenV2, target, IsotopeParameterPile.IsotopeProfileMode, IsotopeParameterPile.ToMassCalibrate, IsotopeParameterPile.ToShift);
            

            resultList.CurrentTargetedResult.Score = CalculateFitScore(target.TheorIsotopicProfile.Peaklist, resultList.CurrentTargetedResult.IsotopicProfile.Peaklist, IsotopeParameterPile.DivideFitScoreByNumberOfIons);
        }
        #endregion

        /// <summary>
        /// these need to be synched and clipped prior to entering
        /// </summary>
        /// <param name="theorProfile"></param>
        /// <param name="observedProfile"></param>
        /// <returns></returns>
        public double CalculateFitScore(List<MSPeak> theorProfile, List<MSPeak> observedProfile, bool usePeakNumberCorrection)
        {
            int lengthTheory = theorProfile.Count;
            int lengthObserved = observedProfile.Count;

            if(lengthTheory!=lengthObserved)
            {
                Console.WriteLine("Isotope profiles are different lenghts");
                Console.Read();
            }

          
 
            PeakLeastSquaresFitterSK peakFitter = new PeakLeastSquaresFitterSK();
            //double fitval = peakFitter.GetFit(theorPeakList, observedPeakList, minCuttoffTheorPeakIntensityFraction, massErrorPPMBetweenPeaks, numberOfPeaksToLeftForPenalty);//0.1
            //double fitval = peakFitter.GetFit(theorProfile, observedProfile);//0.1
            //double fitval = peakFitter.GetWeightedFit(theorProfile, observedProfile);//0.1
             

            double fitval = peakFitter.GetPenaltyFit(theorProfile, observedProfile, usePeakNumberCorrection);//0.1)
            if (double.IsNaN(fitval) || fitval > 1) fitval = 1;

            

            return fitval;
        }
    }
}
