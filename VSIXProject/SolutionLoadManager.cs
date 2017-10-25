using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace VSIXProject
{
    public sealed class SolutionLoadManager : IVsSolutionLoadManager
    {
        #region IVsSolutionLoadManager

        public int OnDisconnect()
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeOpenProject(ref Guid guidProjectID, ref Guid guidProjectType, string pszFileName, IVsSolutionLoadManagerSupport pSLMgrSupport)
        {
            pSLMgrSupport?.SetProjectLoadPriority(guidProjectID, (uint)_VSProjectLoadPriority.PLP_ExplicitLoadOnly);

            // Verify
            uint loadStateUint = 4;
            pSLMgrSupport?.GetProjectLoadPriority(guidProjectID, out loadStateUint);
            var loadState = (_VSProjectLoadPriority)loadStateUint;

            System.Diagnostics.Debug.Assert(loadState == _VSProjectLoadPriority.PLP_ExplicitLoadOnly);

            return VSConstants.S_OK;
        }

        #endregion IVsSolutionLoadManager
    }
}
