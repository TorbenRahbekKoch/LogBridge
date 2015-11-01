## Version 1.3

This version has a few internal plumbing changes, e.g.

  - The use of xUnit instead of NUnit
  - Simplication of some internal inheritance
  - Use of a T4 template to generate Log.XXX methods.

### Bug fixes

  - Issue #3 with missing exception message for log4net
  - Missing exception message for FaultExceptions - a minor brain hemorage on my part. 

### New featuers

  - LogLocation.Here() - please see <a href="Readme.md">Readme.md</a>
  - More Log.XXX overloads.

### Breaking changes

  - Removal of the *stackFrameOffsetCount* property from the *logBridge* configuration element. Please remove this manually when upgrading.