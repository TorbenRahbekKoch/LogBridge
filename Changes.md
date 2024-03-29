## Version 1.6

  - Updated vulnerable log4net to 2.0.15
  - Generally updated projects to .NET 4.8 Frameworkss
  - No longer publishing Umbraco.Log4Net package 

## Version 1.5

  - Removed special handling of the ApplicationName property. This can be considered a (minor) breaking change.
  - Fixed a bug where more specifically given properties - e.g. in a thread instead of process-level - were not always overridden correctly.

## Version 1.4

  - Updated framework references to 4.6.1 from 4.5
  - Add SetExtendedProperty method to LogContext
  - Fix a couple of brittle tests
  - Sign the assemblies - except LogBridge.UmbracoLog4Net

## Version 1.3

This version has a few internal plumbing changes, e.g.

  - The use of xUnit instead of NUnit
  - Simplication of some internal inheritance
  - Use of a T4 template to generate Log.XXX methods.

### Bug fixes

  - Issue #3 with missing exception message for log4net
  - Missing exception message for FaultExceptions - a minor brain hemorage on my part. 
  - Describe.XXX now has an internal recursionDepth stopping infinite recursion AKA stack overflows from happening.

### New featuers

  - LogLocation.Here() - please see <a href="Readme.md">Readme.md</a>
  - More Log.XXX overloads.

### Breaking changes

  - Removal of the *stackFrameOffsetCount* property from the *logBridge* configuration element. Please remove this manually when upgrading.
  - The static ThreadLogContext etc. on the Log class have been moved to the LogContext class instead.
